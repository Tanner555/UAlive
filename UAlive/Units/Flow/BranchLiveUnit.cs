using Ludiq.Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [UnitTitle("Branch [Live]")]
    [UnitCategory("Flow")][TypeIcon(typeof(Branch))]
    public class BranchLiveUnit : LiveUnit
    {
        [DoNotSerialize]
        public ControlInput enter;
        [DoNotSerialize]
        public ControlOutput @true;
        [DoNotSerialize]
        public ControlOutput @false;
        [DoNotSerialize]
        public ControlOutput next;
        [DoNotSerialize]
        public ValueInput condition;
        
        protected override void Definition()
        {
            @true = ControlOutput("true");
            @false = ControlOutput("false");
            next = ControlOutput("next");
            enter = ControlInput("enter", (flow) => 
            {
                if (flow.GetValue<bool>(condition)) { flow.Invoke(@true); } else { flow.Invoke(@false); }
                return next;
            });

            condition = ValueInput<bool>("condition");

            Requirement(condition, enter);
            Succession(enter, @true);
            Succession(enter, @false);
            Succession(enter, next);
        }
    }
}