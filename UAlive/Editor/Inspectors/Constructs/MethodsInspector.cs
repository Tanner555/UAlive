using UnityEngine;
using UnityEditor;
using Ludiq;
using Ludiq.ReorderableList;
using Lasm.UAlive;
using System.Collections.Generic;

[assembly: RegisterInspector(typeof(Methods), typeof(MethodsInspector))]

namespace Lasm.UAlive
{
    public class MethodsInspector : OnGUIBlockInspector
    {
        protected Accessor owner => accessor["owner"];
        protected Accessor parameters => accessor["parameters"];
        protected Accessor scope => accessor["scope"];
        protected Accessor modifier => accessor["modifier"];
        protected Methods declaration => (Methods)accessor.value;
        protected ObjectMacro type => (ObjectMacro)owner.value;
        protected Accessor parent => accessor.parent;

        public MethodsInspector(Accessor accessor) : base(accessor)
        {
        }

        protected override float GetHeight(float width, GUIContent label)
        {
            return accessor.Count * 92;
        }

        protected sealed override void OnGUIBlock(Rect position, GUIContent label)
        {
            var lastPosition = position;
            lastPosition.height = 90;
            lastPosition.width -= 15;
            lastPosition.x += 9;
            lastPosition.y += 2;

            if (accessor.Count == 0)
            {
                GUI.Box(new Rect(lastPosition.x, lastPosition.y, lastPosition.width + 6, 32), GUIContent.none);
                if (GUI.Button(new Rect(lastPosition.x + 5, lastPosition.y + 5, lastPosition.width - 5 , 22), "+ Add Method"))
                {
                    ((Methods)accessor.value).Add(null);
                }
            }

            for (int i = 0; i < accessor.Count; i++)
            {
                GUI.Box(lastPosition, GUIContent.none);

                LudiqGUI.Editor(accessor[i], new Rect(lastPosition.x + 12, lastPosition.y + 5, lastPosition.width - 50, lastPosition.height - 10));

                GUI.Box(new Rect(lastPosition.x + lastPosition.width - 32, lastPosition.y, 38, lastPosition.height), GUIContent.none);

                if (GUI.Button(new Rect(lastPosition.x + lastPosition.width - 25, lastPosition.y + 5, 24, 18), new GUIContent(UAliveResources.removeBlack)))
                {
                    var _dec = accessor[i].value as Method;
                    ((List<Method>)accessor.value).Remove(_dec);
                    AssetDatabase.RemoveObjectFromAsset(_dec);
                    AssetDatabase.SaveAssets();
                }

                EditorGUI.BeginDisabledGroup(i == 0);
                if (GUI.Button(new Rect(lastPosition.x + lastPosition.width - 25, lastPosition.y + (lastPosition.height / 2) - 16, 24, 18), new GUIContent(UAliveResources.arrowUpBlack)))
                {
                    var list = ((List<Method>)accessor.value);
                    var item = list[i];
                    list.Remove(item);
                    list.Insert(i - 1, item);
                }
                EditorGUI.EndDisabledGroup();

                EditorGUI.BeginDisabledGroup(i + 1 == accessor.Count);
                if (GUI.Button(new Rect(lastPosition.x + lastPosition.width - 25, lastPosition.y + (lastPosition.height / 2), 24, 18), new GUIContent(UAliveResources.arrowDownBlack)))
                {
                    var list = ((List<Method>)accessor.value);
                    var item = list[i];
                    list.Remove(item);
                    list.Insert(i + 1, item);
                }
                EditorGUI.EndDisabledGroup();


                if (GUI.Button(new Rect(lastPosition.x + lastPosition.width - 25, lastPosition.y + lastPosition.height - 22, 24, 18), new GUIContent(UAliveResources.addBlack)))
                {
                    ((List<Method>)accessor.value).Insert(i + 1, null);
                }

                lastPosition.y += lastPosition.height;
            }
        }

    }
}