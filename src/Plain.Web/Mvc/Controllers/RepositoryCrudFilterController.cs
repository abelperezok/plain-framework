using Plain.Infrastructure.Domain;
using Plain.Infrastructure.Interfaces.Data;
using Plain.Infrastructure.Interfaces.Domain;
using Plain.Web.Mvc.Models;
using System.Linq;

namespace Plain.Web.Mvc.Controllers
{
    public abstract class RepositoryCrudFilterController<TEntity, TEV, TLV,TF> : CrudFilterController<int, TEntity, TEV, TLV,TF>
        where TEntity : class, IEntityKey<int>, new()
        where TEV : EditViewModel, new()
        where TLV : ListViewModel<TEntity>, new()
        where TF : FilterViewModel, new()
    {
        private IRepository<TEntity> _repository;

        public RepositoryCrudFilterController(IRepository<TEntity> repository)
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

        protected override TEntity LoadEntiy(int id)
        {
            return _repository.FindBy(id);
        }

        protected override Result ExecuteUpdate(TEntity entity, TEV viewModel)
        {
            return Execute(() => _repository.Update(entity));
        }

        protected override Result ExecuteDelete(int id)
        {
            return Execute(() => _repository.Delete(id));
        }

        protected override Result ExecuteInsert(TEntity entity, TEV viewModel)
        {
            return Execute(() => _repository.Add(entity));
        }

        protected abstract PagedResult<TEntity> ApplyFilter(IRepository<TEntity> query, TF filter);
    }
}
