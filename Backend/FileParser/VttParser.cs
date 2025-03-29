using SubtitleTranslator.Backend.Obj;

namespace SubtitleTranslator.Backend.FileParser;

public class VttParser : ISubtitleFileParser
{
    public IAsyncEnumerable<SubtitleItem> ReadAsync(Stream stream)
    {
        throw new NotImplementedException();
    }

    public Task WriteAsync(Stream stream, IAsyncEnumerable<SubtitleItem> sentences)
    {
        throw new NotImplementedException();
    }
}