using Plain.Infrastructure.Interfaces.Domain;
using Plain.Library.ConvertType;

namespace Plain.Setting
{
    public class Setting : IEntityKey<int>
    {
        public Setting() { }

        public Setting(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public virtual int ID { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public virtual string Value { get; set; }

        /// <summary>
        /// Returns the setting value as the specified type
        /// </summary>
        public virtual T As<T>()
        {
            return ConvertHelper.To<T>(this.Value);
        }

        public override string ToString()
        {
            return Name;
        }

        
    }
}
