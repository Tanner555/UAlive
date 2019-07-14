using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Ludiq;
using Ludiq.Bolt;

namespace Lasm.UAlive
{
    public static class UAliveIcons
    {
        private static Dictionary<string, Texture2D> icons = new Dictionary<string, Texture2D>();
        public static string frameworkIcons => RootPathFinder.FindRootPath() + "UAlive/Editor/Resources/Icons/";
        public static string ludiqTypeIcons => "Assets/Ludiq/Ludiq.Core/Editor/Resources/Icons/Types/";
        public static string boltTypeIcons => "Assets/Ludiq/Bolt.Core/Editor/Resources/Icons/Types/";
        public static string boltFlowTypeIcons => "Assets/Ludiq/Bolt.Flow/Editor/Resources/Icons/Types/";
        public static string boltFlowUnitCategoryIcons => "Assets/Ludiq/Bolt.Flow/Editor/Resources/Icons/Unit Categories/";

        public static Texture2D Get(string name)
        {
            return icons[name];
        }

        public static string IconPath(string title, IconSize size, FrameworkType frameworkType)
        {
            switch (frameworkType)
            {
                case FrameworkType.Framework:
                    switch (size)
                    {
                        case IconSize.Small:
                            return frameworkIcons + title + "@8x.png";
                        case IconSize.Medium:
                            return frameworkIcons + title + "@16x.png";
                        case IconSize.Large:
                            return frameworkIcons + title + "@32x.png";
                        case IconSize.XLarge:
                            return frameworkIcons + title + "@64x.png";
                    }
                    break;
                case FrameworkType.Bolt_Core:
                    switch (size)
                    {
                        case IconSize.Small:
                            return boltTypeIcons + title + "@8x.png";
                        case IconSize.Medium:
                            return boltTypeIcons + title + "@16x.png";
                        case IconSize.Large:
                            return boltTypeIcons + title + "@32x.png";
                        case IconSize.XLarge:
                            return boltTypeIcons + title + "@64x.png";
                    }
                    break;
                case FrameworkType.Bolt_Flow:
                    switch (size)
                    {
                        case IconSize.Small:
                            return boltFlowTypeIcons + title + "@8x.png";
                        case IconSize.Medium:
                            return boltFlowTypeIcons + title + "@16x.png";
                        case IconSize.Large:
                            return boltFlowTypeIcons + title + "@32x.png";
                        case IconSize.XLarge:
                            return boltFlowTypeIcons + title + "@64x.png";
                    }
                    break;
                case FrameworkType.Bolt_UnitCategories:
                    switch (size)
                    {
                        case IconSize.Small:
                            return boltFlowUnitCategoryIcons + title + "@8x.png";
                        case IconSize.Medium:
                            return boltFlowUnitCategoryIcons + title + "@16x.png";
                        case IconSize.Large:
                            return boltFlowUnitCategoryIcons + title + "@32x.png";
                        case IconSize.XLarge:
                            return boltFlowUnitCategoryIcons + title + "@64x.png";
                    }
                    break;
                case FrameworkType.Ludiq_Core:
                    switch (size)
                    {
                        case IconSize.Small:
                            return ludiqTypeIcons + title + "@8x.png";
                        case IconSize.Medium:
                            return ludiqTypeIcons + title + "@16x.png";
                        case IconSize.Large:
                            return ludiqTypeIcons + title + "@32x.png";
                        case IconSize.XLarge:
                            return ludiqTypeIcons + title + "@64x.png";
                    }
                    break;
            }
            

            return null;
        }

        public static Texture2D IconFromPath(string title, IconSize size)
        {
            return AssetDatabase.LoadAssetAtPath<Texture2D>(IconPath(title, size, FrameworkType.Framework));
        }

        public static Texture2D IconFromPathBolt(string title, IconSize size)
        {
            return AssetDatabase.LoadAssetAtPath<Texture2D>(IconPath(title, size, FrameworkType.Bolt_Core));
        }

        public static Texture2D IconFromPathBoltFlow(string title, IconSize size)
        {
            return AssetDatabase.LoadAssetAtPath<Texture2D>(IconPath(title, size, FrameworkType.Bolt_Flow));
        }

        public static Texture2D IconFromPathBoltFlowUnitCategories(string title, IconSize size)
        {
            return AssetDatabase.LoadAssetAtPath<Texture2D>(IconPath(title, size, FrameworkType.Bolt_UnitCategories));
        }

        public static Texture2D IconFromPathLudiq(string title, IconSize size)
        {
            return AssetDatabase.LoadAssetAtPath<Texture2D>(IconPath(title, size, FrameworkType.Ludiq_Core));
        }

        public static Texture2D FindIcon(this System.Type type)
        {
            var name = type.IsPrimitive ? type.CSharpName(false) : type.CSharpFullName(false);

            var icon = UAliveIcons.IconFromPathLudiq(name, UAliveIcons.IconSize.Medium);

            if (icon != null) return icon;

            icon = UAliveIcons.IconFromPathBoltFlow(name, UAliveIcons.IconSize.Medium);

            if (icon == null) return icon;

            icon = UAliveIcons.IconFromPathBolt(name, UAliveIcons.IconSize.Medium);

            return icon;
        }

        public static Texture2D Set(string name, Texture2D texture)
        {
            if (icons.ContainsKey(name))
            {
                icons[name] = texture;
            }
            else
            {
                icons.Add(name, texture);
            }

            return icons[name];
        }

        public static Texture2D SetWhenNull(string name, Texture2D texture)
        {
            if (!icons.ContainsKey(name))
            {
                icons.Add(name, texture);
            }
            else
            {
                if (icons[name] == null)
                {
                    icons[name] = texture;
                }
            }

            return icons[name];
        }

        public static bool HasIcon(string name)
        {
            return icons.ContainsKey(name);
        }

        public enum IconSize
        {
            Small,
            Medium,
            Large,
            XLarge
        }

        public enum FrameworkType
        {
            Bolt_Core,
            Bolt_Flow,
            Bolt_UnitCategories,
            Ludiq_Core,
            Framework
        }
    }
}