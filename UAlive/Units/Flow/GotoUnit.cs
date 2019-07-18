using Ludiq.Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [UnitTitle("Goto [Live]")]
    [UnitCategory("Flow")]
    public class GotoUnit : LiveUnit
    {
        [Serialize]
        [UnitHeaderInspectable(null)]
        [Inspectable]
        public string label;

        [DoNotSerialize]
        [UnitPortLabelHidden]
        public ControlInput enter;

        [DoNotSerialize]
        [UnitPortLabelHidden]
        public ControlOutput exit;

        protected override void DefinePorts()
        {
            exit = ControlOutput("exit");
            enter = ControlInput("enter", new System.Func<Flow, ControlOutput>((flow) =>
            {
                LabelUnit.GotoLabel(flow.stack.ToReference(), label, graph);
                return exit;
            }));
        }
    }
}