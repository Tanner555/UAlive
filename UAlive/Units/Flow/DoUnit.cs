using Ludiq.Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [UnitTitle("Do While [Live]")]
    [UnitCategory("Flow")]
    public class DoUnit : LiveUnit
    {
        [DoNotSerialize][UnitPortLabelHidden]
        public ValueInput condition;
        [DoNotSerialize]
        [UnitPortLabelHidden]
        public ControlInput enter;
        [DoNotSerialize]
        public ControlOutput exit;
        [DoNotSerialize]
        public ControlOutput @do;

        protected override void Definition()
        {
            base.Definition();

            exit = ControlOutput("exit");
            @do = ControlOutput("do");
            enter = ControlInput("enter", new System.Func<Flow, ControlOutput>((flow) => 
            {
                do
                {
                    var _flow = Flow.New(GraphReference.New(((MethodInputUnit)entry).declaration, false));
                    flow.Invoke(@do);
                }
                while (flow.GetValue<bool>(condition));

                return exit;
            } ));

            condition = ValueInput<bool>("condition");
        }
    }
}