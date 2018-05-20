using Plain.Infrastructure.Interfaces.Data;
using Plain.Infrastructure.Interfaces.Domain;
using Plain.Infrastructure.Interfaces.Services;

namespace Plain.Infrastructure.Services
{
    public class CrudServices<TKey, TEntity> : Services<TKey, TEntity> , ICrudServices<TKey, TEntity> where TEntity : class, IEntityKey<TKey>
    {
        public CrudServices(IRepository<TKey, TEntity> repository)
            :base(repository)
        {
        }

        public void Add(TEntity entity)
        {
            _repository.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _repository.Update(entity);
        }

        public void Delete(TKey id)
        {
            _repository.Delete(id);
        }
    }

    public class CrudServices<TEntity> : CrudServices<int, TEntity> where TEntity : class, IEntityKey<int>
    {
        public CrudServices(IRepository<int, TEntity> repository) : base(repository)
        {
        }
    }
}
