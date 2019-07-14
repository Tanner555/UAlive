using System;
using System.Collections.Generic;
using Ludiq;
using Ludiq.OdinSerializer;

namespace Lasm.UAlive
{
    [Serializable][Inspectable]
    public class Contract : LudiqScriptableObject
    {
        [OdinSerialize]
        [Inspectable]
        public List<StaticVariable> variables = new List<StaticVariable>();
        [OdinSerialize]
        [Inspectable]
        public List<StaticMethod> methods = new List<StaticMethod>();
    }

}