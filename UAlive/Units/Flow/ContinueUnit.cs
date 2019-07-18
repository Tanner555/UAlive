using Ludiq.Bolt;
using Ludiq;
using System.Collections;

namespace Lasm.UAlive
{
    [UnitTitle("Continue [Live]")]
    [UnitCategory("Flow")]
    public class ContinueUnit : LiveUnit
    {
        [DoNotSerialize]
        public ValueInput enumerator;
        [UnitPortLabelHidden][DoNotSerialize]
        public ControlInput enter;

        protected override void DefinePorts()
        {
            enumerator = ValueInput<IEnumerator>("enumerator");
            enter = ControlInput("enter", new System.Func<Flow, ControlOutput>((flow) => { flow.GetValue<IEnumerator>(enumerator).MoveNext(); return null; }));
        }
    }
}