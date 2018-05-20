using Plain.Caching;
using Plain.Infrastructure.Interfaces.Data;
using Plain.Infrastructure.Interfaces.Services;
using Plain.Library.ConvertType;
using Plain.Setting.Caching;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plain.Setting
{
    public partial class SettingService : ISettingService
    {
        #region Constants
        private const string SETTINGS_ALL_KEY = "App.setting.all";
        #endregion

        #region Fields

        private readonly IRepository<Setting> _settingRepository;

        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="settingRepository">Setting repository</param>
        public SettingService(ICacheManager cacheManager,
            IRepository<Setting> settingRepository)
        {
            this._cacheManager = cacheManager;
            this._settingRepository = settingRepository;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Adds a setting
        /// </summary>
        /// <param name="setting">Setting</param>
        /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
        public virtual void InsertSetting(Setting setting, bool clearCache = true)
        {
            if (setting == null)
                throw new ArgumentNullException("setting");

            _settingRepository.Add(setting);

            //cache
            if (clearCache)
                _cacheManager.RemoveByPattern(SETTINGS_ALL_KEY);
        }

        /// <summary>
        /// Updates a setting
        /// </summary>
        /// <param name="setting">Setting</param>
        /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
        public virtual void UpdateSetting(Setting setting, bool clearCache = true)
        {
            if (setting == null)
                throw new ArgumentNullException("setting");

            _settingRepository.Add(setting);

            //cache
            if (clearCache)
                _cacheManager.RemoveByPattern(SETTINGS_ALL_KEY);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a setting by identifier
        /// </summary>
        /// <param name="settingId">Setting identifer</param>
        /// <returns>Setting</returns>
        public virtual Setting GetSettingById(int settingId)
        {
            if (settingId == 0)
                return null;

            var setting = _settingRepository.FindBy(settingId);
            return setting;
        }

        /// <summary>
        /// Get setting value by key
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">Key</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>Setting value</returns>
        public virtual T GetSettingByKey<T>(string key, T defaultValue = default(T))
        {
            if (String.IsNullOrEmpty(key))
                return defaultValue;

            key = key.Trim().ToLowerInvariant();

            var settings = GetAllSettings();
            if (settings.ContainsKey(key))
            {
                var setting = settings[key];
                return setting.As<T>();
            }
            return defaultValue;
        }

        /// <summary>
        /// Set setting value
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
        public virtual void SetSetting<T>(string key, T value, bool clearCache = true)
        {
            var settings = GetAllSettings();

            key = key.Trim().ToLowerInvariant();

            Setting setting = null;
            string valueStr = ConvertHelper.GetCustomTypeConverter(typeof(T)).ConvertToInvariantString(value);
            if (settings.ContainsKey(key))
            {
                //update
                setting = settings[key];
                //little hack here because of EF issue
                setting = GetSettingById(setting.ID);
                setting.Value = valueStr;
                UpdateSetting(setting, clearCache);
            }
            else
            {
                //insert
                setting = new Setting()
                {
                    Name = key,
                    Value = valueStr,
                };
                InsertSetting(setting, clearCache);
            }
        }

        /// <summary>
        /// Deletes a setting
        /// </summary>
        /// <param name="setting">Setting</param>
        public virtual void DeleteSetting(Setting setting)
        {
            if (setting == null)
                throw new ArgumentNullException("setting");

            _settingRepository.Delete(setting);

            //cache
            _cacheManager.RemoveByPattern(SETTINGS_ALL_KEY);
        }

        /// <summary>
        /// Gets all settings
        /// </summary>
        /// <returns>Setting collection</returns>
        public virtual IDictionary<string, Setting> GetAllSettings()
        {
            //cache
            string key = string.Format(SETTINGS_ALL_KEY);
            return _cacheManager.Get(key, () =>
            {
                var query = from s in _settingRepository.All()
                            orderby s.Name
                            select s;
                var settings = query.ToDictionary(s => s.Name.ToLowerInvariant());

                return settings;
            });
        }

        /// <summary>
        /// Clear cache
        /// </summary>
        public virtual void ClearCache()
        {
            _cacheManager.RemoveByPattern(SETTINGS_ALL_KEY);
        }
        #endregion

        public TSettings GetSettings<TSettings>() where TSettings : ISettings, new()
        {
            TSettings settings = Activator.CreateInstance<TSettings>();

            // get properties we can write to
            var properties = from prop in typeof(TSettings).GetProperties()
                             where prop.CanWrite && prop.CanRead
                             let setting = this.GetSettingByKey<string>(typeof(TSettings).Name + "." + prop.Name)
                             where setting != null
                             where ConvertHelper.GetCustomTypeConverter(prop.PropertyType).CanConvertFrom(typeof(string))
                             let value = ConvertHelper.GetCustomTypeConverter(prop.PropertyType).ConvertFromInvariantString(setting)
                             select new { prop, value };

            // assign properties
            properties.ToList().ForEach(p => p.prop.SetValue(settings, p.value, null));

            return settings;
        }

        public TSettings SaveSettings<TSettings>(TSettings settings) where TSettings : ISettings, new()
        {
            var properties = from prop in typeof(TSettings).GetProperties()
                             where prop.CanWrite && prop.CanRead
                             where ConvertHelper.GetCustomTypeConverter(prop.PropertyType).CanConvertFrom(typeof(string))
                             select prop;

            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            foreach (var prop in properties)
            {
                string key = typeof(TSettings).Name + "." + prop.Name;
                //Duck typing is not supported in C#. That's why we're using dynamic type
                dynamic value = prop.GetValue(settings, null);
                if (value != null)
                    this.SetSetting(key, value, false);
                else
                    this.SetSetting(key, "", false);
            }

            //and now clear cache
            this.ClearCache();

            return settings;
        }
    }
}
