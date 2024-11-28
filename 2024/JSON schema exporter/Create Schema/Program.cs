using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Schema;

JsonSerializerOptions options = JsonSerializerOptions.Default;
JsonNode schema = options.GetJsonSchemaAsNode(typeof(AddressChangedEvent));
Console.WriteLine(schema.ToString());

public record class AddressChangedEvent(Guid UserId, string Address);
