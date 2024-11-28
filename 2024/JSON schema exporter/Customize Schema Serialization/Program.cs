using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Schema;

JsonSerializerOptions options = new(JsonSerializerOptions.Default)
{
    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
};
JsonNode schema = options.GetJsonSchemaAsNode(typeof(AddressChangedEvent));
Console.WriteLine(schema.ToString());

public record class AddressChangedEvent(Guid UserId, string Address);
