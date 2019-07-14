using Ludiq.Bolt;

namespace Lasm.UAlive
{
    public abstract class CodeGenerator : ICodeGenerator
    {
        public IUnit unit { get; set; }

        public abstract string Generate(int indent);
    }
}
