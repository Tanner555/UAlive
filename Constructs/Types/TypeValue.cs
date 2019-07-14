using System;
using UnityEngine;
using Lasm.Reflection;
using Ludiq;
using Ludiq.OdinSerializer;

namespace Lasm.UAlive
{
    [Serializable]
    public class TypeValue
    {
#if UNITY_EDITOR
        [OdinSerialize]
        public bool canChangeType = true;
#endif
        [OdinSerialize]
        public Type type = typeof(int);

        [InspectorToggleLeft]
        public bool hasDefault;

        [OdinSerialize]
        public object _value = 0;
        [InspectorLabel(null)]
        [InspectorObjectType("type")]
        public object value
        {
            get
            {
                CreateType();
                return _value;
            }

            set
            {
                _value = value;
                CreateType();
            }
        }

        public void CreateType()
        {
            if (_value != null)
            {
                if (_value.GetType() != type)
                {
                    if (type.InheritsType(typeof(UnityEngine.Object)))
                    {
                        _value = null;
                    }
                    else
                    {
                        _value = type.TryInstantiate();
                    }
                }
            }
            else
            {
                if (!type.InheritsType(typeof(UnityEngine.Object)))
                {
                    _value = type.TryInstantiate();
                }
            }
        }
    }
}