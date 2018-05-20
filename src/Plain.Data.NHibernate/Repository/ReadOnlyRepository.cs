using Plain.Data.NHibernate.Interfaces;
using Plain.Infrastructure.Domain;
using Plain.Infrastructure.Interfaces;
using Plain.Infrastructure.Interfaces.Data;
using Plain.Infrastructure.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Plain.Data.NHibernate.Repository
{
    public class ReadOnlyRepository<TKey, T> : IReadOnlyRepository<TKey, T> where T : class, IEntityKey<TKey>
    {
        protected readonly INHUnitOfWork _unitOfWork;

        public ReadOnlyRepository(INHUnitOfWork unitOfWork)
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

    public class ReadOnlyRepository<T> : ReadOnlyRepository<int, T>, IReadOnlyRepository<T> where T : class, IEntityKey<int>
    {
        public ReadOnlyRepository(INHUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
