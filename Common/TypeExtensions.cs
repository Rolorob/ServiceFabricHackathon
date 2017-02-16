using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> GetDerivedTypes(this Type baseType, Assembly assembly)
        {
            return assembly
                .GetTypes()
                .Where(t => t.IsSubclassOf(baseType));
        }
    }
}