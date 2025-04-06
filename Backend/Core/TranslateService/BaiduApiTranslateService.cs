using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using SubtitleTranslator.Backend.Obj;
using SubtitleTranslator.Backend.Utils;

namespace SubtitleTranslator.Backend.TranslateService;

public class BaiduApiTranslateService : ITranslateService
{
    private static readonly HttpClient SharedClient = new()
    {
        BaseAddress = new Uri("https://fanyi-api.baidu.com/api/trans/vip/translate")
    };

    private const string AppId = "20250313002302788";

    private const string Key = "v1L9hdDyxp1XBVF07Zdz";

    public async Task<string> TranslateAsync(string sentence)
    {
        if (string.IsNullOrEmpty(sentence))
        {
            return "";
        }
        var salt = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        var sign = MD5Utils.GetMD5WithString(AppId + sentence + salt + Key);
        var requestBody = new Dictionary<string, string>
        {
            { "q", sentence },
            { "from", "auto" },
            { "to", "zh"},
            { "appid", AppId },
            { "salt", salt },
            { "sign", sign }
        };
        var content = new FormUrlEncodedContent(requestBody);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

        var responseMsg = await SharedClient.PostAsync("", content);
        responseMsg.EnsureSuccessStatusCode();
        var responseContent = await responseMsg.Content.ReadAsStringAsync();

        var baiduTranslateRes = JsonSerializer.Deserialize<BaiduTranslateRes>(responseContent);
        if (baiduTranslateRes?.trans_result == null || baiduTranslateRes.trans_result.Count == 0)
        {
            return "";
        }

        var result = new StringBuilder();
        foreach (var t in baiduTranslateRes.trans_result)
        {
            result.Append(t.dst);
        }
            
        return result.ToString();
    }
}