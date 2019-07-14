using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Ludiq;

namespace Lasm.UnityEditorUtilities
{
    public partial class UtilityGUILayout
    {

        public static StaticSelection<TType> StaticSelectionList<TType>(List<StaticSelection<TType>> items, ref Vector2 scrollPosition, ref StaticSelection<TType> lastSelection, float itemHeight, float borderThickness, Color active, Color border, GUIStyle backgroundStyle) where TType : class
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal(backgroundStyle);

            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginVertical(backgroundStyle);

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, backgroundStyle, GUILayout.MaxHeight(300));

            StaticSelection<TType> selectionObject = null;

            foreach (StaticSelection<TType> selection in items)
            {
                EditorGUILayout.BeginHorizontal();

                selection.isSelected = false;

                bool isPressed = false;

                SelectionStyleContent content = new SelectionStyleContent();
                content.text = selection.name;
                content.height = itemHeight;
                content.border = border;
                content.active = active;
                content.borderThickness = borderThickness;
                content.background = new Color(active.r - 0.2f, active.g - 0.2f, active.b - 0.2f);
                content.pressed = new Color(active.r - 0.4f, active.g - 0.4f, active.b - 0.4f);

                if (BorderedRectButton(content, ref selection.isActiveSelection, ref selection.isSelected))
                {
                    selectionObject = selection;

                    if (Lasm.UnityUtilities.EventUtilities.isDoubleClickedInEditor(0.3f))
                    {
                        selection.isOpening = true;
                    }
                    else
                    {
                        if (lastSelection == null)
                        {
                            selection.isActiveSelection = true;
                            lastSelection = selection;
                        }
                        else
                        {
                            if (lastSelection != selection)
                            {
                                selection.isActiveSelection = true;
                                lastSelection.isActiveSelection = false;
                                lastSelection = selection;
                            }
                        }
                    }

                    isPressed = true;
                }

                var textStyle = new GUIStyle(EditorStyles.whiteBoldLabel);
                textStyle.alignment = TextAnchor.MiddleCenter;
                textStyle.fontSize = 12;
                GUI.Label(GUILayoutUtility.GetLastRect(), selection.name, textStyle);

                EditorGUILayout.EndHorizontal();

                if (isPressed) break;
            }

            GUILayout.EndScrollView();

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();

            return selectionObject;
        }

        public static StaticSelection<TType> StaticSelectionList<TType>(List<StaticSelection<TType>> items, ref StaticSelection<TType> lastSelection, Color highlight, Color border, ref Vector2 scrollPosition, GUIStyle backgroundStyle) where TType : class
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal(backgroundStyle);

            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginVertical(backgroundStyle);

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, backgroundStyle, GUILayout.MaxHeight(300));

            StaticSelection<TType> selectionObject = null;

            foreach (StaticSelection<TType> selection in items)
            {
                EditorGUILayout.BeginHorizontal();

                selection.isSelected = false;

                SelectionStyleContent content = new SelectionStyleContent();
                content.text = selection.name;
                content.active = highlight;
                content.background = new Color(highlight.r - 0.2f, highlight.g - 0.2f, highlight.b - 0.2f);
                content.pressed = new Color(highlight.r - 0.4f, highlight.g - 0.4f, highlight.b - 0.4f);

                if (UtilityGUILayout.BorderedRectButton(content, ref selection.isActiveSelection, ref selection.isSelected))
                {
                    selectionObject = selection;

                    if (Lasm.UnityUtilities.EventUtilities.isDoubleClickedInEditor(0.3f))
                    {
                        selection.isOpening = true;
                    }

                    if (lastSelection == null)
                    {
                        lastSelection = selection;
                    }
                    else
                    {
                        if (lastSelection != selection)
                        {
                            selection.isActiveSelection = true;
                            lastSelection.isActiveSelection = false;
                            lastSelection = selection;
                        }
                    }

                }

                var textStyle = new GUIStyle(EditorStyles.whiteBoldLabel);
                textStyle.alignment = TextAnchor.MiddleCenter;
                textStyle.fontSize = 12;
                GUI.Label(GUILayoutUtility.GetLastRect(), selection.name, textStyle);

                EditorGUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();

            return selectionObject;
        }

        [Serializable]
        public class StaticSelection<TType> where TType : class
        {
            [Serialize]
            public string name = string.Empty;
            [Serialize]
            public object @object = null;
            [Serialize]
            public bool isOpening = false;
            [Serialize]
            public bool isSelected = false;
            [Serialize]
            public bool isActiveSelection = false;
            [Serialize]
            public Action<TType> selected = null;
            [Serialize]
            public SelectionStyleContent style;

            public StaticSelection(string name, TType @object, Action<TType> selected)
            {
                this.name = name;
                this.@object = @object;
                this.selected = selected;
            }
        }

        [Serializable]
        public class SelectionStyleContent
        {
            [Serialize]
            public string image = null;
            [Serialize]
            public string text = string.Empty;
            [Serialize]
            public string tooltip = string.Empty;
            [Serialize]
            public float height = 18;
            [Serialize]
            public float borderThickness = 1;
            [Serialize]
            public Color background = Color.grey;
            [Serialize]
            public Color border = Color.black;
            [Serialize]
            public Color pressed = new Color(0.4f, 0.4f, 0.4f);
            [Serialize]
            public Color active = new Color(0.65f, 0.65f, 0.65f);
        }
    }
}
