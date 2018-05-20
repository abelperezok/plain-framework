using CodeGenerater.Infrastructure.Domain;
using Plain.Infrastructure.Interfaces.Data;
using Plain.Infrastructure.Services;

namespace CodeGenerator.Infrastructure.Services
{
    public class ItemClassServices : CrudServices<ItemClass>
    {
        public ItemClassServices(IRepository<ItemClass> repository) : base(repository)
        {
        }
    }
}
