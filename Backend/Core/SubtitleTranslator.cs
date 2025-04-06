using SubtitleTranslator.Backend.Obj;
using SubtitleTranslator.Backend.TranslateService;

namespace SubtitleTranslator.Backend.FileParser;

public class SubtitleTranslator
{
    public async Task TranslateFile(string filePath)
    {
        var directoryPath = Path.GetDirectoryName(filePath);//文件所在目录
        var fileName = Path.GetFileName(filePath);//文件名
        var newFileName = Path.GetFileNameWithoutExtension(fileName) + "_translated" + Path.GetExtension(fileName);//新文件名
        var fileParser = SubtitleParserFactory.CreateInstance(fileName);
        var baiduTranslateService = ITranslateService.GetService(ITranslateService.ServiceType.BaiduApi);
        var subtitleItems = fileParser.Read(filePath);
        var enumerable = subtitleItems as SubtitleItem[] ?? subtitleItems.ToArray();
        foreach (var subtitleItem in enumerable)
        {
            var translatedTex = await baiduTranslateService.TranslateAsync(subtitleItem.Text);
            subtitleItem.TranslatedText = translatedTex;
        }
        await fileParser.WriteAsync(newFileName, enumerable);
    }
}