using Microsoft.EntityFrameworkCore;
using Plain.Data.EntityFramework.Interfaces;
using System;

namespace Plain.Data.EntityFramework.Repository
{
    public class UnitOfWork : IEFUnitOfWork
    {
        protected readonly IDbContextFactory _dbContextFactory;

        public UnitOfWork(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public DbContext Context
        {
            get
            {
                return _dbContextFactory.GetCurrentDbContext();
            }
        }

        public virtual void Commit()
        {
            var context = Context;
            if (context != null)
            {
                context.SaveChanges();
            }
        }

        public virtual void Rollback()
        {
            var context = Context;
            if (context != null && context.Database.CurrentTransaction != null)
            {
                context.Database.CurrentTransaction.Rollback();
            }
            
        }

        public virtual void Dispose()
        {
            try
            {
                Commit();
            }
            catch (Exception e)
            {
                Rollback();
                throw e;
            }
            finally
            {
                Context.Dispose();
            }

        }
    }
}
