using System.Security.Cryptography;
using System.Text;
using Application.Constants;

namespace Application.Extensions;

public static class StringExtensions
{
    public static string PasswordHashEncrypt(this string input)
    {
        byte[] hashSet = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
        return hashSet.Aggregate(string.Empty, (current, x) => current + $"{x:x2}");
    }

    public static string GenerateTeamInitialsWithCode(this string input)
    {
        var name = input.ElementAt(^3);
        var code = RandomNumberGenerator.GetInt32(int.MaxValue / 10).GetHashCode().ToString();
        return $"{name}-{code}";
    }

    public static string GenerateAutoPassword(bool isSpecialCase,bool isUppercase,bool isLowerCase,bool isNumbers,int passwordSize)
    {
        int counter;
        char[] password = new char[passwordSize];
        string charset = string.Empty;
        var code = RandomNumberGenerator.GetInt32(int.MaxValue / passwordSize).GetHashCode();
        if (isUppercase) return charset + StringConstants.UpperCase;
        if (isLowerCase) return charset + StringConstants.LowerCase;
        if (isNumbers) return charset + StringConstants.Numbers;
        if (isSpecialCase) return charset + StringConstants.Specials;

        for (counter = 0; counter  < passwordSize; counter++)
        {
            password[counter] = charset[code];
        }
        return string.Join(null,password);
    }
}