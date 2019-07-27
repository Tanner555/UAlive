using Lasm.UAlive;
using Ludiq.Bolt;

[assembly: RegisterCodeGenerator(typeof(GotoUnit), typeof(GotoLiveGenerator))]

namespace Lasm.UAlive
{
    public class GotoLiveGenerator : LiveUnitGenerator<GotoUnit>
    {
        public GotoLiveGenerator(GotoUnit unit) : base(unit)
        {

        }

        public override string GenerateControlInput(ControlInput input, int indent)
        {
            return "goto " + liveUnit.label + ";";
        }

        public override string GenerateValueOutput(ValueOutput output, int indent)
        {
            return string.Empty;
        }
    }
}
