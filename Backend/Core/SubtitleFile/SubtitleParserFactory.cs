namespace SubtitleTranslator.Backend.FileParser;

public class SubtitleParserFactory
{
    public static ISubtitleFileParser CreateInstance(string fileName)
    {
        //获取文件名的后缀
        var fileSuffix = Path.GetExtension(fileName);
        switch (fileSuffix)
        {
            case ".vtt":
                return new VttParser();
            default:
                //抛出异常
                throw new FileFormatException("文件格式不支持");
        }
    }
}