using System;
using System.Windows;

namespace PasswordManager_R3.ViewModels;
internal class LockScreen_ViewModel : ViewModelBase {
    //Delegates
    public delegate void UnlockDatabaseDelegate(object obj);    //might add event args later...
    public delegate void CloseWindowDelegate(object obj);
    //Events
    public event UnlockDatabaseDelegate? DatabaseUnlocked;
    public event CloseWindowDelegate? WindowClosed;

    //Public Properties
    public Utils.DelegateCommand UnlockDatabaseCommand { get; set; }
    public Utils.DelegateCommand CloseWindowCommand { get; set; }

    //Constructors
    public LockScreen_ViewModel(ViewModelBase parentVM) : base(parentVM) {
        UnlockDatabaseCommand = new Utils.DelegateCommand(onUnlockDatabaseCommand);
        CloseWindowCommand = new Utils.DelegateCommand(onCloseWindowCommand);
    }

    //Event Handlers
    private void onUnlockDatabaseCommand(object obj) {
        //System.Diagnostics.Debug.WriteLine(obj.ToString());
        //if (TryUnlockDatabase() == true) {
        //    DatabaseUnlocked?.Invoke(obj);
        //}
        TryUnlockDatabase(ref obj);
        System.Diagnostics.Debug.WriteLine("UserPassword: " + obj.ToString());
    }
    private void onCloseWindowCommand(object obj) {
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
        /*if (string.IsNullOrEmpty(objAsString)) {
            //display error message to user
            return;
        }*/

        //compare password passed to method with stored hash of master password
        /*if (Hasher.Verify(masterPass, filePath) == false) {
            if (_wrongAttemptsCount > 0) {
                //decrement wrong attempts count and display error message to user

                return;
            } else {
                //display error message and delete database

                //exit app or return to initial setup?
            }
        }*/

        //split stored hash into parts
        //string[] hashedPassParts = stringFromFile.Split(':');

        //set encryption key for Encryptor/Decryptor equal to hash of master password


        //reset wrong attempts count
        //_wrongAttemptsCount = MAX_ATTEMPTS;

        //raise event -- pass something?
        System.Diagnostics.Debug.WriteLine("obj as string: " + objAsString);
        DatabaseUnlocked?.Invoke(obj);

        //return false;
    }
    #endregion OTHER METHODS
}
