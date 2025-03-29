namespace SubtitleTranslator.Backend.Obj;

public class BaiduTranslateRes
{
    public string? from { get; init; }

    public string? to { get; init; }

    public List<Result>? trans_result { get; init; }

    public class Result(string src, string dst)
    {
        public string? src { get; init; }

        public string? dst { get; init; }
    }
}