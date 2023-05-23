using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Utils;
internal class EncryptionTools {
    #region Fields
    private const int KEY_SIZE = 32;
    //private const int IV_SIZE = KEY_SIZE / 8;
    private static byte[] _key = new byte[KEY_SIZE];
    #endregion Fields

    #region Properties
    internal static byte[] Key {
        get { return _key; }
        set { _key = value; }
    }
    #endregion Proeprties

    #region Constructors

    #endregion Constructors

    #region Other Methods
    //accepts seriaized obj as string and encrypts to byte array; returns encrypted byte array as string (?)
    internal static string EncryptObjectStringToBase64String(string serializedObj) {
        System.Security.Cryptography.Aes aesEncrypt = System.Security.Cryptography.Aes.Create();
        aesEncrypt.Key = Key;
        aesEncrypt.IV = System.Security.Cryptography.RandomNumberGenerator.GetBytes(aesEncrypt.BlockSize / 8);

        byte[] plainText = Encoding.Unicode.GetBytes(serializedObj);
        byte[] cipherText = null;

        using (System.IO.MemoryStream ms = new System.IO.MemoryStream()) {
            using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, aesEncrypt.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write)) {
                cs.Write(plainText, 0, plainText.Length);
            }

            cipherText = ms.ToArray();
        }

        List<byte> cipherTextWithIv = new List<byte>();
        cipherTextWithIv.AddRange(aesEncrypt.IV);
        cipherTextWithIv.AddRange(cipherText);

        return Convert.ToBase64String(cipherTextWithIv.ToArray());
    }
    internal static string DecryptBase64StringToObjectString(string encryptedString) {
        System.Security.Cryptography.Aes aesDecrypt = System.Security.Cryptography.Aes.Create();

        return string.Empty;
    }
    #endregion Other Methods
}
