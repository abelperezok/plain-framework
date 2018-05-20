using Plain.Infrastructure.Interfaces.Data;
using Plain.Infrastructure.Interfaces.Domain;
using Plain.Web.Mvc.Models;
using System.Linq;

namespace Plain.Web.Mvc.Controllers
{
    public class RepositoryCrudController<TEntity, TEV, TLV> : CrudControllerBase<int, TEntity, TEV, TLV>
        where TEntity : class, IEntityKey<int>, new()
        where TEV : EditViewModel, new()
        where TLV : ListViewModel<TEntity>, new()
    {
        protected IRepository<TEntity> _repository;

        public RepositoryCrudController(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        protected override TEntity LoadEntiy(int id)
        {
            return _repository.FindBy(id);
        }

        protected override Result ExecuteUpdate(TEntity entity, TEV viewModel)
        {
            _repository.Update(entity);
            return new Result { Success = true };
        }

        protected override Result ExecuteDelete(int id)
        {
            _repository.Delete(id);
            return new Result { Success = true };
        }

        protected override Result ExecuteInsert(TEntity entity, TEV viewModel)
        {
            _repository.Add(entity);
            return new Result { Success = true };
        }

        protected override TLV ExecuteList(int CurrentPage)
        {
            var query = _repository.All(CurrentPage,_pageSize);
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
