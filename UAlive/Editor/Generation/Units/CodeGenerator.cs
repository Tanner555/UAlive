using Ludiq.Bolt;
using System.Collections.Generic;

namespace Lasm.UAlive
{
    public abstract class CodeGenerator : ICodeGenerator
    {
        public IUnit unit { get; set; }

        public virtual List<string> usingStatements => new List<string>();

        public abstract string GenerateControlInput(ControlInput input, int indent);

        public abstract string GenerateValueOutput(ValueOutput output, int indent);
    }
}
