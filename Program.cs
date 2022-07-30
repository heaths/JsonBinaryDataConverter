using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Core.Serialization;

Sample sample = new()
{
    Name = "foo",
    AdditionalData = new[]
    {
        BinaryData.FromObjectAsJson(new { Foo = 1, Bar = "bar" }),
        BinaryData.FromObjectAsJson(new { Baz = new[] { 1, 2, 3 }}),
    },
};

JsonSerializerOptions options = new()
{
    // Can't set this using [JsonConverter] with a property type not explicitly defined as BinaryData.
    Converters =
    {
        new JsonBinaryDataConverter(),
    },
    IgnoreNullValues = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
};
string content = JsonSerializer.Serialize(sample, options);
Console.WriteLine(content);

class Sample
{
    public string? Name { get; set; }

    [JsonPropertyName("modelVersion")]
    
    public string? Version { get; set; }

    [JsonPropertyName("context")]
    public IList<BinaryData>? AdditionalData { get; set; }
}