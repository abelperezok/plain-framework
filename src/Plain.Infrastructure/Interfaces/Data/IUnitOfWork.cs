using System;

namespace Plain.Infrastructure.Interfaces.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        void Rollback();
    }
}
