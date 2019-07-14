using Ludiq;
using Ludiq.OdinSerializer;
using System;

namespace Lasm.UAlive
{
    [Serializable][Inspectable]
    public class Property
    {
        [Inspectable]
        [OdinSerialize]
        [InspectorToggleLeft]
        public bool get = true;

        [Inspectable]
        [OdinSerialize]
        [InspectorToggleLeft]
        public bool set = true;

        [OdinSerialize]
        public bool isProperty;

        [OdinSerialize]
        public ObjectMacro root;

        [OdinSerialize]
        private Getter _getter;
        [Inspectable]
        public Getter getter
        {
            get
            {
                if (_getter == null)
                {
                    _getter = new Getter();
                    _getter.root = this.root;
                }
                return _getter;
            }
        }

        [OdinSerialize]
        private Setter _setter;
        [Inspectable]
        public Setter setter = new Setter();
    }
}