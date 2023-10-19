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
        System.IO.File.Delete(path);
    }

    //methods specific to PasswordManager_R3
    internal static bool DoesMasterPasswordExist(string path) { //will probably make a more sophisticated method to check for exisitng master pass...
        bool result = (DoesFileExist(path) == true) && (GetFileSize(path) > 0 == true);

        return result;
    }
    //methods for database backup on CRUD ops... - will probably move to FileOperations class
    internal static void CreateDatabaseBackup(int backupFilesLength) {
        //exit method if Database connection is null -- probably should check sooner; might be able to remove
        //if (AppVariables.DatabaseConnection == null) {
        //    //throw exception
        //    return;
        //}

        //read bytes from Database file
        var dbBytes = System.IO.File.ReadAllBytes(((App)App.Current).DatabaseOps.DatabaseFilePath);

        //create file path to save the the database backup to
        string backupFilePath = ((App)App.Current).AppVariables.BackupLocation + @"\" + _backupFileName + $"_{(backupFilesLength + 1):000}.bak";

        //write bytes to backup file
        System.IO.File.WriteAllBytes(backupFilePath, dbBytes);
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

    //Database backup method
    internal static void DatabaseBackup() {
        if (DoesDirectoryExist(((App)App.Current).AppVariables.BackupLocation) == false) {
            //throw excception -- try to provide user usefule information
            return;
        }

        //get DB backup files from backup files directory
        var backupFiles = System.IO.Directory.GetFiles(((App)App.Current).AppVariables.BackupLocation);

        //if backup files count is greater than the value for the amount of backup files to retain, delete first file in array; subsequent files in array will be decremented by 1 (e.g., BackupFile002 -> BackupFile001)
        if (backupFiles.Count() >= ((App)App.Current).AppVariables.AutoBackupCount) {
            //delete first DB backup file
            DeleteDatabaseBackup(backupFiles[0]);

            //rename subsequent files in backupFiles array
            RenameDatabaseBackup(backupFiles[1..]);
        }

        CreateDatabaseBackup(backupFiles.Length);
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