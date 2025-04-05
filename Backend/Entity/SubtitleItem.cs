namespace SubtitleTranslator.Backend.Obj;

public class SubtitleItem
{
    //字幕时间key
    public string StartTime { get; set; } = string.Empty;
    
    //字幕时间key
    public string EndTime { get; set; } = string.Empty;
    
    //字幕语句
    public string Text { get; set; } = string.Empty;
    
    //翻译后的句子
    public string TranslatedText { get; set; } = string.Empty;

    public override string ToString()
    {
        return this.StartTime + "-->" + this.EndTime + "\n" + this.Text;
    }
}