using System.Text.RegularExpressions;
using SubtitleTranslator.Backend.Obj;

namespace SubtitleTranslator.Backend.FileParser;

public class VttParser : ISubtitleFileParser
{
    private static readonly Regex TimeRegex = new Regex(@"[0-9]*:[0-9]+:[0-9]+.[0-9]+ --> [0-9]*:[0-9]+:[0-9]+.[0-9]+");

    public async IAsyncEnumerable<SubtitleItem> ReadAsync(string filepath)
    {
        var liensEnumerable = File.ReadLinesAsync(filepath);
        await foreach (var lien in liensEnumerable)
        {
            var lineTrimmed = lien.Trim();
            var subtitleItem = new SubtitleItem();
            if (lineTrimmed.StartsWith("WEBVTT") || lineTrimmed.StartsWith("NOTE") || string.IsNullOrEmpty(lineTrimmed)) 
            {
                continue; 
            }
            //时间
            if (lineTrimmed.Contains("-->"))
            {
                var match = TimeRegex.Match(lineTrimmed);
                if (match.Success)
                {
                    subtitleItem.TimeKey = match.Groups[0].Value + match.Groups[1].Value;
                }
            }
            else
            {
                //字幕
                subtitleItem.Text = lineTrimmed;
            }
            yield return subtitleItem;
        }
    }
    

    public Task WriteAsync(string filepath, IAsyncEnumerable<SubtitleItem> sentences)
    {
        throw new NotImplementedException();
    }
}