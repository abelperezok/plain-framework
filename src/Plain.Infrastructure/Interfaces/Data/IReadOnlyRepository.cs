using Plain.Infrastructure.Domain;
using Plain.Infrastructure.Interfaces.Domain;
using System.Collections.Generic;

namespace Plain.Infrastructure.Interfaces.Data
{
    public interface IReadOnlyRepository<TKey, TEntity> where TEntity : class, IEntityKey<TKey>
    {
        IUnitOfWork UnitOfWork { get; }

        TEntity FindBy(TKey id);

        IList<TEntity> All();

        PagedResult<TEntity> All(int page, int itemsPerPage);
    }

    public interface IReadOnlyRepository<T> : IReadOnlyRepository<int, T> where T : class, IEntityKey<int>
    {

    }
}
