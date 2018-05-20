using Plain.Data.Dapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plain.Data.Dapper.Repository
{
    public class UnitOfWork : IDapperUnitOfWork
    {
        protected IDbConnection _dbConnection;

        protected IDbTransaction _dbTransaction;

        private void CloseConnection()
        {
            if (_dbConnection != null && _dbConnection.State == ConnectionState.Open)
            {
                Commit();
                _dbConnection.Close();
                _dbConnection.Dispose();
                _dbConnection = null;
            }
        }

        public UnitOfWork(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public virtual IDbConnection DbConnection
        {
            get
            {
                if (_dbConnection.State != ConnectionState.Open)
                {
                    _dbConnection.Open();
                }

                if (_dbTransaction == null)
                {
                   _dbTransaction = _dbConnection.BeginTransaction();
                }
                return _dbConnection;
            }
        }

        public virtual IDbTransaction DbTransaction
        {
            get
            {
                if (_dbTransaction == null)
                {
                    _dbTransaction = _dbConnection.BeginTransaction();
                }
                return _dbTransaction;
            }
        }

        public void Commit()
        {
            if (_dbTransaction != null)
            {
                _dbTransaction.Commit();
                _dbTransaction = null;
            }
        }

        public void Rollback()
        {
            if (_dbTransaction != null)
            {
                _dbTransaction.Rollback();
                _dbTransaction = null;
            }
        }

        public void Dispose()
        {
            CloseConnection();
        }
    }
}
