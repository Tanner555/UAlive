using Ludiq.Bolt;
using System.Collections.Generic;

namespace Lasm.UAlive
{
    public interface ICodeGenerator
    {
        IUnit unit { get; set; }
        string Generate(int indent);
        List<string> usingStatements { get; }
    }
}
