using System.Security.Cryptography;
using System.Text;

namespace SubtitleTranslator.Backend.Utils;

public class MD5Utils
{
    public static string GetMD5WithString(string sDataIn)
    {
        byte[] data = Encoding.GetEncoding("utf-8").GetBytes(sDataIn);
        using (MD5 md5 = MD5.Create())
        {
            byte[] bytes = md5.ComputeHash(data);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}