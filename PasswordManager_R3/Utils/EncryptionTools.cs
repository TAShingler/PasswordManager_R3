using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Utils;
internal static class EncryptionTools {
    #region Fields
    private const int KEY_SIZE = 32;    //key size in bytes
    //private const int IV_SIZE = KEY_SIZE / 8;
    private static byte[]? _key = new byte[KEY_SIZE];    //may need to clear when database is locked...
    #endregion Fields

    #region Properties
    internal static byte[]? Key {
        //get { return _key; } //-- might use in future
        set { _key = value; }//if (_key != null) { System.Diagnostics.Debug.WriteLine("Key = " + Convert.ToHexString(_key)); } else { System.Diagnostics.Debug.WriteLine("Key is null "); } }
    }
    #endregion Proeprties

    #region Constructors

    #endregion Constructors

    #region Other Methods
    //accepts seriaized obj as string and encrypts to byte array; returns encrypted byte array as string (?)
    internal static string EncryptObjectStringToBase64String(string serializedObj) {
        byte[] cipherText = null;   //byte array for encrypted plain text

        //Aes object for encryption
        System.Security.Cryptography.Aes aesEncrypt = System.Security.Cryptography.Aes.Create();

        //set encryptor key
        aesEncrypt.Key = _key;

        //generate random bytes for Initialization Vector
        aesEncrypt.IV = System.Security.Cryptography.RandomNumberGenerator.GetBytes(aesEncrypt.BlockSize / 8);

        //convert string to byte array to encrypt
        byte[] plainText = Encoding.Unicode.GetBytes(serializedObj);

        //encrypt
        using (System.IO.MemoryStream ms = new System.IO.MemoryStream()) {
            using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, aesEncrypt.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write)) {
                //encrypt bytes and write to CryptoStream, which are sent to MemoryStream
                cs.Write(plainText, 0, plainText.Length);
            }

            //copy encrypted bytes from MemoryStream
            cipherText = ms.ToArray();
        }

        //List to combine IV and encrypted bytes for storage
        List<byte> cipherTextWithIv = new List<byte>();
        //add IV and encrypted bytes to List
        cipherTextWithIv.AddRange(aesEncrypt.IV);
        cipherTextWithIv.AddRange(cipherText);

        //return fully encrypted bytes to calling method as string
        return Convert.ToBase64String(cipherTextWithIv.ToArray());
    }
    internal static string DecryptBase64StringToObjectString(string encryptedString) {
        //string jsonString;          //json string to be returned to calling method -- prob. don't nee
        byte[] plainText = null;    //byte array for decrypted plain text

        //Aes object for decryption
        System.Security.Cryptography.Aes aesDecrypt = System.Security.Cryptography.Aes.Create();
        aesDecrypt.Key = _key;

        //convert encrypted base 64 string to byte array
        byte[] cipherTextWithIv = Convert.FromBase64String(encryptedString);

        //get IV from encrypted string as byte array
        aesDecrypt.IV = cipherTextWithIv.AsSpan(0, (aesDecrypt.BlockSize / 8)).ToArray();

        //get cipher text from encrypted string as byte array
        byte[] cipherText = cipherTextWithIv.AsSpan((aesDecrypt.BlockSize / 8), cipherTextWithIv.Length - (aesDecrypt.BlockSize / 8)).ToArray();

        //decrypt
        using (System.IO.MemoryStream ms = new System.IO.MemoryStream()) {
            using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, aesDecrypt.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write)) {
                //decrypt encrypted bytes and write to CryptoStream, which are sent to MemoryStream
                cs.Write(cipherText, 0, cipherText.Length);
            }

            //copy decrypted bytes from MemoryStream
            plainText = ms.ToArray();
        }

        //return decrypted bytes as string to calling method
        return Encoding.Unicode.GetString(plainText);
    }
    #endregion Other Methods
}
