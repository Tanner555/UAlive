using Ludiq.Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [UnitTitle("Lock [Live]")]
    [UnitCategory("Flow")]
    public class LockUnit : LiveUnit
    {
        [DoNotSerialize]
        public ControlInput enter;
        [DoNotSerialize]
        public ControlOutput locked;
        [DoNotSerialize]
        public ControlOutput exit;
        [DoNotSerialize]
        public ValueInput lockObject;

        protected override void Definition()
        {
            base.Definition();

            enter = ControlInput("enter", new System.Func<Flow, ControlOutput>((flow) =>
            {
                lock (flow.GetValue(lockObject))
                {
                    var _flow = Flow.New(flow.stack.AsReference(), true);
                    _flow.Invoke(locked);
                }
                return exit;
            }));

            lockObject = ValueInput<object>("lockObject");

            locked = ControlOutput("locked");
        }

    }
}