using Plain.Infrastructure.Domain;
using Plain.Infrastructure.Interfaces.Domain;
using System.Collections.Generic;

namespace Plain.Infrastructure.Interfaces.Services
{
    public interface IServices<TKey, TEntity> where TEntity : class, IEntityKey<TKey>
    {
        TEntity FindBy(TKey id);

        IList<TEntity> All();

        PagedResult<TEntity> All(int page, int itemsPerPage);

    }

    public interface IServices<TEntity> : IServices<int, TEntity> where TEntity : class, IEntityKey<int>
    {

    }
}
