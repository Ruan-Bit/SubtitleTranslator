using SubtitleTranslator.Backend.Obj;

namespace SubtitleTranslator.Backend.FileParser;

public interface ISubtitleFileParser
{
    IAsyncEnumerable<SubtitleItem> ReadAsync(string filePath);
    
    Task WriteAsync(string filePath, IAsyncEnumerable<SubtitleItem> sentences);
}