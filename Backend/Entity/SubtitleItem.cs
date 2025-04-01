namespace SubtitleTranslator.Backend.Obj;

public class SubtitleItem
{
    //字幕时间key
    public string TimeKey { get; set; } = string.Empty;
    
    //字幕语句
    public string Text { get; set; } = string.Empty;
    
    //翻译后的句子
    public string TranslatedText { get; set; } = string.Empty;
}