using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Utils;
internal static class Hasher {
    //static byte[] bytes = System.Security.Cryptography.RandomNumberGenerator.GetBytes(16);
    #region Private Fields
    private const char DELIMITER = ':';
    private const int SALT_SIZE = 16;   //in bytes; 128 bits
    private const int KEY_SIZE = 32;    //in bytes; 256 bits
    private const int ITERATIONS = 500000;
    private static readonly System.Security.Cryptography.HashAlgorithmName ALGORITHM_NAME = System.Security.Cryptography.HashAlgorithmName.SHA256;
    #endregion Private Fields

    #region Properties

    #endregion Properties

    #region Constructors

    #endregion Constructors

    #region Other Methods
    internal static string Hash(string input) {
        byte[] salt = System.Security.Cryptography.RandomNumberGenerator.GetBytes(SALT_SIZE);
        byte[] hash = System.Security.Cryptography.Rfc2898DeriveBytes.Pbkdf2(
            input,
            salt,
            ITERATIONS,
            ALGORITHM_NAME,
            KEY_SIZE
        );

        return string.Join(
            DELIMITER,
            Convert.ToHexString(hash),
            Convert.ToHexString(salt),
            ITERATIONS,
            ALGORITHM_NAME
        );
    }
    internal static bool Verify(string input, string hashString) {
        string[] segments = hashString.Split(DELIMITER);
        byte[] hash = Convert.FromHexString(segments[0]);
        byte[] salt = Convert.FromHexString(segments[1]);
        int iterations = int.Parse(segments[2]);
        System.Security.Cryptography.HashAlgorithmName algorithm = new System.Security.Cryptography.HashAlgorithmName(segments[3]);

        byte[] inputHash = System.Security.Cryptography.Rfc2898DeriveBytes.Pbkdf2(
            input,
            salt,
            iterations,
            algorithm,
            hash.Length
        );

        return System.Security.Cryptography.CryptographicOperations.FixedTimeEquals(inputHash, hash);
    }
    #endregion Other Methods
}
