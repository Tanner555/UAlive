using Ludiq;
using Ludiq.OdinSerializer;
using System;

namespace Lasm.UAlive
{
    [Serializable][Inspectable]
    public class Variable
    {
        [OdinSerialize]
        public AccessModifier scope = AccessModifier.Private;

        [OdinSerialize]
        public FieldModifier fieldModifier = FieldModifier.None;

        [OdinSerialize]
        public PropertyModifier propertyModifier = PropertyModifier.None;

        [OdinSerialize]
        public string name = string.Empty;

        [OdinSerialize]
        public bool isReadOnly = false;

        [OdinSerialize]
        public bool asReference = false;

        [OdinSerialize]
        public TypeValue type = new TypeValue() { type = typeof(int) };
        
        [OdinSerialize]
        private Property prop;
        public Property property
        {
            get
            {
                if (prop == null)
                {
                    prop = new Property();
                    prop.root = this.root;
                }

                return prop;
            }

        }

        [OdinSerialize]
        public ObjectMacro root;

        [OdinSerialize]
        public bool isDynamic;

#if UNITY_EDITOR
        public bool isOpen = false;
#endif
    }
}