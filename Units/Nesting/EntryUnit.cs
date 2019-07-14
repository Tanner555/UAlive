using Ludiq;
using Ludiq.Bolt;
using System.Collections.Generic;
using System.Collections;
using System;
using Ludiq.OdinSerializer;
using System.Linq;
using Lasm.Reflection;

namespace Lasm.UAlive
{
    [SpecialUnit]
    [UnitCategory("Nesting/Live")]
    public abstract class EntryUnit : LiveUnit
    {
        [DoNotSerialize][UnitPortLabelHidden][UnitPrimaryPort]
        public ControlOutput trigger;
        [OdinSerialize]
        public ObjectMacro macro;
        [DoNotSerialize]
        public List<MethodInstance> targets = new List<MethodInstance>();
        
        protected override void Definition()
        {
            trigger = ControlOutput("trigger").External();
        }
    }
}