using SubtitleTranslator.Backend.Obj;

namespace SubtitleTranslator.Backend.FileParser;

public interface ISubtitleFileParser
{
    IAsyncEnumerable<SubtitleItem> ReadAsync(Stream stream);
    
    Task WriteAsync(Stream stream, IAsyncEnumerable<SubtitleItem> sentences);
}