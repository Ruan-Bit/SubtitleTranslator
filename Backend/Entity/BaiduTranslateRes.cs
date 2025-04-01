namespace SubtitleTranslator.Backend.Obj;

public class BaiduTranslateRes
{
    public string? from { get; set; }

    public string? to { get; set; }

    public List<Result>? trans_result { get; set; }

    public class Result
    {
        public string? src { get; set; }

        public string? dst { get; set; }
    }
}