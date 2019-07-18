using Ludiq.Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [UnitCategory("Compiler")]
    [UnitTitle("Compiler Branch [Live]")]
    public class CompilerBranchUnit : LiveUnit
    {
        [DoNotSerialize]
        public ValueInput input;
    }
}