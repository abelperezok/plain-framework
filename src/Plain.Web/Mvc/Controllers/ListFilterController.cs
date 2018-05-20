using Plain.Infrastructure.Interfaces.Domain;
using Plain.Web.Mvc.Controllers;
using Plain.Web.Mvc.Models;

namespace Plain.Controllers
{
    public abstract class ListFilterController<TEntity, TF> : ListFilterControllerBase<int, TEntity, ListViewModel<TEntity>, TF>
        where TEntity : class, IEntityKey<int>, new()
        where TF : FilterViewModel, new()
    {
        
    }
}
