using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Plain.Data.NHibernate.Interfaces;

namespace Plain.Data.NHibernate.Repository
{
    public class UnitOfWork : INHUnitOfWork
    {
        protected readonly ISessionFactory _sessionFactory;

        public UnitOfWork(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public virtual ISession Session
        {
            get
            {
                ISession session = _sessionFactory.GetCurrentSession();
                if (!session.Transaction.IsActive)
                {
                    session.BeginTransaction();
                }
                return session;
            }
        }

        public virtual void Commit()
        {
            if (Session.Transaction.IsActive)
                Session.Transaction.Commit();
        }

        public virtual void Rollback()
        {
            if (Session.Transaction.IsActive)
            {
                Session.Transaction.Rollback();
            }
        }

        public virtual void SaveChanges()
        {
            Session.Flush();
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
            }
        }
    }
}
