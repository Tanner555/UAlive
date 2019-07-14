using Ludiq.Bolt;
using Ludiq;
using UnityEngine;

namespace Lasm.UAlive
{
    [UnitTitle("Continue [Live]")]
    [UnitCategory("Flow")]
    public class ContinueUnit : LiveUnit
    {
        [UnitPortLabelHidden][DoNotSerialize]
        public ControlInput enter;

        protected override void Definition()
        {
            base.Definition();
            enter = ControlInput("enter", new System.Func<Flow, ControlOutput>((flow) => { return null; }));
        }
    }
}