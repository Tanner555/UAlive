using Ludiq.Bolt;
using Ludiq;
using System.Collections.Generic;

namespace Lasm.UAlive
{
    [UnitTitle("Switch On Character [Live]")]
    [UnitCategory("Flow")]
    public class SwitchOnCharUnit : SwitchUnit
    {
        [Serialize]
        [Inspectable]
        public List<char> cases = new List<char>();
        [DoNotSerialize]
        [UnitPrimaryPort(showLabel: false)]
        public ValueInput value;
        [UnitPortLabelHidden]
        [DoNotSerialize]
        public List<ControlOutput> _cases = new List<ControlOutput>();

        protected override void DefinePorts()
        {
            base.DefinePorts();

            _cases.Clear();

            value = ValueInput<char>("value");

            for (int i = 0; i < cases.Count; i++)
            {
                _cases.Add(ControlOutput(cases[i].ToString()));
            }
        }

        public override void OnCase(Flow flow)
        {
            var _flow = Flow.New(flow.stack.ToReference(), true);
            var character = (char)flow.GetValue(value);
            _flow.Invoke(controlOutputs[character.ToString()]);
        }
    }
}