using Plain.Infrastructure.Domain;
using Plain.Infrastructure.Interfaces.Data;
using Plain.Infrastructure.Interfaces.Domain;
using Plain.Infrastructure.Interfaces.Services;
using System.Collections.Generic;

namespace Plain.Infrastructure.Services
{
    public class Services<TKey, TEntity> : IServices<TKey, TEntity> where TEntity : class, IEntityKey<TKey>
    {
        protected IRepository<TKey, TEntity> _repository;

        public Services(IRepository<TKey, TEntity> repository)
        {
            _repository = repository;
        }

        public TEntity FindBy(TKey id)
        {
            return _repository.FindBy(id);
        }

        public IList<TEntity> All()
        {
            return _repository.All();
        }

        public PagedResult<TEntity> All(int page, int itemsPerPage)
        {
            return _repository.All(page,itemsPerPage);
        }
    }
}
