using SubtitleTranslator.Backend.Obj;

namespace SubtitleTranslator.Backend.TranslateService;

public interface ITranslateService
{
    public static ITranslateService GetService(ServiceType serviceType)
    {
        return serviceType switch
        {
            ServiceType.BaiduApi => new BaiduApiTranslateService(),
            _ => new BaiduApiTranslateService()
        };
    }
    public Task<string> TranslateAsync(string sentence);
    public enum ServiceType
    {
        BaiduApi
    }
}