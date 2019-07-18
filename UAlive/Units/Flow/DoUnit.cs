using Ludiq.Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [UnitTitle("Do [Live]")]
    [UnitCategory("Flow")]
    [TypeIcon(typeof(While))]
    public class DoUnit : LiveUnit
    {
        [UnitHeaderInspectable("While")]
        [InspectorToggleLeft]
        [Inspectable]
        [Serialize]
        public bool @while;

        [DoNotSerialize][UnitPortLabelHidden]
        public ValueInput condition;
        [DoNotSerialize]
        public ValueOutput enumerator;
        [DoNotSerialize]
        [UnitPortLabelHidden]
        public ControlInput enter;
        [DoNotSerialize]
        public ControlOutput exit;
        [DoNotSerialize]
        public ControlOutput @do;
        [DoNotSerialize]
        public ControlOutput @whileLoop;

        protected override void DefinePorts()
        {
            exit = ControlOutput("exit");
            @do = ControlOutput("do");
            enter = ControlInput("enter", new System.Func<Flow, ControlOutput>((flow) => 
            {
                do
                {
                    var _flow = Flow.New(GraphReference.New((entry as MethodInputUnit)?.declaration, false));
                    flow.Invoke(@do);
                }
                while (flow.GetValue<bool>(condition));
                {
                    if (@while)
                    {
                        var _flow = Flow.New(GraphReference.New((entry as MethodInputUnit)?.declaration, false));
                        flow.Invoke(whileLoop);
                    }
                }

                return exit;
            } ));

            if (@while) whileLoop = ControlOutput("while");

            condition = ValueInput<bool>("condition");
        }
    }
}