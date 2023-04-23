using System.Security.Cryptography;
using System.Text;

namespace TravelAcrossUkraine.WebApi.Helpers;

public static class HashHelper
{
    public static string HashString(string stringToHash)
    {
        using var sha256 = SHA256.Create();

        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));

        var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

        return hash;
    }
}
