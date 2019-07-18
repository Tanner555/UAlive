using Ludiq.Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [UnitTitle("Is [Live]")]
    [UnitCategory("Logic")]
    public class IsUnit : LiveUnit
    {
        [DoNotSerialize]
        public ValueInput value;
        [DoNotSerialize]
        public ValueInput type;
        [DoNotSerialize]
        public ValueOutput result;

        protected override void DefinePorts()
        {
            base.DefinePorts();

            value = ValueInput<object>("value");
            type = ValueInput("type", typeof(UnityEngine.Object));
            result = ValueOutput<bool>("result", new System.Func<Flow, bool>((flow) =>
            {
                return flow.GetValue<object>(value).IsConvertibleTo(flow.GetValue<System.Type>(type), false);
            }));
        }
    }
}