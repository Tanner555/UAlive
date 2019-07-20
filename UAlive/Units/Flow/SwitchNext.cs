using Ludiq.Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [UnitCategory("Flow")]
    [TypeIcon(typeof(Ludiq.Bolt.SwitchUnit<>))]
    public abstract class SwitchNext : LiveUnit
    {
        [UnitPortLabelHidden]
        [DoNotSerialize]
        public ControlInput enter;
        [DoNotSerialize]
        public ControlOutput next;

        protected override void DefinePorts()
        {
            enter = ControlInput("enter", new System.Func<Flow, ControlOutput>((flow) =>
            {
                OnCase(flow);
                return next;
            }));

            next = ControlOutput("next");
        }

        public abstract void OnCase(Flow flow);
    }
}