using CodeGenerater.Infrastructure.Domain;
using Plain.Infrastructure.Interfaces.Data;
using Plain.Infrastructure.Services;

namespace CodeGenerator.Infrastructure.Services
{
    public class TemplateTextServices : CrudServices<TemplateText>
    {
        public TemplateTextServices(IRepository<TemplateText> repository) : base(repository)
        {
        }
    }
}
