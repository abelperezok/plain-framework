using Microsoft.AspNetCore.Mvc.Filters;
using Plain.Infrastructure.Domain;
using Plain.Infrastructure.Interfaces.Domain;
using Plain.Web.Mvc.Models;
using System.Linq;

namespace Plain.Web.Mvc.Controllers
{
    public abstract class CrudFilterController<TId,TEntity, TEV, TLV, TF> : CrudControllerBase<TId, TEntity, TEV, TLV>
        where TEntity : class, IEntityKey<TId>, new()
        where TEV : EditViewModel, new()
        where TLV : ListViewModel<TEntity>, new()
        where TF : FilterViewModel, new()
    {
        protected TF _filter = new TF();

        protected virtual void InitializeFilter(TF filter)
        { 
            
        }

        protected abstract void FillDictionary(TF filter);

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            FillDictionary();
            TryUpdateModelAsync(_filter);
            FillDictionary(_filter);
            ViewBag.Params = _dictionary;
        }

        protected override TLV ExecuteList(int CurrentPage)
        {
            InitializeFilter(_filter);
            return ExecuteList(CurrentPage, _filter);
        }

        protected abstract PagedResult<TEntity> GetList(int page, TF filter);

        protected virtual TLV ExecuteList(int page, TF filter)
        {
            var result = GetList(page, filter);
            return new TLV
            {
                CurrentPage = page,
                Items = result.PageOfResults.ToList(),
                PageSize = _pageSize,
                TotalItems = result.TotalItems,
                Filter = _filter
            };
        }
    }
}
