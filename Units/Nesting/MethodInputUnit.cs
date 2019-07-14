using Ludiq.Bolt;
using Ludiq;
using System.Collections.Generic;
using Lasm.Reflection;
using System.Linq;

namespace Lasm.UAlive
{
    [SpecialUnit]
    [UnitCategory("Nesting")]
    [UnitTitle("Method [Live]")]
    [TypeIcon(typeof(Flow))]
    public class MethodInputUnit : EntryUnit
    {
        [Serialize]
        public Method declaration;
        [DoNotSerialize]
        public List<ValueOutput> parameters = new List<ValueOutput>();
        [Inspectable][InspectorLabel("Parameters")]
        public Dictionary<string, System.Type> _parameters = new Dictionary<string, System.Type>();
        public object[] parameterValues;

        public event System.Action UpdateGraphs = new System.Action(()=> {
            
        });

        protected override void Definition()
        {
            base.Definition();

            parameters.Clear();

            if (_parameters != null)
            {
                var count = _parameters.Count;
                var keys = _parameters.Keys.ToListPooled();
                  
                for (int i = 0; i < count; i++)
                {
                    parameters.Add(ValueOutput(_parameters[keys[i]], keys[i], (flow)=> { UnityEngine.Debug.Log(i - 1); return parameterValues[i - 1]; }));
                }
            }

            UpdateGraphs();
        }

        public override void AfterAdd()
        {
            base.AfterAdd();

            if (graph != null) graph.onChanged += Define;
        }

        public override void BeforeRemove()
        {
            if (graph != null) graph.onChanged -= Define;

            base.BeforeRemove();
        }
    }
}