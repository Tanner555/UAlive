using Ludiq.Bolt;
using Ludiq;
using System.Collections.Generic;

namespace Lasm.UAlive
{
    [UnitTitle("Switch On String [Live]")]
    [UnitCategory("Flow")]
    public class SwitchOnStringUnit : SwitchUnit
    {
        [Serialize]
        [Inspectable]
        public List<string> cases = new List<string>();
        [DoNotSerialize]
        [UnitPrimaryPort(showLabel: false)]
        public ValueInput value;
        [UnitPortLabelHidden]
        [DoNotSerialize]
        public List<ControlOutput> _cases = new List<ControlOutput>();

        protected override void Definition()
        {
            base.Definition();

            _cases.Clear();

            value = ValueInput<int>("value");

            for (int i = 0; i < cases.Count; i++)
            {
                _cases.Add(ControlOutput(i.ToString()));
            }
        }

        public override void OnCase(Flow flow)
        {
            var _flow = Flow.New(flow.stack.ToReference(), true);
            var stringValue = (string)flow.GetValue(value);
            _flow.Invoke(controlOutputs[stringValue]);
        }
    }
}