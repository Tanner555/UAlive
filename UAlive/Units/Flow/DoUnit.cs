using Ludiq.Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [UnitTitle("Do [Live]")]
    [UnitCategory("Flow")]
    [TypeIcon(typeof(While))]
    public class DoUnit : LiveUnit
    {
        [DoNotSerialize][UnitPortLabelHidden]
        public ValueInput condition;
        [DoNotSerialize]
        public ValueOutput enumerator;
        [DoNotSerialize]
        [UnitPortLabelHidden]
        public ControlInput enter;
        [DoNotSerialize]
        public ControlOutput next;
        [DoNotSerialize]
        public ControlOutput @do;

        protected override void DefinePorts()
        {
            @do = ControlOutput("do");
            next = ControlOutput("next");
            enter = ControlInput("enter", new System.Func<Flow, ControlOutput>((flow) => 
            {
                do
                {
                    var _flow = Flow.New(GraphReference.New((entry as MethodInputUnit)?.declaration, false));
                    flow.Invoke(@do);
                }
                while (flow.GetValue<bool>(condition));

                return next;
            } ));

            condition = ValueInput<bool>("condition", true);
        }
    }
}