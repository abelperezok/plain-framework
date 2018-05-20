using Microsoft.AspNetCore.Mvc.Filters;
using Plain.Infrastructure.Interfaces.Domain;
using Plain.Web.Mvc.Models;

namespace Plain.Web.Mvc.Controllers
{
    public abstract class ListFilterControllerBase<TId, TEntity, TLV, TF> : ListControllerBase<TId, TEntity, TLV>
        where TEntity : class, IEntityKey<TId>, new()
        where TLV : ListViewModel<TEntity>, new()
        where TF : FilterViewModel, new()
    {
        protected TF _filter = new TF();

        protected abstract TLV ExecuteList(int CurrentPage, TF filter);

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
    }
}
