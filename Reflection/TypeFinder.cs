using Ludiq;
using Lasm.UAlive;
using System.Collections.Generic;
using System.Linq;

namespace Lasm.UAlive
{
    public static class TypeFinder
    {
        public static IEnumerable<string> Namespaces(this IEnumerable<ObjectMacro> types)
        {
            return types.Select<ObjectMacro, string>((type) =>
            {
                return type.@namespace;
            });
        }

        public static IEnumerable<ObjectMacro> Classes(this IEnumerable<ObjectMacro> types)
        {
            return types.Where((macro)=> { return macro.kind == ObjectKind.Class; });
        }

        public static IEnumerable<ObjectMacro> Enums(this IEnumerable<ObjectMacro> types)
        {
            return types.Where((macro) => { return macro.kind == ObjectKind.Enum; });
        }

        public static IEnumerable<ObjectMacro> Structs(this IEnumerable<ObjectMacro> types)
        {
            return types.Where((macro) => { return macro.kind == ObjectKind.Struct; });
        }

        public static IEnumerable<ObjectMacro> Interfaces(this IEnumerable<ObjectMacro> types)
        {
            return types.Where((macro) => { return macro.kind == ObjectKind.Interface; });
        }

        public static IEnumerable<ObjectMacro> Events(this IEnumerable<ObjectMacro> types)
        {
            return types.Where((macro) => { return macro.kind == ObjectKind.Event; });
        }

        public static IEnumerable<ObjectMacro> Types()
        {
            return AssetUtility.FindAllAssetsOfType<ObjectMacro>().ToArray();
        }

        public static ObjectMacro GetMacro(string name, ObjectKind kind)
        {
            switch (kind) {

                case ObjectKind.Class:

                    IEnumerable<ObjectMacro> macros = UnityEngine.Resources.FindObjectsOfTypeAll<ObjectMacro>();
                    macros = Classes(macros);
                    return macros.Where((macro) => { return macro.name == name; }).Single();

            }

            return null;
        }
    }
}
