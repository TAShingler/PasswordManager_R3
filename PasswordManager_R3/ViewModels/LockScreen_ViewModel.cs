using ABI.System.Collections.Generic;
using PasswordManager_R3.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

namespace PasswordManager_R3.ViewModels;
internal class LockScreen_ViewModel : ViewModelBase {
    #region Fields
    //private const string NULL_WHITESPACE_ERROR = "No password entered.\nPlease enter a valid password.";
    //private const string INCORRECT_PASSWORD_ERROR = $"Entered password is not correct.";
    private readonly bool DOES_MP_EXIST = true;
    private readonly bool DOES_DB_EXIST = false;
    private readonly int MAX_ATTEMPTS;
    private string _outputMessage = string.Empty;
    private int _attemptsRemaining = ((App)App.Current).AppVariables.UnlockAttempts;
    private readonly object _viewState;
    private string _newMP;
    private string _oldMP;
    #endregion Fields

    #region Delegates and Events
    //Delegates
    public delegate void UnlockDatabaseHandler();    //might add event args later...
    public delegate void CloseWindowHandler(object obj);
    internal delegate void ConfirmSetMasterPasswordHandler();
    internal delegate void CancelSetMasterPasswordHandler();
    //Events
    public event UnlockDatabaseHandler? DatabaseUnlocked;
    public event CloseWindowHandler? WindowClosed;
    internal event ConfirmSetMasterPasswordHandler ConfirmSetMasterPassword;
    internal event CancelSetMasterPasswordHandler? CancelSetMasterPassword;
    #endregion Delegates and Events

    #region Properties
    public Enums.LockScreenState ViewState {
        get => (Enums.LockScreenState)_viewState;
    }
    public Utils.DelegateCommand? UnlockDatabaseCommand { get; set; }
    public Utils.DelegateCommand? CloseWindowCommand { get; set; }
    //public Utils.DelegateCommand? ConfirmSetNewPasswordCommand { get; set; }
    public Utils.DelegateCommand? CancelSetNewPasswordCommand { get; set; }

    public string OutputMessage {
        get { return _outputMessage; }
        set {
            _outputMessage = value;
            OnPropertyChanged(nameof(OutputMessage));
        }
    }
    public string NewMP {
        get => _newMP;
        set {
            _newMP = value;
            OnPropertyChanged(nameof(NewMP));
        }
    }
    public string OldMP {
        get => _oldMP;
        set {
            _oldMP = value;
            OnPropertyChanged(nameof(OldMP));
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
    internal LockScreen_ViewModel(ViewModelBase parentVM, object viewState) : base(parentVM) {
        System.Diagnostics.Debug.WriteLine($"viewState == {viewState}");
        _viewState = viewState;
        UnlockDatabaseCommand = new Utils.DelegateCommand(OnUnlockDatabaseCommand);
        CloseWindowCommand = new Utils.DelegateCommand(OnCloseWindowCommand);
        //ConfirmSetNewPasswordCommand = new(OnConfirmSetNewPasswordCommand);
        CancelSetNewPasswordCommand = new(OnCancelSetNewPasswordCommand);

        //get current user SID for decryption key
        byte[] sidBinaryForm = new byte[256 / 8];
        System.Security.Principal.WindowsIdentity.GetCurrent().User.GetBinaryForm(sidBinaryForm, 0);
        Utils.EncryptionTools.Key = sidBinaryForm;

        DOES_MP_EXIST = Utils.FileOperations.DoesMasterPasswordExist(Utils.FileOperations.AppSettingsDirectory + @"\mp.dat");
        DOES_DB_EXIST = Utils.FileOperations.DoesFileExist(((App)App.Current).DatabaseOps.DatabaseFilePath);

        //might need to change to conform with ViewState value
        if (DOES_MP_EXIST == false) {
            OutputMessage = "Please enter a password to set the master password.";

            if (DOES_DB_EXIST == true) {
                OutputMessage = "The stored master password has been deleted.\nPlease enter a new master password.";
                DeleteDatabase();
            }
        } else {
            if (DOES_DB_EXIST == false)
                OutputMessage = "Database does not exist.\nA new database has been created.";
        }
    }
    //internal LockScreen_ViewModel(ViewModelBase parentVM,)
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

        if (DOES_MP_EXIST == true) {
            try {
                UnlockDatabase(objAsString);
            } catch(Exception ex) {
                //error message
                MessageBox.Show(ex.Message);
                System.Diagnostics.Debug.WriteLine($"objAsString = {objAsString}");
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
    //private void OnConfirmSetNewPasswordCommand(object obj) {
    internal void ConfirmSetNewPassword(string oldMP, string newMP) {
        //System.Diagnostics.Debug.WriteLine($"OnConfirmSetNewPasswordCommand().obj == {obj}");

        //hash old & new passwords
        var oldMpHash = Utils.Hasher.Hash(oldMP);
        var newMpHash = Utils.Hasher.Hash(newMP);

        //verify that old + new password are the same -- hash first?
        //if (Utils.Hasher.Verify(oldMpHash, newMpHash) == false) {
        //    //display error message
        //    OutputMessage = "The entered passwords do not match.";
        //    return;
        //}

        //retrieve stored password hash
        string pwFromFile = Utils.FileOperations.ReadFromFile(Utils.FileOperations.AppSettingsDirectory + @"\mp.dat");
        string storedPwHash = Utils.EncryptionTools.DecryptBase64StringToObjectString(pwFromFile);
        System.Diagnostics.Debug.WriteLine($"oldMpHash = {oldMpHash}\nstoredPwHash = {storedPwHash}");
        //check old password matches stored password
        if (Utils.Hasher.Verify(oldMP, storedPwHash) == false) {
            //display error message
            OutputMessage = "Old password entered does not match current master password.";
            return;
        }

        //create DB backup -- sets encrypt/decrypt key to the current master pass
        Utils.FileOperations.DatabaseBackup();

        /*
        //split stored hash into parts
        string[] hashedPwParts = storedPwHash.Split(':');

        //set encryption key for Encryptor/Decryptor equal to hash of stored password
        Utils.EncryptionTools.Key = Convert.FromHexString(hashedPwParts[0]);
        */

        //decrypt database objects - store in Collection
        ((App)App.Current).DatabaseOps.CreateConnection();
        var GroupsFromDB = ((App)App.Current).DatabaseOps.RetrieveGroupsData();
        var RecordsFromDB = ((App)App.Current).DatabaseOps.RetrieveRecordsData();

        //set encrypt key to new password
        Utils.EncryptionTools.Key = Convert.FromHexString(newMpHash.Split(':').ElementAt(0));

        //encrypt database objects and write to database, overwriting exisiting data -- maybe just backup and delete old database, then create new database with new encryption
        //Groups
        foreach (var kvp in GroupsFromDB) {
            ((App)App.Current).DatabaseOps.UpdateData(kvp.Key, kvp.Value);
        }

        //Records
        foreach (var kvp in RecordsFromDB) {
            ((App)App.Current).DatabaseOps.UpdateData(kvp.Key, kvp.Value);
        }

        //dispose DB connection
        //((App)App.Current).DatabaseOps.DisposeConnection();

        //get current user SID for decryption key
        byte[] sidBinaryForm = new byte[256 / 8];
        System.Security.Principal.WindowsIdentity.GetCurrent().User.GetBinaryForm(sidBinaryForm, 0);
        Utils.EncryptionTools.Key = sidBinaryForm;

        //encrypt new password hash
        var newMpHashEncrypted = Utils.EncryptionTools.EncryptObjectStringToBase64String(newMpHash);

        //store hash of new password
        Utils.FileOperations.WriteToFile(Utils.FileOperations.AppSettingsDirectory + @"\mp.dat", newMpHashEncrypted, System.IO.FileMode.Create);

        //bubble to MainWindow_ViewModel
        ConfirmSetMasterPassword?.Invoke();
        //try {
        //    throw new NotImplementedException("OnConfirmSetNewPasswordCommand() not implemented yet...");
        //} catch (Exception ex) {
        //    MessageBox.Show(ex.Message);
        //}
    }
    private void OnCancelSetNewPasswordCommand(object obj) {
        //do something
        CancelSetMasterPassword?.Invoke();
        //try {
        //    throw new NotImplementedException("OnCancelSetNewPasswordCommand() not implemented yet...");
        //} catch(Exception ex) {
        //    MessageBox.Show(ex.Message);
        //}
    }
    #endregion Event Handlers

    #region OTHER METHODS
    private void UnlockDatabase(string password) {
        string passwordFromFile = Utils.FileOperations.ReadFromFile(Utils.FileOperations.AppSettingsDirectory + @"\mp.dat");
        string storedPasswordHash = Utils.EncryptionTools.DecryptBase64StringToObjectString(passwordFromFile); //ReadMasterPasswordFromFile();
        System.Diagnostics.Debug.WriteLine("Utils.Hasher.Verify(password, storedPasswordHash) == " + Utils.Hasher.Verify(password, storedPasswordHash));
        //compare password passed to method with stored hash of master password
        if (Utils.Hasher.Verify(password, storedPasswordHash) == false) {
            System.Diagnostics.Debug.WriteLine("Utils.Hasher.Verify(password, storedPasswordHash) == " + Utils.Hasher.Verify(password, storedPasswordHash));
            if (_attemptsRemaining > 1) {
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

        //recreate DB if master pass exists and DB does not
        if (DOES_DB_EXIST == false) {
            ((App)App.Current).DatabaseOps.CreateConnection();
            ((App)App.Current).DatabaseOps.CreateTables();
            ((App)App.Current).DatabaseOps.CheckForTables();
        }

        System.Diagnostics.Debug.WriteLine("password == " + password);
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
        System.Diagnostics.Debug.WriteLine("DeleteDatabase() called...");
        System.Diagnostics.Debug.WriteLine("Utils.FileOperations.DoesDirectoryExist(((App)App.Current).DatabaseOps.DatabaseFolderPath) = " + Utils.FileOperations.DoesDirectoryExist(((App)App.Current).DatabaseOps.DatabaseFolderPath));
        System.Diagnostics.Debug.WriteLine("Utils.FileOperations.DoesFileExist(((App)App.Current).DatabaseOps.DatabaseFilePath)        = " + Utils.FileOperations.DoesFileExist(((App)App.Current).DatabaseOps.DatabaseFilePath));
        
        //check that DB file directory exists
        if (Utils.FileOperations.DoesDirectoryExist(((App)App.Current).DatabaseOps.DatabaseFolderPath) == false) {
            //error message?
            return;
        }

        //check that DB file exists
        if (Utils.FileOperations.DoesFileExist(((App)App.Current).DatabaseOps.DatabaseFilePath) == false) {
            //error message
            return;
        }

        //delete DB file
        //((App)App.Current).DatabaseOps.DisposeConnection();
        Utils.FileOperations.DeleteFile(((App)App.Current).DatabaseOps.DatabaseFilePath);
        //recreate database file and tables
        //((App)App.Current).DatabaseOps.CreateConnection();
        //((App)App.Current).DatabaseOps.CreateTables();
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