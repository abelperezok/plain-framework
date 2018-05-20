using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NHibernate.Linq;
using Plain.Data.NHibernate.Interfaces;
using Plain.Infrastructure.Interfaces.Domain;
using Plain.Infrastructure.Interfaces.Data;
using Plain.Infrastructure.Interfaces;
using Plain.Infrastructure.Domain;
using Plain.Infrastructure.Interfaces.Services;

namespace Plain.Data.NHibernate.Repository
{
    public class Repository<TKey, T> : IRepository<TKey, T> where T : class, IEntityKey<TKey>
    {
        protected readonly INHUnitOfWork _unitOfWork;

        public Repository(INHUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        public virtual T FindBy(TKey id)
        {
            return _unitOfWork.Session.Get<T>(id);
        }

        public virtual void Add(T entity)
        {
            _unitOfWork.Session.Save(entity);
        }

        public virtual void Add(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                _unitOfWork.Session.Save(item);
            }
        }

        public virtual void Update(T entity)
        {
            _unitOfWork.Session.Update(entity);
        }

        public void Delete(T entity)
        {
            _unitOfWork.Session.Delete(entity);
        }

        public virtual void Delete(TKey id)
        {
            _unitOfWork.Session.Delete(FindBy(id));
        }

        public void Delete(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                _unitOfWork.Session.Delete(entity);
            }
        }

        public virtual IList<T> All()
        {
            return Query().ToList();
        }

        public virtual PagedResult<T> All(int page, int itemsPerPage)
        {
            PagedResult<T> result = new PagedResult<T>();
            var items = Query();

            if (page <= 0)
                page = 1;

            if (itemsPerPage > 0)
            {
                result.PageOfResults = items.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            }
            else
            {
                result.PageOfResults = items.ToList();
            }

            result.TotalItems = items.Count();

            return result;
        }

        protected PagedResult<T> All(int page, int itemsPerPage, IQueryable<T> queryable)
        {
            PagedResult<T> result = new PagedResult<T>();
            var items = queryable;

            if (page <= 0)
                page = 1;

            if (itemsPerPage > 0)
            {
                result.PageOfResults = items.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            }
            else
            {
                result.PageOfResults = items.ToList();
            }

            result.TotalItems = items.Count();

            return result;
        }


        
        #region [    Metodos protegidos    ]

        protected IQueryable<T> Query()
        {
            return _unitOfWork.Session.Query<T>();
        }

        protected T FindBy(Expression<Func<T, bool>> expression)
        {
            return FilterBy(expression).SingleOrDefault();
        }

        protected IQueryable<T> FilterBy(Expression<Func<T, bool>> expression)
        {
            return Query().Where(expression).AsQueryable();
        }

        protected IQueryable<T> ToPage(IQueryable<T> items, int page, int pageSize)
        {
            if (page <= 0)
                page = 1;

            if (pageSize > 0)
            {
                return items.Skip((page - 1) * pageSize).Take(pageSize);
            }
            return items;
        }

        #endregion
    }

    public class Repository<T> : Repository<int, T>, ICrudServices<T> where T : class, IEntityKey<int>
    {
        public Repository(INHUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
