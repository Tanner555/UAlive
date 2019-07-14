using UnityEngine;
using UnityEditor;
using Ludiq;
using Lasm.UnityUtilities;
using Lasm.UnityEditorUtilities;
using System;
using System.Collections.Generic;

namespace Lasm.UAlive
{
    public static class UAliveGUI
    {
        public static Rect DrawSection(Rect position, Accessor _accessor, Accessor @base, Event e, string title, Texture2D icon, int heightModifier, bool togglable, int items, Accessor isOpen, bool isEditor = false, List<Rect> untouchableAreas = null)
        {
            var accessorHeight = isEditor ? _accessor.Editor().GetCachedHeight(position.width, GUIContent.none, @base.Editor()) : _accessor.Inspector().GetCachedHeight(position.width, GUIContent.none, @base.Inspector());
            var sectionRect = position;
            sectionRect.y = position.height + position.y - 2;
            sectionRect.height = (bool)isOpen.value ? accessorHeight + heightModifier : heightModifier;

            // Draw a rectangle that contains a border and is light grey. Color usage for differentiating sections.
            UtilityGUI.Container(sectionRect, Color.black, 0.6f.Grey(), 1, 10, (rootContainer) =>
            {
                UtilityGUI.Container(rootContainer, Color.black, 0.8f.Grey(), 1, 4, (container) =>
                {
                    var canTouch = true;

                    if (untouchableAreas != null)
                    {
                        foreach (Rect rect in untouchableAreas)
                        {
                            if (rect.Contains(e.mousePosition))
                            {
                                canTouch = false;
                                break;
                            }
                        }
                    }

                    if (canTouch && new Rect(container.x, container.y, container.width, 24).Contains(e.mousePosition))
                    {
                        if (e.type == EventType.MouseDown && e.button == 0) isOpen.value = !(bool)isOpen.value;
                    }

                    UtilityGUI.BorderRect(new Rect(rootContainer.x, rootContainer.y, rootContainer.width, 24), 1, 0.3f.Grey(), Color.black, UtilityGUI.BorderDrawPlacement.Inside);
                    Graphics.DrawTexture(new Rect(rootContainer.x + 5, rootContainer.y + 4, 16, 16), icon);
                    EditorGUI.LabelField(new Rect(container.x + 20, container.y, container.width - 26, 16), title, new GUIStyle(EditorStyles.boldLabel) { normal = new GUIStyleState() { textColor = Color.white } });

                    var accessorRect = new Rect(container.x, container.y + 24, container.width, accessorHeight);

                    UtilityGUI.BorderRect(new Rect(rootContainer.x + 100, rootContainer.y + 3, 20, 18), 1, 0.4f.Grey(), Color.black, UtilityGUI.BorderDrawPlacement.Inside);
                    EditorGUI.LabelField(new Rect(rootContainer.x + 100, rootContainer.y + 3, 20, 18), items.ToString(), new GUIStyle(EditorStyles.whiteBoldLabel) { alignment = TextAnchor.MiddleCenter, normal = new GUIStyleState() { textColor = 0.85f.Grey() } });

                    if (togglable)
                    {
                        if ((bool)isOpen.value) LudiqGUI.Inspector(_accessor, accessorRect, GUIContent.none);
                        Texture2D arrow = (bool)isOpen.value ? UAliveResources.arrowDownWhite : UAliveResources.arrowRightWhite;
                        Graphics.DrawTexture(new Rect(container.x + container.width - 14, rootContainer.y + 6, 10, 10), arrow);
                        return;
                    }

                    if (!isEditor) { LudiqGUI.Inspector(_accessor, accessorRect, GUIContent.none); } else { LudiqGUI.Editor(_accessor, accessorRect); }
                });
            });

            return sectionRect;
        }

        public static Rect DrawSection(Rect position, Accessor _accessor, Accessor @base, Event e, string title, Texture2D icon, int heightModifier, bool togglable, Accessor isOpen)
        {
            var accessorHeight = _accessor.Inspector().GetCachedHeight(position.width, GUIContent.none, @base.Inspector());
            var sectionRect = position;
            sectionRect.y = position.height + position.y - 2;
            sectionRect.height = (bool)isOpen.value ? accessorHeight + heightModifier : heightModifier;

            // Draw a rectangle that contains a border and is light grey. Color usage for differentiating sections.
            UtilityGUI.Container(sectionRect, Color.black, 0.6f.Grey(), 1, 10, (rootContainer) =>
            {
                UtilityGUI.Container(rootContainer, Color.black, 0.8f.Grey(), 1, 4, (container) =>
                {
                    if (new Rect(container.x, container.y, container.width, 24).Contains(e.mousePosition))
                    {
                        if (e.type == EventType.MouseDown && e.button == 0) isOpen.value = !(bool)isOpen.value;
                    }

                    UtilityGUI.BorderRect(new Rect(rootContainer.x, rootContainer.y, rootContainer.width, 24), 1, 0.3f.Grey(), Color.black, UtilityGUI.BorderDrawPlacement.Inside);
                    Graphics.DrawTexture(new Rect(rootContainer.x + 5, rootContainer.y + 4, 16, 16), icon);
                    EditorGUI.LabelField(new Rect(container.x + 20, container.y, container.width - 26, 16), title, new GUIStyle(EditorStyles.boldLabel) { normal = new GUIStyleState() { textColor = Color.white } });

                    var accessorRect = new Rect(container.x, container.y + 24, container.width, accessorHeight);

                    if (togglable)
                    {
                        if ((bool)isOpen.value) LudiqGUI.Inspector(_accessor, accessorRect, GUIContent.none);
                        Texture2D arrow = (bool)isOpen.value ? UAliveResources.arrowDownWhite : UAliveResources.arrowRightWhite;
                        Graphics.DrawTexture(new Rect(container.x + container.width - 14, rootContainer.y + 6, 10, 10), arrow);
                        return;
                    }

                    LudiqGUI.Inspector(_accessor, accessorRect, GUIContent.none);
                });
            });

            return sectionRect;
        }

        public static Rect NestedSection(Rect position, Event e, string title, Texture2D icon, int heightModifier, Action<Rect> draw, Accessor conditional = null)
        {
            var sectionRect = position;
            sectionRect.y =  position.y;
            sectionRect.height = position.height - 24 + heightModifier;
            Rect output = new Rect();

            // Draw a rectangle that contains a border and is light grey. Color usage for differentiating sections.
            UtilityGUI.Container(sectionRect, Color.black, 0.6f.Grey(), 1, 0, (rootContainer) =>
            {
                UtilityGUI.Container(rootContainer, Color.black, 0.8f.Grey(), 1, 4, (container) =>
                {
                    var header = new Rect(rootContainer.x, rootContainer.y, rootContainer.width, 24);

                    UtilityGUI.BorderRect(new Rect(rootContainer.x, rootContainer.y, rootContainer.width, 24), 1, 0.3f.Grey(), Color.black, UtilityGUI.BorderDrawPlacement.Inside);
                    Graphics.DrawTexture(new Rect(rootContainer.x + 5, rootContainer.y + 4, 16, 16), icon);
                    EditorGUI.LabelField(new Rect(rootContainer.x + 20 + (conditional != null ? 20 : 4), rootContainer.y + 4, container.width - 26, 16), title, EditorStyles.whiteBoldLabel);

                    var accessorRect = new Rect(container.x + 30, container.y + 42, container.width - 45, container.height);

                    if (conditional != null)
                    {
                        LudiqGUI.Inspector(conditional, new Rect(container.x + 22, container.y, 16, 16), GUIContent.none);
                    }

                    GUI.Button(header, GUIContent.none, GUIStyle.none);

                    output = accessorRect;

                    draw(output);
                });
            });

            return output;
        }
    }
}
