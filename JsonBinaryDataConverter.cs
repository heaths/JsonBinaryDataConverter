using System.Text.Json;
using System.Text.Json.Serialization;

namespace Azure.Core.Serialization;

internal class JsonBinaryDataConverter : JsonConverter<BinaryData>
{
    public override BinaryData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return BinaryData.FromBytes(reader.ValueSpan.ToArray());
    }

    public override void Write(Utf8JsonWriter writer, BinaryData value, JsonSerializerOptions options)
    {
        using JsonDocument doc = JsonDocument.Parse(value.ToStream());
        doc.RootElement.WriteTo(writer);
    }
}