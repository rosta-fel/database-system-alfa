using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using DatabaseSystemAlfa.Library.Configuration;

namespace DatabaseSystemAlfa.Library.Tools.Json;

public class AppSettingsConverter : JsonConverter<AppSettings>
{
    public override AppSettings Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, AppSettings value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (var kvp in value.Config)
        {
            writer.WriteStartObject(kvp.Key);

            var configType = kvp.Value.GetType();
            var properties = configType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(kvp.Value);
                writer.WritePropertyName(property.Name);
                JsonSerializer.Serialize(writer, propertyValue, property.PropertyType, options);
            }

            writer.WriteEndObject();
        }

        writer.WriteEndObject();
    }
}