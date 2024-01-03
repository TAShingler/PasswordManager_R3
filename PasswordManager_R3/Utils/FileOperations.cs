using PasswordManager_R3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Utils;
internal static class FileOperations {
    #region Fields
    private static readonly string _backupFileName = "AutoBackup";
    private static readonly string _appSettingsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PMR3";
    private static readonly string _appSettingsFileName = "appSettings";
    #endregion Fields

    #region Properties
    internal static string AppSettingsDirectory {
        get => _appSettingsDirectory;
    }
    internal static string AppSettingsFileName {
        get => _appSettingsFileName;
    }
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
    internal static void CreateEmptyFile(string path) {
        System.IO.FileStream fs = System.IO.File.Create(path);

        fs.Flush();
        fs.Dispose();
        fs.Close();
    }
    internal static void WriteToFile(string path, string dataToFile, System.IO.FileMode fileMode = System.IO.FileMode.Open) {  //pass encrypted/unencrypted data to method
        System.IO.FileStream fs = System.IO.File.Open(path, fileMode, System.IO.FileAccess.Write);
        System.IO.StreamWriter writer = new(fs);

        writer.Write(dataToFile);

        writer.Flush();
        writer.Dispose();
        writer.Close();

        //fs.Flush();
        fs.Dispose();
        fs.Close();
    }
    internal static string ReadFromFile(string path) {
        System.IO.FileStream fs = System.IO.File.OpenRead(path);
        System.IO.StreamReader reader = new(fs);

        string dataFromFile = reader.ReadToEnd();

        reader.Dispose();
        reader.Close();

        //fs.Flush();
        fs.Dispose();
        fs.Close();

        return dataFromFile;
    }
    internal static void DeleteFile(string path) {
        //will probably need to add try blocks
        try {
            System.IO.File.Delete(path);
        } catch(Exception e) {
            System.Diagnostics.Debug.WriteLine($"Failed to delete file at {path}. Exception: {e}");
        }
    }

    //methods specific to PasswordManager_R3
    internal static bool DoesMasterPasswordExist(string path) { //will probably make a more sophisticated method to check for exisitng master pass...
        bool result = (DoesFileExist(path) == true) && (GetFileSize(path) > 0 == true);

        return result;
    }
    //methods for database backup on CRUD ops... - will probably move to FileOperations class
    internal static void CreateDatabaseBackup(string backupFilePath) { //int backupFilesLength) {
        //exit method if Database connection is null -- probably should check sooner; might be able to remove
        //if (AppVariables.DatabaseConnection == null) {
        //    //throw exception
        //    return;
        //}





        //read master pass from file - already encrypted
        var masterPassFromFile = FileOperations.ReadFromFile(Utils.FileOperations.AppSettingsDirectory + @"\mp.dat");

        //set encryption/decryption key equal to hash of current user SID
        byte[] sidBinaryForm = new byte[256 / 8];
        System.Security.Principal.WindowsIdentity.GetCurrent().User.GetBinaryForm(sidBinaryForm, 0);
        Utils.EncryptionTools.Key = sidBinaryForm;

        //decrypt master pass hash - to reset EncryptionTool.Key after backup encryption
        var masterPassFromFileDecrypted = EncryptionTools.DecryptBase64StringToObjectString(masterPassFromFile);

        //read database bytes from file
        var databaseBytesFromFile = System.IO.File.ReadAllBytes(((App)App.Current).DatabaseOps.DatabaseFilePath);
        //convert to base64
        var databaseBytesFromFileAsBase64String = Convert.ToHexString(databaseBytesFromFile);

        //encrypt database string
        var databaseBytesFromFileEncrypted = EncryptionTools.EncryptObjectStringToBase64String(databaseBytesFromFileAsBase64String);

        //combine byte arrays
        var backupBytes = masterPassFromFile + ':' + databaseBytesFromFileEncrypted;
        System.Diagnostics.Debug.WriteLine($"backupBytes: {backupBytes}");
        //convert to base64 string
        //var backupBytesAsBase64String = Convert.ToBase64String(backupBytes);

        //encrypt backup byte array
        //var backupBytesEncrypted = EncryptionTools.EncryptObjectStringToBase64String(backupBytes);  //going to need encryption solution that utilizes SID or some other value for key; will need for decryption as well

        //create file path to save the the database backup to
        //string backupFilePath = ((App)App.Current).AppVariables.BackupLocation + @"\" + _backupFileName + $"_{(backupFilesLength + 1):000}.bak";

        //write bytes to backup file
        FileOperations.WriteToFile(backupFilePath, backupBytes, System.IO.FileMode.Create);


        //set key to master password for encryption/decryption
        //split stored hash into parts
        string[] hashedPasswordParts = masterPassFromFileDecrypted.Split(':');

        //set encryption key for Encryptor/Decryptor equal to hash of master password
        Utils.EncryptionTools.Key = Convert.FromHexString(hashedPasswordParts[0]);






        /* OLD
        //read bytes from Database file
        var dbBytesAsByteArray = System.IO.File.ReadAllBytes(((App)App.Current).DatabaseOps.DatabaseFilePath);
        var dbBytesAsBase64String = Convert.ToBase64String(dbBytesAsByteArray);
        System.Diagnostics.Debug.WriteLine($"dbBytes.Length = {dbBytesAsByteArray.Length}");
        System.Diagnostics.Debug.WriteLine($"dbBytesAsBase64String.Length = {dbBytesAsBase64String.Length} bytes : as string = {dbBytesAsBase64String}");

        //create file path to save the the database backup to
        string backupFilePath = ((App)App.Current).AppVariables.BackupLocation + @"\" + _backupFileName + $"_{(backupFilesLength + 1):000}.bak";

        //write bytes to backup file
        System.IO.File.WriteAllBytes(backupFilePath, bakBytes);
        */
    }
    internal static void RenameDatabaseBackup(string[] filePaths) {
        //rename files in DB backups directory using the System.IO.File.Move() method
        for (int i = 0; i <= filePaths.Length; i++) {
            System.IO.File.Move(filePaths[i], $"{filePaths[i][..^8]}_{(i+1):000}.txt");
            //System.IO.File.Move(filePaths[i], $"{filePaths[i].Substring(0, filePaths[i].Length - 8)}_{(i + 1):000}.txt");
        }

        //System.IO.FileInfo fi = new(filePaths[0]);
        //fi.MoveTo(backupFiles[0].Substring(0, backupFiles[0].Length-3),
    }
    internal static void DeleteDatabaseBackup(string filePath) {
        if (DoesFileExist(filePath) == false) {
            //throw exception
            return;
        }

        DeleteFile(filePath);
    }

    //Database backup methods
    internal static void DatabaseBackup() {//async Task<bool> DatabaseBackup() {
        if (DoesDirectoryExist(((App)App.Current).AppVariables.BackupLocation) == false) {
            //throw excception -- try to provide user usefule information
            return;
        }

        //get DB backup files from backup files directory
        var backupFiles = System.IO.Directory.GetFiles(((App)App.Current).AppVariables.BackupLocation);

        //if backup files count is greater than the value for the amount of backup files to retain, delete first file in array; subsequent files in array will be decremented by 1 (e.g., BackupFile002 -> BackupFile001)
        if (backupFiles.Count() >= ((App)App.Current).AppVariables.AutoBackupCount) {
            System.Diagnostics.Debug.WriteLine("backupFiles.Count() >= ((App)App.Current).AppVariables.AutoBackupCount");
            //delete first DB backup file
            DeleteDatabaseBackup(backupFiles[0]);

            //rename subsequent files in backupFiles array
            RenameDatabaseBackup(backupFiles[1..]);
        }

        //if (((App)App.Current).DatabaseOps.DatabaseConnection != null) {
        //    ((App)App.Current).DatabaseOps.DisposeConnection();

        //CreateDatabaseBackup(backupFiles.Length);
        CreateDatabaseBackup(((App)App.Current).AppVariables.BackupLocation + @"\" + _backupFileName + $"_{(backupFiles.Length + 1):000}.bak");

        //    ((App)App.Current).DatabaseOps.CreateConnection();
        //} else {
        //    CreateDatabaseBackup(backupFiles.Length);
        //}

        return;// true;
    }
    internal static void DatabaseBackup(string saveLocation) {
        CreateDatabaseBackup(saveLocation);
    }

    //Database restore method
    internal static void RestoreDatabase(string restoreLocation) {
        //do something
        //throw new NotImplementedException("RestoreDatabase() not yet implemented...");

        //FOR TESTING PURPOSES
        //var currentEncryptKey = EncryptionTools.Key;


        //read data from backup file
        var dataFromBackupFile = ReadFromFile(restoreLocation);

        //split data into encrypted master pass string and encrypted database string -- ':' is separator
        var dataFromBackupFileStrings = dataFromBackupFile.Split(':');
        System.Diagnostics.Debug.WriteLine($"dataFromBackupFileStrings.Length = {dataFromBackupFileStrings.Length}");

        //set EncryptionTools.Key to current user SID
        byte[] sidBinaryForm = new byte[256 / 8];
        System.Security.Principal.WindowsIdentity.GetCurrent().User.GetBinaryForm(sidBinaryForm, 0);
        Utils.EncryptionTools.Key = sidBinaryForm;

        //decrypt database string
        var databaseStringDecrypted = EncryptionTools.DecryptBase64StringToObjectString(dataFromBackupFileStrings[1]);
        System.Diagnostics.Debug.WriteLine($"master pass string from backup file: {dataFromBackupFileStrings[0]}\n\tas Hex string: {Convert.ToHexString(Convert.FromBase64String(dataFromBackupFileStrings[0]))}");
        System.Diagnostics.Debug.WriteLine($"databaseStringDecrypted = {databaseStringDecrypted}");

        //convert database to byte array from hex string
        var databaseStringAsByteArray = Convert.FromHexString(databaseStringDecrypted);

        //try overwriting current master password if file exists, or create new master password with retrieved string -- might need to delete existing file
        try {
            WriteToFile(Utils.FileOperations.AppSettingsDirectory + @"\mp.dat", dataFromBackupFileStrings[0], System.IO.FileMode.Create);
        } catch(Exception ex) {
            System.Diagnostics.Debug.WriteLine($"Exception caught while trying to save retrieved master password\n\tex.ToString = {ex.ToString}");
        }

        //try overwriting current database if file exists, or create new with retrieved string -- might need to delete existing file
        try {
            System.IO.File.WriteAllBytes(((App)App.Current).DatabaseOps.DatabaseFilePath, databaseStringAsByteArray);
        } catch (Exception ex) {
            System.Diagnostics.Debug.WriteLine($"Exception caught while trying to save retrieved database string\n\tex.ToString = {ex.ToString}");
        }

        //set EncryptionTools.Key to retrieved master password
        var masterPassFromFileDecrypted = EncryptionTools.DecryptBase64StringToObjectString(dataFromBackupFileStrings[0]);
        var masterPassFromFileDecryptedStrings = masterPassFromFileDecrypted.Split(':');
        Utils.EncryptionTools.Key = Convert.FromHexString(masterPassFromFileDecryptedStrings[0]);
        //Utils.EncryptionTools.Key = currentEncryptKey;



        /* OLD
        //read bytes from database file
        var backupBytesEncrypted = ReadFromFile(restoreLocation);
        System.Diagnostics.Debug.WriteLine($"backupBytesEncrypted = {backupBytesEncrypted}");

        var backupBytesDecrypted = EncryptionTools.DecryptBase64StringToObjectString(backupBytesEncrypted);
        System.Diagnostics.Debug.WriteLine($"backupBytesDecrypted = {backupBytesDecrypted}");

        //split strings
        var backupBytesDecryptedStrings = backupBytesDecrypted.Split('\t'); //  \u002c
        System.Diagnostics.Debug.WriteLine($"strings in backupBytesDecryptedStrings:");
        foreach (string s in backupBytesDecryptedStrings) {
            System.Diagnostics.Debug.WriteLine($"\ts = {s}");
            System.Diagnostics.Debug.WriteLine($"\t\tlength = {s.Length}");
        }

        //write master pass to file
        WriteToFile(Utils.FileOperations.AppSettingsDirectory + @"\mp.dat", backupBytesDecryptedStrings[0], System.IO.FileMode.Create);

        //write database to file
        System.IO.File.WriteAllBytes(((App)App.Current).DatabaseOps.DatabaseFilePath, Convert.FromBase64String(backupBytesDecrypted));

        //write bytes to destination
        //System.IO.File.WriteAllBytes(((App)App.Current).DatabaseOps.DatabaseFilePath, dbBytes);

        return;
        */
    }

    //method to write AppVariables values to file
    internal static void WriteAppVariablesToFile() {
        //check that directory exists
        if (DoesDirectoryExist(_appSettingsDirectory) == false) {
            //create directory
            CreateDirectory(_appSettingsDirectory);
        }

        //check that file exists
        if (DoesFileExist(_appSettingsDirectory) == false) {
            //create file
            CreateEmptyFile(_appSettingsDirectory + @"\" + _appSettingsFileName + ".dat");
        }

        //serialize AppVariables
        var serializedObj = Newtonsoft.Json.JsonConvert.SerializeObject(((App)App.Current).AppVariables);

        //write to file
        WriteToFile(_appSettingsDirectory + @"\" + _appSettingsFileName + ".dat", serializedObj);
    }
    //method to read AppVariables values from file
    internal static string ReadAppVariablesFromFile() {
        //check that directory exists
        if (DoesDirectoryExist(_appSettingsDirectory) == false) {
            //error message
            throw new Exception();  //catch in calling class
        }

        //check that file exists
        if (DoesFileExist(_appSettingsDirectory + @"\" + _appSettingsFileName + ".dat") == false) {
            //error message
            throw new Exception();  //catch in calling class
        }

        //read data from file
        return ReadFromFile(_appSettingsDirectory + @"\" + _appSettingsFileName + ".dat");

        //return "";
    }
    //private static void SetAppVariablesValues() { }
    #endregion Other Methods
}