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
    public Classes.DelegateCommand UnlockDatabaseCommand { get; set; }
    public Classes.DelegateCommand CloseWindowCommand { get; set; }

    //Constructors
    public LockScreen_ViewModel(ViewModelBase parentVM) : base(parentVM) {
        UnlockDatabaseCommand = new Classes.DelegateCommand(onUnlockDatabaseCommand);
        CloseWindowCommand = new Classes.DelegateCommand(onCloseWindowCommand);
    }

    //Event Handlers
    private void onUnlockDatabaseCommand(object obj) {
        //System.Diagnostics.Debug.WriteLine(obj.ToString());
        DatabaseUnlocked?.Invoke(obj);
    }
    private void onCloseWindowCommand(object obj) {
        //System.Diagnostics.Debug.WriteLine(obj.ToString());
        //raise event to MainWindow
        WindowClosed?.Invoke(obj);
        //if (DatabaseUnlocked != null) {
        //    DatabaseUnlocked.Invoke(obj);
        //}
    }
}
