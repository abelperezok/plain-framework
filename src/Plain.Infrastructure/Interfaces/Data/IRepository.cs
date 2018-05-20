using Plain.Infrastructure.Interfaces.Domain;
using System.Collections.Generic;

namespace Plain.Infrastructure.Interfaces.Data
{
    public interface IRepository<TKey, TEntity> : IReadOnlyRepository<TKey, TEntity> where TEntity : class, IEntityKey<TKey>
    {
        void Add(TEntity entity);

        void Add(IEnumerable<TEntity> items);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(TKey id);

        void Delete(IEnumerable<TEntity> entities);
    }

    public interface IRepository<T> : IRepository<int, T> where T : class, IEntityKey<int>
    {

    }
}
