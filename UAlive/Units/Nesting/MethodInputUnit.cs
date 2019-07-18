using Ludiq.Bolt;
using Ludiq;
using System.Collections.Generic;
using Lasm.Reflection;
using System.Linq;

namespace Lasm.UAlive
{
    [SpecialUnit]
    [UnitTitle("Method [Live]")]
    [TypeIcon(typeof(Flow))]
    public class MethodInputUnit : EntryUnit
    {
        [Serialize]
        public Method declaration;
        [DoNotSerialize]
        public List<ValueOutput> parameters = new List<ValueOutput>();
        [Inspectable][InspectorLabel("Parameters")][Serialize]
        public Dictionary<string, System.Type> _parameters = new Dictionary<string, System.Type>();
        public object[] parameterValues;

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
                    var index = i;
                    parameters.Add(ValueOutput(_parameters[keys[i]], keys[i], (flow)=> { UnityEngine.Debug.Log(index); return parameterValues[index]; }));
                }
            }
        }
    }
}