using CodeGenerater.Infrastructure.Domain;
using Plain.Infrastructure.Interfaces.Data;
using Plain.Infrastructure.Services;

namespace CodeGenerator.Infrastructure.Services
{
    public class PropertyServices : CrudServices<Property>
    {
        public PropertyServices(IRepository<Property> repository) : base(repository)
        {
        }
    }
}
