using Ludiq.OdinSerializer;
using Ludiq;

namespace Lasm.UAlive
{
    [System.Serializable]
    public sealed class TargetObject
    { 
        [OdinSerialize]
        [InspectorWide][InspectorLabel(null)]
        public ObjectMacro type;
        [OdinSerialize]
        [Inspectable]
        [InspectorWide]
        public InstanceTarget instanceType;
        [OdinSerialize]
        public DeclarationType targetUnitType;
        [OdinSerialize]
        public object selectedObject;
    }
}