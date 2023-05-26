using System;
using System.Windows;

namespace PasswordManager_R3.ViewModels;
internal class LockScreen_ViewModel : ViewModelBase {
    //Fields
    //private const string NULL_WHITESPACE_ERROR = "No password entered.\nPlease enter a valid password.";
    //private const string INCORRECT_PASSWORD_ERROR = $"Entered password is not correct.";
    private string _outputMessage = string.Empty;
    private int _attemptsRemaining;

    //Delegates
    public delegate void UnlockDatabaseHandler(object obj);    //might add event args later...
    public delegate void CloseWindowHandler(object obj);
    //Events
    public event UnlockDatabaseHandler? DatabaseUnlocked;
    public event CloseWindowHandler? WindowClosed;

    //Public Properties
    public Utils.DelegateCommand UnlockDatabaseCommand { get; set; }
    public Utils.DelegateCommand CloseWindowCommand { get; set; }

    public string OutputMessage {
        get { return _outputMessage; }
        set {
            _outputMessage = value;
            OnPropertyChanged(nameof(OutputMessage));
        }
    }

    //Constructors
    public LockScreen_ViewModel(ViewModelBase parentVM) : base(parentVM) {
        UnlockDatabaseCommand = new Utils.DelegateCommand(OnUnlockDatabaseCommand);
        CloseWindowCommand = new Utils.DelegateCommand(OnCloseWindowCommand);
    }

    //Event Handlers
    private void OnUnlockDatabaseCommand(object obj) {
        //System.Diagnostics.Debug.WriteLine(obj.ToString());
        //if (TryUnlockDatabase() == true) {
        //    DatabaseUnlocked?.Invoke(obj);
        //}
        TryUnlockDatabase(ref obj);
        System.Diagnostics.Debug.WriteLine("UserPassword: " + obj.ToString());
    }
    private void OnCloseWindowCommand(object obj) {
        //System.Diagnostics.Debug.WriteLine(obj.ToString());
        //raise event to MainWindow
        WindowClosed?.Invoke(obj);
        //if (DatabaseUnlocked != null) {
        //    DatabaseUnlocked.Invoke(obj);
        //}
    }

    #region OTHER METHODS
    private void TryUnlockDatabase(ref object obj) {
        //int _wrongAttemptsCount = MAX_ATTEMPTS;
        //get password (already passed to method)
        string objAsString = (string)obj;

        //is password null or whitespace?
        if (string.IsNullOrEmpty(objAsString)) {
            //display error message to user
            OutputMessage = "No password entered.\nPlease enter a valid password.";
            return;
        }

        //compare password passed to method with stored hash of master password
        if (Utils.Hasher.Verify(objAsString, string.Empty) == false) {
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
        string[] hashedPasswordParts = objAsString.Split(':');

        //set encryption key for Encryptor/Decryptor equal to hash of master password
        Utils.EncryptionTools.Key = Convert.FromHexString(hashedPasswordParts[0]);

        //reset wrong attempts count
        //_wrongAttemptsCount = MAX_ATTEMPTS;

        //raise event -- pass something?
        System.Diagnostics.Debug.WriteLine("obj as string: " + objAsString);
        DatabaseUnlocked?.Invoke(obj);

        //return false;
    }
    private void DeleteDatabase() { //might move this method to app.cs and process there; possibly in MainWindow
        //do something
    }
    private void CreateMasterPassword(object obj) {   //might move this app.cs
        //do something
        string objAsString = (string)obj;

        if (string.IsNullOrWhiteSpace(objAsString)) {
            OutputMessage = $"Entered password is not valid.\nPlease enter a valid password.";
        }

        string hashedPassword = Utils.Hasher.Hash(objAsString);

        //might move FileStream to WriteMasterPasswordToFile()
        System.IO.FileStream fs = System.IO.File.Open(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\PasswordManager_R3\Data\master_pass.dat", System.IO.FileMode.Append);

        WriteMasterPasswordToFile(fs, hashedPassword);

        fs.Dispose();
        fs.Close();

        //set EncryptionTools key to hashed password as byte array
    }
    private void WriteMasterPasswordToFile(System.IO.FileStream fs, string hashedMasterPassword) {
        //do something
        System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);

        //get current user's SID and convert to byte array to use for encryption key
        byte[] sidBinaryForm = new byte[256 / 8];
        System.Security.Principal.WindowsIdentity.GetCurrent().User.GetBinaryForm(sidBinaryForm, 0);

        //set encryption key to current user's SID as byte array
        Utils.EncryptionTools.Key = sidBinaryForm;

        //encrypt
        string encryptedHashedPassword = Utils.EncryptionTools.EncryptObjectStringToBase64String(hashedMasterPassword);

        //write to file
        sw.Write(encryptedHashedPassword);

        sw.Flush();
        sw.Close();
        sw.Dispose();
    }
    private string ReadMasterPasswordFromFile() {
        //do something
        System.IO.FileStream fs = System.IO.File.OpenRead(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\PasswordManager_R3\Data\master_pass.data");
        System.IO.StreamReader sr = new System.IO.StreamReader(fs);

        //string decryptedPassword = string.Empty;
        string passwordFromFile = sr.ReadToEnd();
        sr.Close();
        fs.Close();

        //get current user SID for decryption key
        byte[] sidBinaryForm = new byte[256 / 8];
        System.Security.Principal.WindowsIdentity.GetCurrent().User.GetBinaryForm(sidBinaryForm, 0);
        Utils.EncryptionTools.Key = sidBinaryForm;

        //decrypt
        return Utils.EncryptionTools.DecryptBase64StringToObjectString(passwordFromFile);
    }

    #endregion OTHER METHODS
}
