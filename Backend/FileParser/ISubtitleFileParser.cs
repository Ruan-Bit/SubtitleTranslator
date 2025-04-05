using SubtitleTranslator.Backend.Obj;

namespace SubtitleTranslator.Backend.FileParser;

public interface ISubtitleFileParser
{
    IEnumerable<SubtitleItem> Read(string filePath);
    
    Task WriteAsync(string newFilePath, IEnumerable<SubtitleItem> sentences);

}