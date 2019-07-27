using Lasm.UAlive;
using Ludiq.Bolt;

[assembly: RegisterCodeGenerator(typeof(LabelUnit), typeof(LabelLiveGenerator))]

namespace Lasm.UAlive
{
    public class LabelLiveGenerator : LiveUnitGenerator<LabelUnit>
    {
        public LabelLiveGenerator(LabelUnit unit) : base(unit)
        {

        }

        public override string GenerateControlInput(ControlInput input, int indent)
        {
            return liveUnit.label + ":" + "\n";
        }

        public override string GenerateValueOutput(ValueOutput output, int indent)
        {
            return string.Empty;
        }
    }
}
