using System.Text.Json;
using System.Text.Json.Serialization;
using ComponentName.SharedKernel;

namespace ComponentName.Infrastructure;

public class EntityIdConverter<T> : JsonConverter<T>
    where T : EntityId
{
    public override T Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var stringValue = reader.GetString();
        ArgumentException.ThrowIfNullOrEmpty(stringValue);

        var entityId = Activator.CreateInstance(typeof(T), Guid.Parse(stringValue)) as T;
        return entityId!;
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
