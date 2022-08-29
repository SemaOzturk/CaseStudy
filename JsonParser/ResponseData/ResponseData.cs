using System.Text.Json.Serialization;

namespace JsonParser.ResponseData;

public class ResponseData
{
    [JsonPropertyName("locale")]
    public string Locale { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("boundingPoly")]
    public BoundingPoly BoundingPoly { get; set; }
}
public class BoundingPoly
{
    [JsonPropertyName("vertices")]
    public List<Vertex> Vertices { get; set; }
}
public class Vertex
{
    [JsonPropertyName("x")]
    public int X { get; set; }

    [JsonPropertyName("y")]
    public int Y { get; set; }
}