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
    private int _attemptsRemaining;
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
    internal LockScreen_ViewModel(ViewModelBase parentVM, bool doesMasterPasswordExist = true) : base(parentVM) {
        UnlockDatabaseCommand = new Utils.DelegateCommand(OnUnlockDatabaseCommand);
        CloseWindowCommand = new Utils.DelegateCommand(OnCloseWindowCommand);

        //get current user SID for decryption key
        byte[] sidBinaryForm = new byte[256 / 8];
        System.Security.Principal.WindowsIdentity.GetCurrent().User.GetBinaryForm(sidBinaryForm, 0);
        Utils.EncryptionTools.Key = sidBinaryForm;

        FIRST_RUN = !doesMasterPasswordExist;

        if (doesMasterPasswordExist == false) {
            OutputMessage = "Please enter a password to set the master password.";
        }
    }
    #endregion Constructors

    #region Event Handlers
    private void OnUnlockDatabaseCommand(object obj) {
        /* if (obj.GetType().Equals(typeof(string)) == false) {
            //throw new Exception();
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

        if (FIRST_RUN == true) {
            CreateMasterPassword(objAsString);
        }

        TryUnlockDatabase(objAsString); */
        //var result = 
        ((App)App.Current).AppVariables.DatabaseConnection = new Utils.DatabaseOperations();
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
    private void TryUnlockDatabase(string password) {
        string passwordFromFile = Utils.FileOperations.ReadFromFile(string.Empty);
        string storedPasswordHash = Utils.EncryptionTools.DecryptBase64StringToObjectString(passwordFromFile); //ReadMasterPasswordFromFile();

        //compare password passed to method with stored hash of master password
        if (Utils.Hasher.Verify(password, storedPasswordHash) == false) {
            if (_attemptsRemaining > 0) {
                //decrement wrong attempts count and display error message to user
                OutputMessage = $"Entered password is not correct.\n{--_attemptsRemaining} attempts remaining.";
                return;
            } else {
                //display error message and delete database
                OutputMessage = $"Entered password is not correct.\nOut of attempts; deleteing database.";
                //exit app or return to initial setup?
                DeleteDatabase();   //might put in different class, app.cs maybe (?)
                return;
            }
        }

        //split stored hash into parts
        string[] hashedPasswordParts = storedPasswordHash.Split(':');

        //set encryption key for Encryptor/Decryptor equal to hash of master password
        Utils.EncryptionTools.Key = Convert.FromHexString(hashedPasswordParts[0]);

        //reset wrong attempts count
        //_wrongAttemptsCount = MAX_ATTEMPTS;

        //raise event -- pass something?
        DatabaseUnlocked?.Invoke();

        //return false;
    }
    private void DeleteDatabase() { //might move this method to app.cs and process there; possibly in MainWindow
        Utils.FileOperations.DeleteFile(string.Empty);
    }
    private void CreateMasterPassword(string passwordToHash) {   //might move this app.cs
        string hashedPassword = Utils.Hasher.Hash(passwordToHash);

        //might move FileStream to WriteMasterPasswordToFile()
        //System.IO.FileStream fs = System.IO.File.Open(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\PasswordManager_R3\Data\master_pass.dat", System.IO.FileMode.Append);
        
        //encrypt
        string encryptedHashedPassword = Utils.EncryptionTools.EncryptObjectStringToBase64String(hashedPassword);

        //WriteMasterPasswordToFile(hashedPassword);
        Utils.FileOperations.WriteToFile(string.Empty, encryptedHashedPassword);

        //fs.Dispose();
        //fs.Close();

        //set EncryptionTools key to hashed password as byte array
    }
    #endregion OTHER METHODS
}
