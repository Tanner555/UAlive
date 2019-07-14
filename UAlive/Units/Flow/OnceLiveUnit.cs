using Ludiq;
using Ludiq.Bolt;

namespace Lasm.UAlive
{
    [UnitTitle("Once [Live]")]
    [UnitCategory("Flow")]
    public class OnceLiveUnit : LiveUnit
    {
        private bool onceComplete;
        [DoNotSerialize]
        public ControlInput enter;
        [DoNotSerialize]
        public ControlInput reset;
        [DoNotSerialize]
        public ControlOutput once;
        [DoNotSerialize]
        public ControlOutput after;

        protected override void Definition()
        {
            base.Definition();

            enter = ControlInput("enter", new System.Func<Flow, ControlOutput>((flow) => { if (!onceComplete) { onceComplete = true; return once; } return after; }));
            reset = ControlInput("reset", new System.Func<Flow, ControlOutput>((flow) => { onceComplete = false; return null; }));
            once = ControlOutput("once");
            after = ControlOutput("after");
        }
    }
}