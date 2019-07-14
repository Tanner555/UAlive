using Ludiq.Bolt;

namespace Lasm.UAlive
{
    public interface ICodeGenerator
    {
        IUnit unit { get; set; }
        string Generate(int indent);
    }
}
