using PasswordManager_R3.Models;
using System;
using System.Windows;

namespace PasswordManager_R3.ViewModels;
internal class LockScreen_ViewModel : ViewModelBase {
    #region Fields
    //private const string NULL_WHITESPACE_ERROR = "No password entered.\nPlease enter a valid password.";
    //private const string INCORRECT_PASSWORD_ERROR = $"Entered password is not correct.";
    private readonly bool FIRST_RUN;
    private readonly int MAX_ATTEMPTS;
    private string _outputMessage = string.Empty;
    private int _attemptsRemaining = 10;
    #endregion Fields

    #region Delegates and Events
    //Delegates
    public delegate void UnlockDatabaseHandler();    //might add event args later...
    public delegate void CloseWindowHandler(object obj);
    //Events
    public event UnlockDatabaseHandler? DatabaseUnlocked;
    public event CloseWindowHandler? WindowClosed;
    #endregion Delegates and Events

    #region Properties
    public Utils.DelegateCommand UnlockDatabaseCommand { get; set; }
    public Utils.DelegateCommand CloseWindowCommand { get; set; }

    public string OutputMessage {
        get { return _outputMessage; }
        set {
            _outputMessage = value;
            OnPropertyChanged(nameof(OutputMessage));
        }
    }
    #endregion Properties

    #region Constructors
    //internal LockScreen_ViewModel(ViewModelBase parentVM) : base(parentVM) {
    //    UnlockDatabaseCommand = new Utils.DelegateCommand(OnUnlockDatabaseCommand);
    //    CloseWindowCommand = new Utils.DelegateCommand(OnCloseWindowCommand);

    //    //get current user SID for decryption key
    //    byte[] sidBinaryForm = new byte[256 / 8];
    //    System.Security.Principal.WindowsIdentity.GetCurrent().User.GetBinaryForm(sidBinaryForm, 0);
    //    Utils.EncryptionTools.Key = sidBinaryForm;
    //}
    internal LockScreen_ViewModel(ViewModelBase parentVM) : base(parentVM) {
        UnlockDatabaseCommand = new Utils.DelegateCommand(OnUnlockDatabaseCommand);
        CloseWindowCommand = new Utils.DelegateCommand(OnCloseWindowCommand);

        //get current user SID for decryption key
        byte[] sidBinaryForm = new byte[256 / 8];
        System.Security.Principal.WindowsIdentity.GetCurrent().User.GetBinaryForm(sidBinaryForm, 0);
        Utils.EncryptionTools.Key = sidBinaryForm;

        FIRST_RUN = !Utils.FileOperations.DoesMasterPasswordExist(Utils.FileOperations.AppSettingsDirectory + @"\mp.dat");

        if (FIRST_RUN == true) {
            OutputMessage = "Please enter a password to set the master password.";
        }
    }
    #endregion Constructors

    #region Event Handlers
    private void OnUnlockDatabaseCommand(object obj) {
        if (obj.GetType().Equals(typeof(string)) == false) {
            //throw new Exception();    ?
            return;
        }

        string objAsString = (string)obj;

        //is password null or whitespace?
        if (string.IsNullOrWhiteSpace(objAsString)) {
            //display error message to user
            OutputMessage = "Entered password is not valid.\nPlease enter a valid password.";
            return;
        }

        //Add check for specific criteria?

        if (FIRST_RUN == false) {
            try {
                UnlockDatabase(objAsString);
            } catch(Exception ex) {
                //error message
                return;
            }
        } else {
            CreateMasterPassword(objAsString);

            //create db tables?
            ((App)App.Current).DatabaseOps.CreateConnection();
            //if (Utils.FileOperations.DoesDirectoryExist(((App)App.Current).DatabaseOps.DatabaseFolderPath) == false) { //might not be necessary
            //    Utils.FileOperations.CreateDirectory(((App)App.Current).DatabaseOps.DatabaseFolderPath);
            //}
            //if (Utils.FileOperations.DoesFileExist(((App)App.Current).DatabaseOps.DatabaseFilePath) == false) {
            //    Utils.FileOperations.CreateEmptyFile(((App)App.Current).DatabaseOps.DatabaseFilePath);
            //}
            ((App)App.Current).DatabaseOps.CreateTables();
            ((App)App.Current).DatabaseOps.CheckForTables();
        }

        System.Diagnostics.Debug.WriteLine("Is App.Current.DatabaseOps == null? " + (((App)App.Current).DatabaseOps == null ? "true" : "false"));
        System.Diagnostics.Debug.WriteLine($"Does DB directory exist at {Utils.FileOperations.DoesDirectoryExist(((App)App.Current).DatabaseOps.DatabaseFolderPath)}? " + Utils.FileOperations.DoesDirectoryExist(((App)App.Current).DatabaseOps.DatabaseFolderPath));
        System.Diagnostics.Debug.WriteLine($"Does DB file exist at {Utils.FileOperations.DoesFileExist(((App)App.Current).DatabaseOps.DatabaseFilePath)}? " + Utils.FileOperations.DoesFileExist(((App)App.Current).DatabaseOps.DatabaseFilePath));

        //var result = 
        /*((App)App.Current).DatabaseConnection = new Utils.DatabaseOperations();*/
        DatabaseUnlocked?.Invoke();
        //System.Diagnostics.Debug.WriteLine("UserPassword: " + obj.ToString());
    }
    private void OnCloseWindowCommand(object obj) {
        //System.Diagnostics.Debug.WriteLine(obj.ToString());
        //raise event to MainWindow
        WindowClosed?.Invoke(obj);
        //if (DatabaseUnlocked != null) {
        //    DatabaseUnlocked.Invoke(obj);
        //}
    }
    #endregion Event Handlers

    #region OTHER METHODS
    private void UnlockDatabase(string password) {
        string passwordFromFile = Utils.FileOperations.ReadFromFile(Utils.FileOperations.AppSettingsDirectory + @"\mp.dat");
        string storedPasswordHash = Utils.EncryptionTools.DecryptBase64StringToObjectString(passwordFromFile); //ReadMasterPasswordFromFile();

        //compare password passed to method with stored hash of master password
        if (Utils.Hasher.Verify(password, storedPasswordHash) == false) {
            System.Diagnostics.Debug.WriteLine("Utils.Hasher.Verify(password, storedPasswordHash) == " + Utils.Hasher.Verify(password, storedPasswordHash));
            if (_attemptsRemaining > 0) {
                //decrement wrong attempts count and display error message to user
                OutputMessage = $"Entered password is not correct.\n{--_attemptsRemaining} attempts remaining.";
                //return;
                throw new Exception();
            } else {
                //display error message and delete database
                OutputMessage = $"Entered password is not correct.\nOut of attempts; deleteing database.";
                //exit app or return to initial setup?
                DeleteDatabase();   //might put in different class, app.cs maybe (?)
                //return;
                throw new Exception();
            }
        }

        //split stored hash into parts
        string[] hashedPasswordParts = storedPasswordHash.Split(':');

        //set encryption key for Encryptor/Decryptor equal to hash of master password
        Utils.EncryptionTools.Key = Convert.FromHexString(hashedPasswordParts[0]);

        System.Diagnostics.Debug.WriteLine("passwordFromFile == " + passwordFromFile);
        System.Diagnostics.Debug.WriteLine("storedPasswordHash == " + storedPasswordHash);
        for (int i=0;i<hashedPasswordParts.Length;i++) {
            System.Diagnostics.Debug.WriteLine($"hashedPasswordParts[{i}] == " + hashedPasswordParts[i]);
        }
        //reset wrong attempts count
        //_wrongAttemptsCount = MAX_ATTEMPTS;

        //raise event -- pass something?
        //DatabaseUnlocked?.Invoke();

        //return false;
    }
    private void DeleteDatabase() { //might move this method to app.cs and process there; possibly in MainWindow
        //check that DB file directory exists
        if (Utils.FileOperations.DoesDirectoryExist(((App)App.Current).DatabaseOps.DatabaseFolderPath) == false) {
            //error message?
            return;
        }

        //check that DB file exists
        if (Utils.FileOperations.DoesFileExist(((App)App.Current).DatabaseOps.DatabaseFilePath + ".db")) {
            //error message
            return;
        }

        //delete DB file
        Utils.FileOperations.DeleteFile(((App)App.Current).DatabaseOps.DatabaseFilePath + ".db");
    }
    private void CreateMasterPassword(string passwordToHash) {   //might move this app.cs
        string hashedPassword = Utils.Hasher.Hash(passwordToHash);
        System.Diagnostics.Debug.WriteLine("hashed pass: " + hashedPassword);

        //might move FileStream to WriteMasterPasswordToFile()
        //System.IO.FileStream fs = System.IO.File.Open(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\PasswordManager_R3\Data\master_pass.dat", System.IO.FileMode.Append);
        
        //encrypt
        string encryptedHashedPassword = Utils.EncryptionTools.EncryptObjectStringToBase64String(hashedPassword);

        //WriteMasterPasswordToFile(hashedPassword);
        Utils.FileOperations.CreateEmptyFile(Utils.FileOperations.AppSettingsDirectory + @"\mp.dat");
        Utils.FileOperations.WriteToFile(Utils.FileOperations.AppSettingsDirectory + @"\mp.dat", encryptedHashedPassword);

        //fs.Dispose();
        //fs.Close();

        //split stored hash into parts
        string[] hashedPasswordParts = hashedPassword.Split(':');

        //set EncryptionTools key to hashed password as byte array
        Utils.EncryptionTools.Key = Convert.FromHexString(hashedPasswordParts[0]);
    }
    #endregion OTHER METHODS
}
