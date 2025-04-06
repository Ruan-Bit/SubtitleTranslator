using System.Text.RegularExpressions;
using SubtitleTranslator.Backend.Obj;

namespace SubtitleTranslator.Backend.FileParser;

public class VttParser : ISubtitleFileParser
{
    private static readonly Regex TimeRegex = new Regex(@"([0-9]+:)*[0-9]+:[0-9]+.[0-9]+ --> ([0-9]+:)*[0-9]+:[0-9]+.[0-9]+");

    public IEnumerable<SubtitleItem> Read(string filepath)
    {
        var liensEnumerable = File.ReadAllLines(filepath);
        int i = 0;
        while (i < liensEnumerable.Length)
        {
            var curLine = liensEnumerable[i].Trim();
            if (IsTimeLine(curLine))
            {
                var subtitleItem = new SubtitleItem();
                var times = curLine.Split("-->");
                subtitleItem.StartTime = times[0].Trim();
                subtitleItem.EndTime = times[1].Trim();
                //字幕行
                var subtitleLines = new List<string>();
                while (i < liensEnumerable.Length - 1)
                {
                    var subtitleLine = liensEnumerable[++i];
                    if (!IsTimeLine(subtitleLine))
                    {
                        subtitleLines.Add(subtitleLine.Replace("—", "").Trim());
                    }
                    else
                    {
                        break;
                    }
                }
                subtitleItem.Text = subtitleLines.Aggregate((a, b) => a  + b);
                yield return subtitleItem;
            }
            else
            {
                i++;
            }
        }
    }
    
    private bool IsTimeLine(string line)
    {
        return line.Contains("-->") && TimeRegex.Match(line).Success;
    }

    public async Task WriteAsync(string newFilePath, IEnumerable<SubtitleItem> subtitleItems)
    {
        var fileStream = File.Create(newFilePath);
        var streamWriter = new StreamWriter(fileStream);
        await streamWriter.WriteLineAsync("WEBVTT\n\n");
        foreach (var subtitleItem in subtitleItems)
        {
            await streamWriter.WriteLineAsync(
                subtitleItem.StartTime + " --> " + subtitleItem.EndTime
                + "\n"
                + subtitleItem.TranslatedText
                + "\n"
            );
        }
        await streamWriter.FlushAsync();
        streamWriter.Close();
    }
}