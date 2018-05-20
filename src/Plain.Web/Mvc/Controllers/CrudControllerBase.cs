using System;
using System.Collections.Generic;
using Plain.Library.Reflection;
using Plain.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Plain.Infrastructure.Interfaces.Domain;

namespace Plain.Web.Mvc.Controllers
{
    public abstract class CrudControllerBase<TId, TEntity, TEV, TLV> : Controller
        where TEntity : class, IEntityKey<TId>, new()
        where TEV : EditViewModel, new()
        where TLV : ListViewModel<TEntity>, new()
    {

        protected string _editView = "Edit";
        protected string _createView = "Create";
        protected string _indexView = "Index";
        protected string _pageParam = "page";
        protected int _pageSize = 15;

        protected Dictionary<string, object> _dictionary = new Dictionary<string, object>();

        [HttpGet]
        public virtual ActionResult Index(int page = 1)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", ExecuteList(CurrentPage));
            }
            return View(_indexView, ExecuteList(CurrentPage));
        }

        [HttpPost]
        public virtual ActionResult Index(FormCollection collection)
        {
            return RedirectToAction("Index", GetRoutesValues());
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            TEV viewModel = new TEV();
            InitializeEditViewModel(viewModel);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Create",viewModel);
            }

            return View(_createView, viewModel);
        }

        protected virtual void InitializeEditViewModel(TEV viewModel)
        {
        }

        [HttpPost]
        public virtual ActionResult Create(TEV viewModel, string option)
        {
            if (ModelState.IsValid)
            {
                var result = ExecuteInsert(CreateEntity(viewModel), viewModel);
                if (result.Success)
                {
                    if (Request.IsAjaxRequest())
                    {
                        if (option == "Save")
                            return PartialView("_List", ExecuteList(CurrentPage));
                        else
                            return PartialView("_Create", viewModel);
                    }
                    else
                    {
                        if (option == "Save")
                            return RedirectToAction("Index", GetRoutesValues());
                        else
                            return RedirectToAction("Create", GetRoutesValues());
                    }
                }
                else
                {
                    FillErrorMessages(result);
                }
            }

            viewModel = InitializeEditViewModel();
            InitializeEditViewModel(viewModel);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Create", viewModel);
            }
            else
            {
                return View(_createView, viewModel);
            }

        }

        [HttpGet]
        public virtual ActionResult Edit(TId id)
        {
            var entity = LoadEntiy(id);
            if (entity != null)
            {
                var viewModel = CreateViewModel(entity);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Edit", viewModel);
                }

                return View(_editView, viewModel);
            }
            return NotFound();
        }
        
        [HttpPost]
        public virtual ActionResult Edit(TId Id, TEV viewModel)
        {
            var entity = LoadEntiy(Id);
            if (ModelState.IsValid)
            {
                if (entity != null)
                {
                    FromViewModelToEntity(viewModel, entity);
                    var result = ExecuteUpdate(entity, viewModel);
                    if (result.Success)
                    {
                        if (Request.IsAjaxRequest())
                        {
                            return PartialView("_List", ExecuteList(CurrentPage));
                        }
                        else
                        {
                            return RedirectToAction("Index", GetRoutesValues());
                        }
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
            FromEntityToViewModel(entity, viewModel);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Edit", viewModel);
            }

            return View(_editView, viewModel);
        }

        [HttpPost]
        public virtual ActionResult Delete(TId id)
        {
            var result = ExecuteDelete(id);
            return RedirectToAction("Index", GetRoutesValues());
        }        
        
        #region [    Metodos protegidos    ]        

        protected abstract TEntity LoadEntiy(TId id);

        protected abstract Result ExecuteUpdate(TEntity entity, TEV viewModel);

        protected abstract Result ExecuteDelete(TId id);

        protected abstract Result ExecuteInsert(TEntity entity, TEV viewModel);

        protected virtual TEV InitializeEditViewModel()
        {
            return new TEV();
        }

        protected abstract TLV ExecuteList(int CurrentPage);

        protected TEntity CreateEntity(TEV viewModel)
        {
            TEntity entity = new TEntity();
            FromViewModelToEntity(viewModel, entity);
            return entity;
        }

        protected virtual TEV CreateViewModel(TEntity entity)
        {
            TEV viewModel = InitializeEditViewModel();
            InitializeEditViewModel(viewModel);
            FromEntityToViewModel(entity, viewModel);
            return viewModel;
        }

        protected virtual void FromViewModelToEntity(TEV viewModel, TEntity entity)
        {
            Mapping.Map(viewModel, entity);
        }

        protected virtual void FromEntityToViewModel(TEntity entity, TEV viewModel)
        {
            Mapping.Map(entity, viewModel);
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
                    //ModelState.AddModelError(item.Key, Convert.ToString(HttpContext.GetGlobalResourceObject("", item.Key)));
                }
            }
        }

        #endregion

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            FillDictionary();
            ViewBag.Params = _dictionary;
        }

        protected void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (!Request.IsAjaxRequest()) return;

            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetExpires(DateTime.Today.AddYears(-1));
        }

        protected virtual int CurrentPage
        {
            get { return 1; /*Convert.ToInt32(Request.Params[_pageParam] ?? "1");*/ }
        }

        protected RouteValueDictionary GetRoutesValues()
        {
            var result = new RouteValueDictionary(_dictionary);
            return result;
        }

        protected virtual void FillDictionary()
        {
            _dictionary.Clear();
            _dictionary.Add(_pageParam, CurrentPage);
        }

        protected void AddToDictionary(string key, object value)
        {
            _dictionary.Add(key, value);
        }

        protected Result Execute(Action action)
        {
            var result = new Result { Success = true };
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Messages.Add(new Message { Key = String.Empty, Text = e.Message });
            }
            return result;
        }

        public string GetResourceText(string key)
        {
            string value = ""; //Convert.ToString(HttpContext.GetGlobalResourceObject("", key));
            
            if (String.IsNullOrEmpty(value))
            {
                value = String.Concat("[", key, "]");
            }
            return value;
        }
    }
}
