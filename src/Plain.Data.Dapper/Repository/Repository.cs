using Plain.Data.Dapper.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Plain.Infrastructure.Interfaces.Data;
using Plain.Infrastructure.Interfaces.Domain;
using Plain.Infrastructure.Interfaces;
using Plain.Infrastructure.Domain;
using Plain.Infrastructure.Interfaces.Services;

namespace Plain.Data.Dapper.Repository
{
    public class Repository<TKey, T> : IRepository<TKey, T> where T : class, IEntityKey<TKey>
    {
        protected readonly IDapperUnitOfWork _unitOfWork;

        protected readonly ISqlGenerator _sqlGenerator;

        public Repository(IDapperUnitOfWork unitOfWork, ISqlGenerator sqlGenerator)
        {
            _unitOfWork = unitOfWork;
            _sqlGenerator = sqlGenerator;
        }

        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        public virtual T FindBy(TKey id)
        {
            return _unitOfWork.DbConnection.Query<T>(_sqlGenerator.GetSqlFindBy(), new { id }, _unitOfWork.DbTransaction).SingleOrDefault();
        }

        public virtual IList<T> All()
        {
            return _unitOfWork.DbConnection.Query<T>(_sqlGenerator.GetSqlAll(), null, _unitOfWork.DbTransaction).ToList();
        }

        public virtual PagedResult<T> All(int page, int itemsPerPage)
        {
            return new PagedResult<T>
            {
                PageOfResults = _unitOfWork.DbConnection.Query<T>(_sqlGenerator.GetSqlPageAll(),new { PageNumber = page,  PageSize = itemsPerPage,  }, _unitOfWork.DbTransaction).ToList(),
                TotalItems = _unitOfWork.DbConnection.ExecuteScalar<int>(_sqlGenerator.GetSqlCount(), null, _unitOfWork.DbTransaction)
            };
        }

        public virtual void Add(T entity)
        {
            _unitOfWork.DbConnection.Execute(_sqlGenerator.GetSqlInsert(), entity, _unitOfWork.DbTransaction);
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
            _unitOfWork.DbConnection.Execute(_sqlGenerator.GetSqlUpdate(), entity, _unitOfWork.DbTransaction);
        }

        public virtual void Delete(T entity)
        {
            if (entity != null)
            {
                _unitOfWork.DbConnection.Execute(_sqlGenerator.GetSqlDelete(), entity, _unitOfWork.DbTransaction);
            }
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

    public class Repository<T> : Repository<int, T>, ICrudServices<T> where T : class, IEntityKey<int>
    {
        public Repository(IDapperUnitOfWork unitOfWork, ISqlGenerator sqlGenerator)
            : base(unitOfWork, sqlGenerator)
        {
        }

        public override void Add(T entity)
        {
            entity.ID = _unitOfWork.DbConnection.ExecuteScalar<int>(_sqlGenerator.GetSqlInsert(), entity, _unitOfWork.DbTransaction);
        }
    }
}
