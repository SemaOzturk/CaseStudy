// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using System.Text.Json.Serialization;
using JsonParser.ResponseData;


var example = JsonSerializer.Deserialize<IEnumerable<ResponseData>>(File.ReadAllText("response.json"));
var writer = new OCRWriter(26);
var ocrTexts = example.Skip(1).Select(x =>
{
    var vertex = x.BoundingPoly.Vertices.First();
    return new OCRWriter.OCRText()
    {
        Text = x.Description,
        X = vertex.X,
        Y = vertex.Y
    };
});
writer.AddTexts(ocrTexts);

File.WriteAllText("output.txt", writer.GetOutputText());

