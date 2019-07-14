using Ludiq.Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [UnitTitle("As [Live]")]
    [UnitCategory("Logic")]
    public class AsUnit : LiveUnit
    {
        [DoNotSerialize]
        public ValueInput value;
        [DoNotSerialize]
        public ValueInput type;
        [DoNotSerialize]
        public ValueOutput result;

        protected override void Definition()
        {
            base.Definition();

            value = ValueInput<object>("value");
            type = ValueInput("type", typeof(UnityEngine.Object));
            result = ValueOutput<object>("result", new System.Func<Flow, object>((flow) =>
            {
                var output = flow.GetValue<object>(value).ConvertTo(flow.GetValue<System.Type>(type));
                return output;
            }));
        }
    }
}