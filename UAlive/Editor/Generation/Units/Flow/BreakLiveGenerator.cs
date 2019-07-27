using Lasm.UAlive;
using Ludiq.Bolt;

[assembly: RegisterCodeGenerator(typeof(Break), typeof(BreakLiveGenerator))]

namespace Lasm.UAlive
{
    public class BreakLiveGenerator : LiveUnitGenerator<Break>
    {
        public BreakLiveGenerator(Break unit) : base(unit)
        {

        }

        public override string GenerateControlInput(ControlInput input, int indent)
        {
            return "break;";
        }

        public override string GenerateValueOutput(ValueOutput output, int indent)
        {
            return string.Empty;
        }
    }
}
