namespace SubtitleTranslator.Backend.Obj;

public class SubtitleItem
{
    //字幕时间key
    public string TimeKey { get; set; } = string.Empty;
    
    //字幕语句
    public string Sentence { get; set; } = string.Empty;
    
    //字幕所在行数
    public long LineIndex { get; set; }
    
    //翻译后的句子
    public string SubtitleTranslatedItem { get; set; } = string.Empty;
}