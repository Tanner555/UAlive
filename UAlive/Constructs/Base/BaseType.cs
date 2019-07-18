using Ludiq;
using System;

namespace Lasm.UAlive
{
    [Inspectable][Serializable]
    public class BaseType
    {
        [Serialize]
        [InspectorWide]
        [InspectorLabel(null)]
        [TypeFilter(Value = false, Sealed = false, Primitives = false, Generic = true, 
            Classes = true, Enums = false, Structs = false, Interfaces = false, Object = true, OpenConstructedGeneric = true, 
            GenericParameterAttributeFlags = System.Reflection.GenericParameterAttributes.Covariant)]
        public Type type = typeof(object);
    }

}