using Ludiq.Bolt;
using System.Collections.Generic;

namespace Lasm.UAlive
{
    public interface ICodeGenerator
    {
        IUnit unit { get; set; }
        string GenerateControlInput(ControlInput input, int indent);
        string GenerateValueOutput(ValueOutput output, int indent);
        List<string> usingStatements { get; }
    }
}
