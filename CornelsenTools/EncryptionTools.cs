using System.Security.Cryptography;
using System.Text;

namespace CornelsenTools;

public class EncryptionTools
{
    /// <summary>
    /// Decrypts the given byte array using the AES-128-CBC algorithm.
    /// </summary>
    /// <param name="toDecrypt">The byte array to decrypt.</param>
    /// <returns>The decrypted byte array.</returns>
    public static byte[] Decrypt(byte[] toDecrypt)
    {
        var api = "aes-128-cbc|D+DxJSF}2B;k-;C}";
        var @params = api.Split('|');
        var key = @params[1];
        var reversedKey = ReverseString(key);

        using var aes = Aes.Create();
        aes.KeySize = 128;
        aes.Mode = CipherMode.CBC;
        aes.Key = Encoding.ASCII.GetBytes(key);

        return aes.DecryptCbc(toDecrypt, Encoding.ASCII.GetBytes(reversedKey));
    }

    /// <summary>
    /// Encrypts the given byte array using the AES-128-CBC algorithm.
    /// </summary>
    /// <param name="toEncrypt">The byte array to encrypt.</param>
    /// <returns>The encrypted byte array.</returns>
    public static byte[] Encrypt(byte[] toEncrypt)
    {
        var api = "aes-128-cbc|D+DxJSF}2B;k-;C}";
        var @params = api.Split('|');
        var key = @params[1];
        var reversedKey = ReverseString(key);

        using var aes = Aes.Create();
        aes.KeySize = 128;
        aes.Mode = CipherMode.CBC;
        aes.Key = Encoding.ASCII.GetBytes(key);

        return aes.EncryptCbc(toEncrypt, Encoding.ASCII.GetBytes(reversedKey));
    }

    private static string ReverseString(string input)
    {
        var charArray = input.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}