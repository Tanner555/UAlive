using Ludiq.Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [UnitTitle("This [Live]")]
    [TypeIcon(typeof(Self))]
    public class ThisUnit : LiveUnit
    {
        [DoNotSerialize][UnitPortLabelHidden][UnitPrimaryPort]
        public ValueOutput instance;

        protected override void DefinePorts()
        {
            base.DefinePorts();

            instance = ValueOutput<ILiveObject>("instance", new System.Func<Flow, ILiveObject>((flow) => 
            {
                if (entry != null)
                {
                    if (entry.targets != null)
                    {
                        return (ILiveObject)flow.GetLocal("This");
                    }
                }

                return null;
            }));
        }
    }
}