using System.Text.Json;
using System.Text.Json.Serialization;
using Ganss.Xss;

namespace CustomerProfileCenter.Api;

public class XssSanitizeJsonConvert : JsonConverter<string>
{
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var sanitiser = new HtmlSanitizer();
        var raw = reader.GetString();
        return sanitiser.Sanitize(raw);
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}