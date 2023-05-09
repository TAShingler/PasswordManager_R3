using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordManager_R3.ViewModels;
internal class MainWindow_ViewModel : System.ComponentModel.INotifyPropertyChanged {
    //Private Fields
    private WindowState _winState = WindowState.Normal;
    private System.Windows.Controls.ContentControl _currentView;

    //Public Properties
    public WindowState WinState { 
        get { return _winState; }
        set {
            _winState = value;
            OnPropertyChanged(nameof(WinState));
        }
    }
    public System.Windows.Controls.ContentControl CurrentView {
        get { return _currentView; }
        set {
            _currentView = value;
            OnPropertyChanged(nameof(CurrentView));
        }
    }

    //Delegates for Commands
    public Classes.DelegateCommand MinimizeWindowCommand { get; set; }
    public Classes.DelegateCommand MaximizeRestoreWindowCommand { get; set; }
    public Classes.DelegateCommand CloseWindowCommand { get; set; }
    public Classes.DelegateCommand LockDatabaseCommand { get; set; }
    public Classes.DelegateCommand AddRecordCommand { get; set; }
    public Classes.DelegateCommand EditRecordCommand { get; set; }
    public Classes.DelegateCommand DeleteRecordCommand { get; set; }
    public Classes.DelegateCommand CopyUsernameToClipboardCommand { get; set; }
    public Classes.DelegateCommand CopyPasswordToClipboardCommand { get; set; }
    public Classes.DelegateCommand CopyUrlToClipboardCommand { get; set; }
    public Classes.DelegateCommand GeneratePasswordCommand { get; set; }
    public Classes.DelegateCommand AppSettingsCommand { get; set; }


    public MainWindow_ViewModel() {
        //WinState = WindowState.Normal;

        //Set delegates for commands
        SetDelegateCommands();

        CurrentView = new Views.LockScreen_View();
    }

    //Event handlers for Button Commands
    private void onMinimizeWindowCommand(object obj) {
        WinState = WindowState.Minimized;
    }
    private void onMaximizeRestoreWindowCommand(object obj) {
        if (WinState == WindowState.Normal) {
            WinState = WindowState.Maximized;
        } else {
            WinState = WindowState.Normal;
        }
    }
    private void onWindowCloseCommand(object obj) {
        Window win = (Window)obj;
        win.Close();
    }
    private void onLockDatabaseCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onLockDatabaseCommand clicked, but it's not fully implemented yet...");
        CurrentView = new Views.LockScreen_View();
    }
    private void onAddRecordCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onAddRecordCommand clicked, but it's not implemented yet...");
        CurrentView = new Views.AddEditRecord_View();
    }
    private void onEditRecordCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onEditRecordCommand clicked, but it's not implemented yet...");
        CurrentView = new Views.AddEditRecord_View(); //will need to have 2 constructors -- one for add record, the other for edit record
    }
    private void onDeleteRecordCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onDeleteRecordCommand clicked, but it's not implemented yet...");
        //does not change views, but might need to add code to refresh DB -- will figure ot for sure later
    }
    private void onCopyUsernameToClipboardCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onCopyUsernameToClipboardCommand clicked, but it's not implemented yet...");
        //does not change views; copies username of selected record to system clipboard
    }
    private void onCopyPasswordToClipboardCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onCopyPasswordToClipboardCommand clicked, but it's not implemented yet...");
        //does not change views; copies password of selected record to system clipboard
    }
    private void onCopyUrlToClipboardCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onCopyUrlToClipboardCommand clicked, but it's not implemented yet...");
        //does not change views; copies url of selected record to system clipboard
    }
    private void onGeneratePasswordCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onGeneratePasswordCommand clicked, but it's not implemented yet...");
        CurrentView = new Views.PasswordGenerator_View();
    }
    private void onAppSettingsCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onAppSettingsCommand clicked, but it's not implemented yet...");
        CurrentView = new Views.AppSettings_View();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName) {
        if (PropertyChanged != null) {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    //protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null) { }  -- could be useful in future
    //protected Action<PropertyChangedEventArgs> RaisePropertyChanged() {
    //    return args => PropertyChanged?.Invoke(this, args);
    //}

    //Method to set Command delegates
    private void SetDelegateCommands() {
        MinimizeWindowCommand = new Classes.DelegateCommand(onMinimizeWindowCommand);
        MaximizeRestoreWindowCommand = new Classes.DelegateCommand(onMaximizeRestoreWindowCommand);
        CloseWindowCommand = new Classes.DelegateCommand(onWindowCloseCommand);

        LockDatabaseCommand = new Classes.DelegateCommand(onLockDatabaseCommand);
        AddRecordCommand = new Classes.DelegateCommand(onAddRecordCommand);
        EditRecordCommand = new Classes.DelegateCommand(onEditRecordCommand);
        DeleteRecordCommand = new Classes.DelegateCommand(onDeleteRecordCommand);
        CopyUsernameToClipboardCommand = new Classes.DelegateCommand(onCopyUsernameToClipboardCommand);
        CopyPasswordToClipboardCommand = new Classes.DelegateCommand(onCopyPasswordToClipboardCommand);
        CopyUrlToClipboardCommand = new Classes.DelegateCommand(onCopyUrlToClipboardCommand);
        GeneratePasswordCommand = new Classes.DelegateCommand(onGeneratePasswordCommand);
        AppSettingsCommand = new Classes.DelegateCommand(onAppSettingsCommand);
    }
}
