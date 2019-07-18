using Ludiq.Bolt;
using Ludiq;
using System.Collections.Generic;

namespace Lasm.UAlive
{
    [SpecialUnit]
    [UnitTitle("Constructor [Live]")]
    [TypeIcon(typeof(Flow))]
    public class ConstructorUnit : EntryUnit
    {
        [Serialize]
        public Constructor declaration;
        [DoNotSerialize]
        public List<ValueOutput> parameters = new List<ValueOutput>();
        [Inspectable]
        [InspectorLabel("Parameters")]
        public Dictionary<string, System.Type> _parameters = new Dictionary<string, System.Type>();
        public event System.Action UpdateGraphs = new System.Action(() => { });

        protected override void DefinePorts()
        {
            base.DefinePorts();

            parameters.Clear();

            if (_parameters != null)
            {
                var count = _parameters.Count;
                var keys = _parameters.Keys.ToListPooled();

                for (int i = 0; i < count; i++)
                {
                    parameters.Add(ValueOutput(_parameters[keys[i]], keys[i]));
                }
            }

            if (graph != null) UpdateGraphs();
        }
    }
}