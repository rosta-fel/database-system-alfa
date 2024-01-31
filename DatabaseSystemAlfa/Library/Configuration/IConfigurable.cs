using System.Reflection;

namespace DatabaseSystemAlfa.Library.Configuration;

public interface IConfigurable
{
    IEnumerable<PropertyInfo> GetProperties();
}