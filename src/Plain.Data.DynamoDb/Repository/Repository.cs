using System.Collections.Generic;
using System.Linq;
using Plain.Infrastructure.Interfaces.Data;
using Plain.Infrastructure.Interfaces.Domain;
using Plain.Infrastructure.Interfaces;
using Plain.Infrastructure.Domain;
using Plain.Infrastructure.Interfaces.Services;
using Plain.Data.DynamoDb.Interfaces;

namespace Plain.Data.DynamoDb.Repository
{
    public class Repository<TKey, T> : IRepository<TKey, T> where T : class, IEntityKey<TKey>
    {
        private IDynamoDbUnitOfWork _unitOfWork;
        public IUnitOfWork UnitOfWork { get => _unitOfWork; }

        public Repository(IDynamoDbUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public virtual T FindBy(TKey id)
        {
            return null;
        }

        public virtual IList<T> All()
        {
            return null;
        }

        public virtual PagedResult<T> All(int page, int itemsPerPage)
        {
            return new PagedResult<T>
            {
                PageOfResults = new List<T>(),
                TotalItems = 0
            };
        }

        public virtual void Add(T entity)
        {
            
        }

        public void Add(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public virtual void Update(T entity)
        {
            
        }

        public virtual void Delete(T entity)
        {
            
        }

        public virtual void Delete(TKey id)
        {
            Delete(FindBy(id));
        }

        public void Delete(IEnumerable<T> entities)
        {
            foreach (var item in entities)
            {
                Delete(item);
            }
        }
    }
}
