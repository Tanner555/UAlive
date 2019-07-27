using Lasm.UAlive;
using Ludiq.Bolt;

[assembly: RegisterCodeGenerator(typeof(ContinueUnit), typeof(ContinueLiveGenerator))]

namespace Lasm.UAlive
{
    public class ContinueLiveGenerator : LiveUnitGenerator<ContinueUnit>
    {
        public ContinueLiveGenerator(ContinueUnit unit) : base(unit)
        {

        }

        public override string GenerateControlInput(ControlInput input, int indent)
        {
            return "continue;";
        }

        public override string GenerateValueOutput(ValueOutput output, int indent)
        {
            return string.Empty;
        }
    }
}
