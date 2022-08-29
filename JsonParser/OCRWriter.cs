using System.Collections.ObjectModel;
using System.Text;

public class OCRWriter
{
    private int _lineHeight;
    private readonly IList<OCRLine> _lines;

    public OCRWriter(int lineHeight)
    {
        _lineHeight = lineHeight;
        _lines = new List<OCRLine>()
        {
            new()
            {
                StartHeight = 0
            }
        };
    }

    public void AddTexts(IEnumerable<OCRText> ocrTexts)
    {
        int firstHeight = ocrTexts.First().Y;
        _lines.First().StartHeight = firstHeight;
        foreach (var ocrText in ocrTexts)
        {
            AddText(ocrText);
        }
    }

    private void AddText(OCRText ocrText)
    {
        var heightCheck = (OCRLine x) => x.StartHeight <= ocrText.Y && ocrText.Y < x.StartHeight + _lineHeight;
        var line = _lines.FirstOrDefault(heightCheck);
        while (line == null)
        {
            var last = _lines.Last();
            _lines.Add(new OCRLine()
            {
                StartHeight = last.StartHeight + _lineHeight
            });
            line = _lines.FirstOrDefault(heightCheck);
        }

        line.Texts.Add(ocrText);
    }

    public string GetOutputText()
    {
        var stringBuilder = new StringBuilder();
        foreach (var line in _lines)
        {
            if (line.Texts.Any())
            {
                stringBuilder.AppendLine(string.Join(" ", line.Texts.OrderBy(x => x.X).Select(x => x.Text)));
            }
        }

        return stringBuilder.ToString();
    }

    public class OCRText
    {
        public string Text { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    private class OCRLine
    {
        public int StartHeight { get; set; }
        public IList<OCRText> Texts { get; set; } = new Collection<OCRText>();
    }
}