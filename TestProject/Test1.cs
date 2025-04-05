using System.Text.RegularExpressions;
using SubtitleTranslator.Backend.FileParser;
using SubtitleTranslator.Backend.Obj;
using SubtitleTranslator.Backend.TranslateService;

namespace TestProject;

[TestClass]
public sealed class Test1
{
    [TestMethod]
    public async Task TestParseVtt()
    {
        var vttParser = new VttParser();
        var baiduTranslateService = ITranslateService.GetService(ITranslateService.ServiceType.BaiduApi);
        var subtitleItems = vttParser.Read(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\TestFiles\") + @"Greater than 24h.vtt");
        var enumerable = subtitleItems as SubtitleItem[] ?? subtitleItems.ToArray();
        foreach (var subtitleItem in enumerable)
        {
            var translatedTex = await baiduTranslateService.TranslateAsync(subtitleItem.Text);
            subtitleItem.TranslatedText = translatedTex;
        }
        await vttParser.WriteAsync(@"..\..\..\TargetFiles\TestVttRes.vtt", enumerable);
    }

    private static readonly Regex TimeRegex = new Regex(@"([0-9]+:)*[0-9]+:[0-9]+.[0-9]+ --> ([0-9]+:)*[0-9]+:[0-9]+.[0-9]+");

    [TestMethod]
    public void TestRead()
    {
        var liensEnumerable = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\TestFiles\") + @"Greater than 24h.vtt");
        int i = 0;
        while (i < liensEnumerable.Length)
        {
            var curLine = liensEnumerable[i].Trim();
            if (curLine.Contains("-->") && TimeRegex.Match(curLine).Success)
            {
                var subtitleItem = new SubtitleItem();
                var times = curLine.Split("-->");
                subtitleItem.StartTime = times[0].Trim();
                subtitleItem.EndTime = times[1].Trim();
                var nextLine = liensEnumerable[++i].Replace("-", "").Trim();
                subtitleItem.Text = nextLine;
                Console.WriteLine(subtitleItem.ToString());
            }
            i++;
        }
    }
    

    [TestMethod]
    public void TestTranslateService()
    {
        var baiduTranslateService = ITranslateService.GetService(ITranslateService.ServiceType.BaiduApi);
        var result = baiduTranslateService.TranslateAsync("hello").Result;
        Console.WriteLine("翻译结果：" + result);
        
    }
}