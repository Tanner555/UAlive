using Ludiq;
using System;
using Ludiq.OdinSerializer;

namespace Lasm.UAlive
{
    [Inspectable][Serializable]
    public class BaseType
    {
        [OdinSerialize]
        [InspectorWide]
        [InspectorLabel(null)]
        [TypeFilter(Value = false, Sealed = false, Primitives = false, Generic = true, 
            Classes = true, Enums = false, Structs = false, Interfaces = false, Object = true, OpenConstructedGeneric = true, 
            GenericParameterAttributeFlags = System.Reflection.GenericParameterAttributes.Covariant)]
        public Type type = typeof(object);
    }

}