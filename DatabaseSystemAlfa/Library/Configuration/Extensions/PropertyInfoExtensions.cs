using System.Reflection;

namespace DatabaseSystemAlfa.Library.Configuration.Extensions;

/// <remarks>
/// Class from StackOverflow, <a href="https://stackoverflow.com/questions/2051834/exclude-property-from-gettype-getproperties">source</a>.
/// </remarks>
public static class PropertyInfoExtensions
{
    private static IEnumerable<T> GetAttributes<T>(this PropertyInfo propertyInfo) where T : Attribute
    {
        return propertyInfo.GetCustomAttributes(typeof(T), true).Cast<T>();
    }

    public static bool IsMarketWith<T>(this PropertyInfo propertyInfo) where T : Attribute
    {
        return propertyInfo.GetAttributes<T>().Any();
    }
}