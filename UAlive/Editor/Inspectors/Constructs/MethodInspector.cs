using UnityEngine;
using UnityEditor;
using Ludiq;
using System.Collections.Generic;
using Lasm.UAlive;
using Lasm.UnityEditorUtilities;
using System.Linq;

[assembly: RegisterEditor(typeof(Method), typeof(MethodInspector))]

namespace Lasm.UAlive
{
    public class MethodInspector : OnGUIBlockInspector
    {
        protected Accessor owner => accessor["owner"];
        protected Accessor returns => accessor["returns"];
        protected Accessor parameters => accessor["parameters"];
        protected Accessor name => accessor["name"];
        protected Accessor scope => accessor["scope"];
        protected Accessor entry => accessor["entry"];
        protected Accessor modifier => accessor["modifier"];
        protected Method declaration => (Method)accessor.value;
        protected ObjectMacro type => (ObjectMacro)owner.value;
        protected Accessor parent => accessor.parent;

        public MethodInspector(Accessor accessor) : base(accessor)
        {

        }

        protected override float GetHeight(float width, GUIContent label)
        {
            return 60;
        }
        
        protected sealed override void OnGUIBlock(Rect position, GUIContent label)
        {
            y = 0;

            Contents(position);

            if (declaration.ShouldChange())
            {
                var returnUnits = declaration.graph.units.OfType<ReturnUnit>();

                foreach (ReturnUnit returnUnit in returnUnits)
                {
                    returnUnit.returnType = declaration.returns;
                }
            }

            height = y;
        }

        /// <summary>
        /// The surrounding box of all contents of a method.
        /// </summary>
        /// <param name="position"></param>
        private void Contents(Rect position)
        {
            var rect = new Rect(position.x + 1, position.y + 4, position.width + 2, GetHeight(position.width, GUIContent.none) + 30);
            var nameRect = Name(rect);
            SetGraphTitle();
            var editRect = EditGraph(nameRect);
            var returns = Returns(editRect);
        }

        /// <summary>
        /// Sets the title of a graph, based on the macro name.
        /// </summary>
        private void SetGraphTitle()
        {
            if (declaration.graph != null) declaration.graph.title = (string)name.value;
        }

        /// <summary>
        /// GUI for drawing the name field and its label.
        /// </summary>
        /// <param name="previous"></param>
        private Rect Name(Rect previous)
        {
            var padding = 4f;
            var doublePadding = padding * 2;

            var nameLabelSize = GUI.skin.label.CalcSize(new GUIContent("Name"));

            var nameRect = previous;
            nameRect.height = 18;
            nameRect.width -= nameLabelSize.x + (doublePadding * 2);
            nameRect.y += 2;
            nameRect.x += nameLabelSize.x + doublePadding;

            var labelRect = nameRect;
            labelRect.width = nameLabelSize.x + doublePadding;
            labelRect.x = previous.x + 4;

            y += 20;

            LudiqGUI.Inspector(name, nameRect, GUIContent.none);
            GUI.Label(labelRect, new GUIContent("Name"));

            return nameRect;
        }

        /// <summary>
        /// Draws a button to click and edit the graph of this method.
        /// </summary>
        /// <param name="previous"></param>
        private Rect EditGraph(Rect previous)
        {
            var padding = 4f;
            var doublePadding = padding * 2;

            var returnLabelSize = GUI.skin.label.CalcSize(new GUIContent("Returns"));

            var buttonRect = previous;
            buttonRect.y += 24;
            buttonRect.x += padding;
            buttonRect.width -= doublePadding;
            buttonRect.height = 18;

            if (GUI.Button(buttonRect, "Edit Graph"))
            {
                declaration.owner.activeGraph = declaration.graph;
                var reference = GraphReference.New(declaration, true);
                GraphWindow.OpenActive(reference);
            }

            y += 20;

            return buttonRect;
        }

        private Rect Returns(Rect previous)
        {
            var returnRect = previous;
            returnRect.y = returnRect.y + returnRect.height;

            LudiqGUI.Inspector(accessor["returns"], returnRect, GUIContent.none);

            return returnRect;
        }
    }
}