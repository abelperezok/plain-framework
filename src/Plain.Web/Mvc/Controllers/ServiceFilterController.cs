using Plain.Infrastructure.Interfaces.Domain;
using Plain.Infrastructure.Interfaces.Services;
using Plain.Web.Mvc.Controllers;
using Plain.Web.Mvc.Models;

namespace Plain.Controllers
{
    public abstract class ServiceFilterController<TEntity, TEV, TLV, TF> : CrudFilterController<int, TEntity, TEV, TLV, TF>
        where TEntity : class, IEntityKey<int>, new()
        where TEV : EditViewModel, new()
        where TLV : ListViewModel<TEntity>, new()
        where TF : FilterViewModel, new()
    {
        protected IServices<TEntity> _service;

        public ServiceFilterController(IServices<TEntity> service)
        {
            _service = service;
        }

        protected override TEntity LoadEntiy(int id)
        {
            return _service.FindBy(id);
        }
    }
}

