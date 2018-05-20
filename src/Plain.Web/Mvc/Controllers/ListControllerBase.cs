using System.Collections.Generic;
using Plain.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Plain.Infrastructure.Interfaces.Domain;

namespace Plain.Web.Mvc.Controllers
{
    public abstract class ListControllerBase<TId, TEntity,TLV> : Controller
        where TEntity : class, IEntityKey<TId>, new()
        where TLV : ListViewModel<TEntity>, new()
    {

        protected string _indexView = "Index";
        protected string _pageParam = "page";
        protected int _pageSize = 15;

        protected Dictionary<string, object> _dictionary = new Dictionary<string, object>();

        [HttpGet]
        public virtual ActionResult Index(int page = 1)
        {
            return View(_indexView, ExecuteList(CurrentPage));
        }

        [HttpPost]
        public virtual ActionResult Index(FormCollection collection)
        {
            return RedirectToAction("Index", GetRoutesValues());
        }

        #region [    Metodos protegidos    ]

        protected abstract TLV ExecuteList(int CurrentPage);

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

        protected virtual RouteValueDictionary GetRoutesValues()
        {
            var result = new RouteValueDictionary();
            foreach (var item in _dictionary.Keys)
            {
                result.Add(item, _dictionary[item]);
            }
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
    }
}
