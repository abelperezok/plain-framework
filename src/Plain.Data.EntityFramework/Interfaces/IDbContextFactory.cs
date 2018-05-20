using Microsoft.EntityFrameworkCore;

namespace Plain.Data.EntityFramework.Interfaces
{
    public interface IDbContextFactory
    {
        DbContext GetCurrentDbContext();
    }
}
