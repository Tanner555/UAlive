using UnityEngine;

namespace Lasm.UAlive
{
    public static class UAliveResources
    {
        #region Cached Textures
        public static Texture2D @class;
        public static Texture2D scope;
        public static Texture2D variable;
        public static Texture2D method;
        public static Texture2D @struct;
        public static Texture2D @interface;
        public static Texture2D @enum;
        public static Texture2D @event;
        public static Texture2D properties;
        public static Texture2D contract;
        public static Texture2D arrowRightWhite;
        public static Texture2D arrowDownWhite;
        public static Texture2D arrowRightDark;
        public static Texture2D arrowDownDark;
        public static Texture2D arrowUpBlack;
        public static Texture2D arrowDownBlack;
        public static Texture2D addBlack;
        public static Texture2D removeBlack;
        public static Texture2D warning;
        public static Texture2D okay;

        public static bool texturesCached;
        #endregion

        public static void CacheTextures()
        {
            if (!texturesCached)
            {
                contract = UAliveIcons.SetWhenNull("Contract", UAliveIcons.IconFromPath("contract", UAliveIcons.IconSize.Large));
                scope = UAliveIcons.SetWhenNull("Scope", UAliveIcons.IconFromPath("scope", UAliveIcons.IconSize.Large));
                variable = UAliveIcons.SetWhenNull("Variable", UAliveIcons.IconFromPathBoltFlowUnitCategories("Variables", UAliveIcons.IconSize.Medium));
                method = UAliveIcons.SetWhenNull("Method", UAliveIcons.IconFromPathBoltFlow("Ludiq.Bolt.Flow", UAliveIcons.IconSize.Medium));
                @class = UAliveIcons.SetWhenNull("ClassKind", UAliveIcons.IconFromPath("class", UAliveIcons.IconSize.Medium));
                @struct = UAliveIcons.SetWhenNull("StructKind", UAliveIcons.IconFromPath("struct", UAliveIcons.IconSize.Medium));
                @interface = UAliveIcons.SetWhenNull("InterfaceKind", UAliveIcons.IconFromPath("interface", UAliveIcons.IconSize.Medium));
                @enum = UAliveIcons.SetWhenNull("EnumKind", UAliveIcons.IconFromPath("enum", UAliveIcons.IconSize.Medium));
                @event = UAliveIcons.SetWhenNull("EventKind", UAliveIcons.IconFromPath("event", UAliveIcons.IconSize.Medium));
                properties = UAliveIcons.SetWhenNull("Properties", UAliveIcons.IconFromPath("properties", UAliveIcons.IconSize.Medium));
                arrowRightWhite = UAliveIcons.SetWhenNull("ArrowRightWhite", UAliveIcons.IconFromPath("whitearrow_right", UAliveIcons.IconSize.Medium));
                arrowDownWhite = UAliveIcons.SetWhenNull("ArrowDownWhite", UAliveIcons.IconFromPath("whitearrow_down", UAliveIcons.IconSize.Medium));
                arrowRightDark = UAliveIcons.SetWhenNull("ArrowRightDark", UAliveIcons.IconFromPath("darkarrow_right", UAliveIcons.IconSize.Medium));
                arrowDownDark = UAliveIcons.SetWhenNull("ArrowDownDark", UAliveIcons.IconFromPath("darkarrow_down", UAliveIcons.IconSize.Medium));
                arrowUpBlack = UAliveIcons.SetWhenNull("ArrowUpBlack", UAliveIcons.IconFromPath("blackarrow_up", UAliveIcons.IconSize.Medium));
                arrowDownBlack = UAliveIcons.SetWhenNull("ArrowDownBlack", UAliveIcons.IconFromPath("blackarrow_down", UAliveIcons.IconSize.Medium));
                addBlack = UAliveIcons.SetWhenNull("AddBlack", UAliveIcons.IconFromPath("black_add", UAliveIcons.IconSize.Medium));
                removeBlack = UAliveIcons.SetWhenNull("RemoveBlack", UAliveIcons.IconFromPath("black_remove", UAliveIcons.IconSize.Medium));
                warning = UAliveIcons.SetWhenNull("Warning", UAliveIcons.IconFromPath("warning", UAliveIcons.IconSize.Large));
                okay = UAliveIcons.SetWhenNull("Okay", UAliveIcons.IconFromPath("okay", UAliveIcons.IconSize.Large));
                texturesCached = true; 
            }
        }
    }
}
