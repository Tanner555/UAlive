using Ludiq;
using Ludiq.Bolt;

namespace Lasm.UAlive
{
    [UnitTitle("Label [Live]")]
    [UnitCategory("Events")]
    public class LabelUnit : LiveEventUnit<LabelArgs>
    {
        public override string hookName => "Label";

        [Serialize][UnitHeaderInspectable(null)][Inspectable]
        public string label;

        [DoNotSerialize][UnitPortLabelHidden][UnitPrimaryPort]
        public ControlInput enter;
        [DoNotSerialize][UnitPortLabelHidden]
        public ControlOutput exit;

        protected override void Definition()
        {
            base.Definition();

            enter = ControlInput("enter", new System.Func<Flow, ControlOutput>((flow) =>
            {
                return trigger;
            }));
        }

        override protected bool ShouldTrigger(Flow flow, LabelArgs args)
        {
            return (args.label == label) && (graph == args.graph);
        }

        public static void GotoLabel(GraphReference reference, string label, FlowGraph graph)
        {
            reference.TriggerEventHandler<LabelArgs>(hook => hook.name == "Label", new LabelArgs(label, graph), parent => true, true);
        }
    }

    public struct LabelArgs
    {
        public readonly string label;
        public readonly FlowGraph graph;

        public LabelArgs(string label, FlowGraph graph)
        {
            this.label = label;
            this.graph = graph;
        }
    }
}