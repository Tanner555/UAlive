using System;
using System.Collections.Generic;
using Ludiq;
using Ludiq.OdinSerializer;

namespace Lasm.UAlive
{
    [Serializable]
    [Inspectable]
    public class Interface
    {
        [TypeFilter(Classes = false, Enums = false, Structs = false,
            Interfaces = true, Delegates = false, Public = true,
            Value = false, Static = false, Primitives = false,
            Obsolete = false)]
        [OdinSerialize]
        [Inspectable]
        [InspectorLabel(null)]
        public Type type;
    }
}
