

using CodeGenerator.Templating.Interfaces;
using DotLiquid;

namespace CodeGenerator.Templating.DotLiquid
{
    public class StringGenerator : IStringGenerator
    {
        public string Generate(object entity, string stringTemplate)
        {
            var template = Template.Parse(stringTemplate);
            string _result = template.Render(Hash.FromAnonymousObject(new { element = entity }));

            return _result;
        }
    }
}
