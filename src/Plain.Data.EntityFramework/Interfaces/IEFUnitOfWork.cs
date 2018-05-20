using Microsoft.EntityFrameworkCore;
using Plain.Infrastructure.Interfaces.Data;

namespace Plain.Data.EntityFramework.Interfaces
{
    public interface IEFUnitOfWork : IUnitOfWork
    {
        DbContext Context { get; }
    }
}
