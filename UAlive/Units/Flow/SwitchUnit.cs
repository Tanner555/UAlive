using Ludiq.Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [UnitCategory("Flow")]
    [TypeIcon(typeof(Ludiq.Bolt.SwitchUnit<>))]
    public abstract class SwitchUnit : LiveUnit
    {
        [UnitPortLabelHidden]
        [DoNotSerialize]
        public ControlInput enter;
        [DoNotSerialize]
        public ControlOutput exit;

        protected override void DefinePorts()
        {
            enter = ControlInput("enter", new System.Func<Flow, ControlOutput>((flow) =>
            {
                OnCase(flow);
                return exit;
            }));

            exit = ControlOutput("exit");
        }

        public abstract void OnCase(Flow flow);
    }
}