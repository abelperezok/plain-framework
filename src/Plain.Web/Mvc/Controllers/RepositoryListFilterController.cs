using System.Linq;
using Plain.Web.Mvc.Models;
using Plain.Infrastructure.Interfaces.Domain;
using Plain.Infrastructure.Interfaces.Data;
using Plain.Infrastructure.Domain;

namespace Plain.Web.Mvc.Controllers
{
    public abstract class RepositoryListFilterController<TEntity, TLV, TF> : ListFilterControllerBase<int, TEntity, TLV, TF>
        where TEntity : class, IEntityKey<int>, new()
        where TLV : ListViewModel<TEntity>, new()
        where TF : FilterViewModel, new()
    {

        private IRepository<TEntity> _repository;

        public RepositoryListFilterController(IRepository<TEntity> repository)
        {
            _repository = repository;
        }


        protected override TLV ExecuteList(int CurrentPage, TF filter)
        {
            var query = ApplyFilter(_repository, filter);
            return new TLV
            {
                CurrentPage = CurrentPage,
                Items = query.PageOfResults.ToList(),
                PageSize = _pageSize,
                TotalItems = query.TotalItems,
                Filter = filter
            };
        }

        protected abstract PagedResult<TEntity> ApplyFilter(IRepository<TEntity> query, TF filter);
    }
}
