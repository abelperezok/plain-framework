using Microsoft.EntityFrameworkCore;
using Plain.Data.EntityFramework.Interfaces;
using Plain.Infrastructure.Domain;
using Plain.Infrastructure.Interfaces;
using Plain.Infrastructure.Interfaces.Data;
using Plain.Infrastructure.Interfaces.Domain;
using Plain.Infrastructure.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Plain.Data.EntityFramework.Repository
{
    public class Repository<TKey, T> : IRepository<TKey, T> where T : class, IEntityKey<TKey>
    {
        protected readonly IEFUnitOfWork _unitOfWork;

        protected readonly DbSet<T> _dbSet;

        public Repository(IEFUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Context.Set<T>();
        }

        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        public virtual T FindBy(TKey id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Add(IEnumerable<T> items)
        {
            _dbSet.AddRange(items);
        }

        public virtual void Update(T entity)
        {
            _unitOfWork.Context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Delete(TKey id)
        {
            _dbSet.Remove(FindBy(id));
        }

        public void Delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
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
        
        #region [    Metodos protegidos    ]

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

        protected IQueryable<T> Query()
        {
            return _dbSet.AsQueryable<T>();
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
        public Repository(IEFUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
