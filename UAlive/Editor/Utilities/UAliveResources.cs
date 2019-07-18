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

        #region Units
        public static Texture2D returnUnit;
        public static Texture2D processorDirectiveUnit;
        public static Texture2D asUnit;
        public static Texture2D isUnit;
        public static Texture2D continueUnit;
        public static Texture2D doUnit;
        public static Texture2D gotoUnit;
        public static Texture2D labelUnit;
        public static Texture2D lockUnit;
        public static Texture2D constructorUnit;
        public static Texture2D getterUnit;
        public static Texture2D setterUnit;
        #endregion

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

                // UNITS
                returnUnit = UAliveIcons.SetWhenNull("ReturnUnit", UAliveIcons.IconFromPath("return_unit", UAliveIcons.IconSize.Large));
                processorDirectiveUnit = UAliveIcons.SetWhenNull("ProcessorDirectiveUnit", UAliveIcons.IconFromPath("processor_directive_unit", UAliveIcons.IconSize.Large));
                asUnit = UAliveIcons.SetWhenNull("AsUnit", UAliveIcons.IconFromPath("as_unit", UAliveIcons.IconSize.Large));
                isUnit = UAliveIcons.SetWhenNull("IsUnit", UAliveIcons.IconFromPath("is_unit", UAliveIcons.IconSize.Large));
                doUnit = UAliveIcons.SetWhenNull("DoUnit", UAliveIcons.IconFromPath("do_unit", UAliveIcons.IconSize.Large));
                continueUnit = UAliveIcons.SetWhenNull("ContinueUnit", UAliveIcons.IconFromPath("continue_unit", UAliveIcons.IconSize.Large));
                gotoUnit = UAliveIcons.SetWhenNull("GotoUnit", UAliveIcons.IconFromPath("goto_unit", UAliveIcons.IconSize.Large));
                labelUnit = UAliveIcons.SetWhenNull("LabelUnit", UAliveIcons.IconFromPath("label_unit", UAliveIcons.IconSize.Large));
                lockUnit = UAliveIcons.SetWhenNull("LockUnit", UAliveIcons.IconFromPath("lock_unit", UAliveIcons.IconSize.Large));
                constructorUnit = UAliveIcons.SetWhenNull("ConstructorUnit", UAliveIcons.IconFromPath("constructor_unit", UAliveIcons.IconSize.Large));
                getterUnit = UAliveIcons.SetWhenNull("GetterUnit", UAliveIcons.IconFromPath("getter_unit", UAliveIcons.IconSize.Large));
                setterUnit = UAliveIcons.SetWhenNull("SetterUnit", UAliveIcons.IconFromPath("setter_unit", UAliveIcons.IconSize.Large));
                 
                texturesCached = true; 
            }
        }
    }
}
