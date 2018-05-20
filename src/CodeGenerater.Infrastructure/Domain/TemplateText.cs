using Plain.Infrastructure.Domain;

namespace CodeGenerater.Infrastructure.Domain
{
    public class TemplateText : Entity
    {
        public virtual string Name { get; set; }

        public virtual string Text { get; set; }
    }
}
