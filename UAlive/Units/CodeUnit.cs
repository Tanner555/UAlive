using Ludiq.Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [UnitCategory("Compiler")]
    [UnitTitle("Code [Live]")]
    public class CodeUnit : LiveUnit
    {
        [DoNotSerialize]
        public ValueInput input;
    }
}