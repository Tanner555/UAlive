using UnityEngine;
using Ludiq;
using Lasm.BoltExtensions;
using Lasm.UAlive;

[assembly: RegisterInspector(typeof(TypeValue), typeof(TypeValueInspector))]

namespace Lasm.BoltExtensions
{
    public class TypeValueInspector : OnGUIBlockInspector
    {
        Accessor typeMeta => accessor["type"];
        Accessor valueMeta => accessor["value"];

        public TypeValueInspector(Accessor accessor) : base(accessor)
        {
        }

        protected override float GetHeight(float width, GUIContent label)
        {
            return height - 12;
        }

        protected override void OnGUIBlock(Rect position, GUIContent label)
        {
            if (accessor != null && accessor.value != null)
            {
                var typePosition = position;
                typePosition.height = 16;

                var valuePosition = typePosition;
                valuePosition.y += 20;

                UnityEditor.EditorGUI.BeginDisabledGroup(!(bool)accessor["canChangeType"].value);
                    LudiqGUI.Inspector(typeMeta, typePosition, GUIContent.none);
                UnityEditor.EditorGUI.EndDisabledGroup();
                
                if (accessor["value"].value != null)
                {
                    LudiqGUI.Inspector(accessor["value"].Cast(typeMeta.value as System.Type), valuePosition, GUIContent.none);
                }
                else
                {
                    if (((System.Type)typeMeta.value).GetType() == typeof(UnityEngine.Object))
                    {
                        accessor["value"].value = UnityEditor.EditorGUI.ObjectField(valuePosition, (UnityEngine.Object)accessor["value"].value, typeMeta.value as System.Type, false);
                    }
                    else
                    {
                        if (typeMeta.value.GetType().CSharpName() == "object")
                        {
                            LudiqGUI.Inspector(accessor["value"].Cast(typeMeta.value as System.Type), valuePosition, GUIContent.none);
                        }
                        else
                        {
                            accessor["value"].value = ((System.Type)typeMeta.value).Instantiate();
                        }
                    }
                }
            }
            else
            {
                accessor.value = new TypeValue { type = typeof(int), value = 0 };
            }

            height = LudiqGUI.GetInspectorHeight(accessor.Inspector(), accessor["value"].Cast(typeMeta.value as System.Type), position.width, GUIContent.none) + 40;
        }
    }
}