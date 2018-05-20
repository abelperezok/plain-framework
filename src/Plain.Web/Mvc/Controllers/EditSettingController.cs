using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Plain.Library.Reflection;
using Plain.Setting;
using Plain.Web.Mvc.Models;
using System;

namespace Plain.Web.Mvc.Controllers
{
    public class EditSettingController<TSetting, TEV> : Controller
        where TSetting : class, ISettings, new()
        where TEV : EditViewModel, new()
    {
        private ISettingService _settingService;

        protected string _editView = "EditSetting";

        public EditSettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }


        [HttpGet]
        public virtual ActionResult Edit()
        {
            var entity = _settingService.GetSettings<TSetting>();
            if (entity != null)
            {
                var viewModel = CreateViewModel(entity);
                return View(_editView,viewModel);
            }
            return NotFound();
        }

        [HttpPost]
        public virtual ActionResult Edit(TEV viewModel)
        {
            var entity = _settingService.GetSettings<TSetting>();
            if (ModelState.IsValid)
            {
                if (entity != null)
                {
                    FromViewModelToEntity(viewModel, entity);
                    var result = ExecuteUpdate(entity, viewModel);
                    if (result.Success)
                    {
                        return RedirectToAction("Index", new { controller = "AdminSetting" });
                    }
                    else
                    {
                        FillErrorMessages(result);
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            InitializeEditViewModel(viewModel);
            FromEntityToViewModel(entity,viewModel);
            return View(_editView, viewModel);
        }

        #region [    Metodos protegidos    ]

        protected virtual void InitializeEditViewModel(TEV viewModel)
        {

        }

        protected TSetting CreateEntity(TEV viewModel)
        {
            TSetting entity = new TSetting();
            FromViewModelToEntity(viewModel, entity);
            return entity;
        }

        protected TEV CreateViewModel(TSetting entity)
        {
            TEV viewModel = new TEV();
            InitializeEditViewModel(viewModel);
            FromEntityToViewModel(entity, viewModel);
            return viewModel;
        }

        protected virtual void FromViewModelToEntity(TEV viewModel, TSetting entity)
        {
            Mapping.Map(viewModel, entity);
        }

        protected virtual void FromEntityToViewModel(TSetting entity, TEV viewModel)
        {
            Mapping.Map(entity, viewModel);
        }

        #region [    Actions    ]


        protected virtual Result ExecuteUpdate(TSetting entity, TEV viewModel)
        {
            var result = new Result { Success = true };
            _settingService.SaveSettings<TSetting>(entity);
            return result;
        }

        #endregion

        #endregion

        protected  void OnResultExecuted(ResultExecutedContext filterContext)//override
        {
            if (!Request.IsAjaxRequest()) return;

            //Response.HttpContext.Request.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetExpires(DateTime.Today.AddYears(-1));
        }

        protected virtual void FillErrorMessages(Result result)
        {
            foreach (var item in result.Messages)
            {
                if (String.IsNullOrEmpty(item.Key))
                {
                    ModelState.AddModelError(item.Key, item.Text);
                }
                else
                {
                   // ModelState.AddModelError(item.Key, Convert.ToString(HttpContext.GetGlobalResourceObject("", item.Key)));
                }
            }
        }
    }
}
