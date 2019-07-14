using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Linq;

namespace Lasm.Reflection
{
    public static class Reflector
    {
        /// <summary>
        /// Finds all types derived from another type, including abstracts.
        /// </summary>
        public static List<Type> GetAllOfType(Type derivedType)
        {
            List<System.Type> result = new List<System.Type>();
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (int assembly = 0; assembly < assemblies.Length; assembly++)
            {
                Type[] types = assemblies[assembly].GetTypes();

                for (int type = 0; type < types.Length; type++)
                {
                    if (derivedType.IsAssignableFrom(types[type]))
                    {
                        result.Add(types[type]);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Finds all types derived from another type, including abstracts.
        /// </summary>
        public static IEnumerable<Type> GetAllInNamespace(string @namespace, bool includeChildren = false)
        {
            List<System.Type> result = new List<System.Type>();
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                result.AddRange(assembly.GetTypes().Where((type) => 
                {
                    if (@namespace != type.Namespace) return false;
                    return true;
                }));
            }

            return result;
        }

        /// <summary>
        /// Finds and returns all types derived from another type. Excludes abstracts.
        /// </summary>
        public static List<Type> GetTypesOfType(Type derivedType)
        {
            List<System.Type> result = new List<System.Type>();
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (int assembly = 0; assembly < assemblies.Length; assembly++)
            {
                Type[] types = assemblies[assembly].GetTypes();

                for (int type = 0; type < types.Length; type++)
                {
                    if (!types[type].IsAbstract && derivedType.IsAssignableFrom(types[type]))
                    {
                        result.Add(types[type]);
                    }
                }
            }

            return result;
        }

        public static bool InheritsType(this Type type, Type inheritsFrom)
        {
            return inheritsFrom.IsAssignableFrom(type);
        }

        public static bool InheritsType<TType, TInheritsFrom>()
            where TType : Type
            where TInheritsFrom : Type
        {
            return typeof(TInheritsFrom).IsAssignableFrom(typeof(TType));
        }

        public static bool IsEnumerable(this Type type)
        {
            return type.InheritsType(typeof(IEnumerator)) || type.InheritsType(typeof(IEnumerable)) || type.IsIEnumerableT() || type.IsIEnumeratorT();
        }

        public static bool IsVoid(this Type type)
        {
            return type == typeof(Lasm.UAlive.Void) || type == typeof(void);
        }

        public static bool IsIEnumeratorT(this Type type)
        {
            var interfaces = type.GetInterfaces();
            foreach (Type _type in interfaces)
            {
                if (_type.IsGenericType && _type.GetGenericTypeDefinition() == typeof(IEnumerator<>))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsCoroutine(this Type type)
        {
            if (type == typeof(IEnumerator) && type != typeof(IEnumerable) && type != typeof(IEnumerator<>) && type != typeof(IEnumerable<>))
            {
                return true;
            }

            return false;
        }

        public static bool IsIEnumerator(this Type type)
        {
            if (type == typeof(IEnumerator) || type == typeof(IEnumerable) || type == typeof(IEnumerator<>) || type == typeof(IEnumerable<>))
            {
                return true;
            }

            return false;
        }

        public static bool IsIEnumerableT(this Type type)
        {
            var interfaces = type.GetInterfaces();
            foreach (Type _type in interfaces)
            {
                if (_type.IsGenericType && _type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return true;
                }
            }
            return false;
        }

        public static string ValueCompiled(this object value, bool isNew)
        {
            Type type = value.GetType();

            if (type == typeof(bool)) return value.ToString().ToLower();
            if (type == typeof(float)) return value.ToString() + "f";
            if (type == typeof(string)) return @"""" + value.ToString() + @"""";
            if (type == typeof(UnityEngine.GameObject)) return "null";
            if (type == typeof(int) || type == typeof(uint) || type == typeof(byte) || type == typeof(long) || type == typeof(short) || type == typeof(double)) return value.ToString();

            if (isNew) {
                if (type.IsClass || !type.IsInterface && !type.IsEnum)
                {
                    return "new " + type.Name + "(" + ")";
                }
            }

            return string.Empty;
        }
    }
}
