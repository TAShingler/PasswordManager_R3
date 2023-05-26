using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Utils;
internal static class FileOperations {
    #region Fields

    #endregion Fields

    #region Properties

    #endregion Properties

    #region Constructors

    #endregion Constructors

    #region Other Methods
    internal static bool DoesFileExist(string path) {
        System.IO.FileInfo fInfo = new(path);
        return fInfo.Exists;
    }
    internal static bool DoesDirectoryExist(string path) {
        if (string.IsNullOrWhiteSpace(path)) {
            //maybe throw exception
            return false;
        }

        System.IO.DirectoryInfo dInfo = new(path);

        return dInfo.Exists;
    }
    internal static long GetFileSize(string path) {
        System.IO.FileInfo fInfo = new(path);
        return fInfo.Length;
    }
    internal static void CreateDirectory(string path) {
        System.IO.Directory.CreateDirectory(path);
    }
    internal static void CreateFile(string path) {
        System.IO.FileStream fs = System.IO.File.Create(path);

        fs.Flush();
        fs.Dispose();
        fs.Close();
    }
    internal static void WriteToFile(string path, string dataToFile) {  //pass encrypted/unencrypted data to method
        System.IO.FileStream fs = System.IO.File.OpenWrite(path);
        System.IO.StreamWriter writer = new(fs);

        writer.Write(dataToFile);

        writer.Flush();
        writer.Dispose();
        writer.Close();

        fs.Flush();
        fs.Dispose();
        fs.Close();
    }
    internal static string ReadFromFile(string path) {
        System.IO.FileStream fs = System.IO.File.OpenRead(path);
        System.IO.StreamReader reader = new(fs);

        string dataFromFile = reader.ReadToEnd();

        reader.Dispose();
        reader.Close();

        fs.Flush();
        fs.Dispose();
        fs.Close();

        return dataFromFile;
    }
    internal static void DeleteFile(string path) {
        //will probably need to add try blocks
        System.IO.File.Delete(path);
    }

    //methods specific to PasswordManager_R3
    internal static bool DoesMasterPasswordExist(string path) { //will probably make a more sophisticated method to check for exisitng master pass...
        bool result = (DoesFileExist(path) == true) && (GetFileSize(path) > 0 == true);

        return result;
    }
    #endregion Other Methods
}