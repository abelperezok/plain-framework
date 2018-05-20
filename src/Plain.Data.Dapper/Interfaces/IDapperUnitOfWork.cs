using Plain.Infrastructure.Interfaces.Data;
using System.Data;

namespace Plain.Data.Dapper.Interfaces
{
    public interface IDapperUnitOfWork : IUnitOfWork
    {
        IDbConnection DbConnection { get; }

        IDbTransaction DbTransaction { get; }
    }
}
