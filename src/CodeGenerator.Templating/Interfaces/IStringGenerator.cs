
namespace CodeGenerator.Templating.Interfaces
{
    public interface IStringGenerator
    {
        string Generate(object entity, string stringTemplate);
    }
}
