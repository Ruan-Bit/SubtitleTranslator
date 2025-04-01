using SubtitleTranslator.Backend.FileParser;
using SubtitleTranslator.Backend.TranslateService;

namespace TestProject;

[TestClass]
public sealed class Test1
{
    [TestMethod]
    public async Task TestParseVtt()
    {
        var vttParser = new VttParser();
        await foreach(var subtitleItem in vttParser.ReadAsync(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\TestFiles\") + @"Greater than 24h.vtt"))
        {
            Console.WriteLine(subtitleItem.Text);
        }
    }
    

    [TestMethod]
    public void TestTranslateService()
    {
        var baiduTranslateService = ITranslateService.GetService(ITranslateService.ServiceType.BaiduApi);
        var result = baiduTranslateService.TranslateAsync("hello testing").Result;
        Console.WriteLine("翻译结果：" + result);
        
    }
}