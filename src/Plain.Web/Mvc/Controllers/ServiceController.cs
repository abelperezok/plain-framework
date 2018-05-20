using Plain.Infrastructure.Domain;
using Plain.Infrastructure.Interfaces.Domain;
using Plain.Infrastructure.Interfaces.Services;
using Plain.Web.Mvc.Controllers;
using Plain.Web.Mvc.Models;
using System.Linq;

namespace Plain.Controllers
{
    public abstract class ServiceController<TEntity, TEV, TLV> : CrudControllerBase<int, TEntity, TEV, TLV>
        where TEntity : class, IEntityKey<int>, new()
        where TEV : EditViewModel, new()
        where TLV : ListViewModel<TEntity>, new()
    {
        protected IServices<TEntity> _service;

        public ServiceController(IServices<TEntity> service)
        {
            _service = service;
        }

        protected override TEntity LoadEntiy(int id)
        {
            return _service.FindBy(id);
        }

        protected virtual PagedResult<TEntity> GetData(int currentPage,int itemsByPage)
        {
            return _service.All(currentPage, itemsByPage);
        }

        protected override TLV ExecuteList(int CurrentPage)
        {
            var query = GetData(CurrentPage,_pageSize);
            return new TLV
            {
                CurrentPage = CurrentPage,
                Items = query.PageOfResults.ToList(),
                PageSize = _pageSize,
                TotalItems = query.TotalItems
            };
        }
    }
}

