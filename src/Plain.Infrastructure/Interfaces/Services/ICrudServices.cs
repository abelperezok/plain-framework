using Plain.Infrastructure.Interfaces.Domain;

namespace Plain.Infrastructure.Interfaces.Services
{
    public interface ICrudServices<TKey, TEntity> : IServices<TKey, TEntity> where TEntity : class, IEntityKey<TKey>
    {
        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TKey  id);
    }

    public interface ICrudServices<TEntity> : IServices<TEntity> where TEntity : class, IEntityKey<int>
    {
        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(int id);
    }
}
