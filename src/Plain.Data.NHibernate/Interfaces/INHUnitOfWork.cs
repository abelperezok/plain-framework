using NHibernate;
using Plain.Infrastructure.Interfaces.Data;

namespace Plain.Data.NHibernate.Interfaces
{
    public interface INHUnitOfWork : IUnitOfWork
    {
        ISession Session { get; }
    }
}
