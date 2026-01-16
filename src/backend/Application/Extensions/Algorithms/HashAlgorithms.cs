
using System.Security.Cryptography;
using System.Text;

namespace Application.Extensions.Algorithms;

public class HashAlgorithms
{
    public static string ConvertToHash(string rawData)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));

        StringBuilder builder = new StringBuilder();

        foreach (var v in bytes)
        {
            builder.Append(v.ToString("x2"));
        }

        return builder.ToString();
    }
}