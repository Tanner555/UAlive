using Ludiq.Bolt;
using Ludiq;
using System.Collections.Generic;

namespace Lasm.UAlive
{
    [UnitTitle("Switch On Enum [Live]")]
    [UnitCategory("Flow")]
    public class SwitchOnEnumUnit : SwitchUnit
    {
        [UnitHeaderInspectable][Serialize]
        [Inspectable][TypeFilter(Classes = false, Enums = true, Structs = false, Interfaces = false, Delegates = false, NonPublic = false, Primitives = false)]
        public System.Type @enum;
        [DoNotSerialize][UnitPrimaryPort(showLabel: false)]
        public ValueInput _enum;
        [UnitPortLabelHidden]
        [DoNotSerialize]
        public List<ControlOutput> cases = new List<ControlOutput>();

        protected override void DefinePorts()
        {
            base.DefinePorts();

            cases.Clear();

            if (@enum != null)
            {
                _enum = ValueInput(@enum, "enum");
                var names = System.Enum.GetNames(@enum);

                for (int i = 0; i < names.Length; i++)
                {
                    cases.Add(ControlOutput(names[i]));
                }
            }
        }

        public override void OnCase(Flow flow)
        {
            var _flow = Flow.New(flow.stack.ToReference(), true);
            var enumValue = flow.GetValue(_enum);
            _flow.Invoke(controlOutputs[enumValue.ToString()]);
        }
    }
}