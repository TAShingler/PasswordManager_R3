using PasswordManager_R3.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordManager_R3.ViewModels;
internal class MainWindow_ViewModel : ViewModelBase {
    //Private Fields
    private WindowState _winState = WindowState.Normal;
    private ViewModels.ViewModelBase _selectedViewModel;

    //Public Properties
    public WindowState WinState { 
        get { return _winState; }
        set {
            _winState = value;
            OnPropertyChanged(nameof(WinState));
        }
    }
    public ViewModels.ViewModelBase SelectedViewModel {
        get { return _selectedViewModel; }
        set {
            _selectedViewModel = value;
            OnPropertyChanged(nameof(SelectedViewModel));
        }
    }

    //Delegates for Commands to handle events
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

        //LockScreen_ViewModel lockScreen_ViewModel = new LockScreen_ViewModel();
        //lockScreen_ViewModel.ParentVM = this;

        SelectedViewModel = new ViewModels.LockScreen_ViewModel(); //Views.LockScreen_View();
        //((LockScreen_ViewModel)SelectedViewModel).DatabaseUnlocked += new LockScreen_ViewModel.UnlockDatabaseDelegate(TryUnlockDatabase);
        //SelectedViewModel.ParentVM = this;
    }

    #region Title Bar Event Handlers
    //Minimize Window event handler
    private void onMinimizeWindowCommand(object obj) {
        WinState = WindowState.Minimized;
    }
    //Maximize and Restore Window event handler
    private void onMaximizeRestoreWindowCommand(object obj) {
        if (WinState == WindowState.Normal) {
            WinState = WindowState.Maximized;
        } else {
            WinState = WindowState.Normal;
        }
    }
    //Close Window event handler
    private void onWindowCloseCommand(object obj) {
        Window win = (Window)obj;
        win.Close();
    }
    #endregion Title Bar Event Handlers
    #region Change CurrentView Event Handlers
    //Set CurrentView to LockScreen_View event handler
    private void onLockDatabaseCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onLockDatabaseCommand clicked, but it's not fully implemented yet...");
        SelectedViewModel = new ViewModels.LockScreen_ViewModel();
        //((LockScreen_ViewModel)SelectedViewModel).DatabaseUnlocked += new LockScreen_ViewModel.UnlockDatabaseDelegate(TryUnlockDatabase);
        //SelectedViewModel.ParentVM = this;
        //LockScreen_ViewModel lockScreen_ViewModel = new LockScreen_ViewModel();
        //lockScreen_ViewModel.ParentVM = this;

        //SelectedViewModel = lockScreen_ViewModel;
        //SelectedViewModel.ParentVM = this;
    }
    //Set CurrentView to AddEditRecord_View (to add record to database) event handler
    private void onAddRecordCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onAddRecordCommand clicked, but it's not implemented yet...");
        //CurrentView = new ViewModels.AddEditRecord_ViewModel();
    }
    //Set CurrentView to AddEditRecord_View (to edit record in database) event handler
    private void onEditRecordCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onEditRecordCommand clicked, but it's not implemented yet...");
        //CurrentView = new ViewModels.AddEditRecord_ViewModel(); //will need to have 2 constructors -- one for add record, the other for edit record
    }
    //Set CurrentView to PasswordGenerator_View event handler
    private void onGeneratePasswordCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onGeneratePasswordCommand clicked, but it's not implemented yet...");
        //CurrentView = new ViewModels.PasswordGenerator_ViewModel();
    }
    //Set CurrentView to AppSettings_View event handler
    private void onAppSettingsCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onAppSettingsCommand clicked, but it's not implemented yet...");
        //CurrentView = new ViewModels.AppSettings_ViewModel();
    }
    #endregion Change CurrentView Event Handlers
    #region Misc. Event Handlers
    //Event Handler to delete record from DB
    private void onDeleteRecordCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onDeleteRecordCommand clicked, but it's not implemented yet...");
        //does not change views, but might need to add code to refresh DB -- will figure ot for sure later
    }
    //Event Handler to copy username of selected record to the system clipboard
    private void onCopyUsernameToClipboardCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onCopyUsernameToClipboardCommand clicked, but it's not implemented yet...");
        //does not change views; copies username of selected record to system clipboard
    }
    //Event Handler to copy password of selected record to the system clipboard
    private void onCopyPasswordToClipboardCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onCopyPasswordToClipboardCommand clicked, but it's not implemented yet...");
        //does not change views; copies password of selected record to system clipboard
    }
    //Event Handler to copy URL of selected record to the system clipboard
    private void onCopyUrlToClipboardCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onCopyUrlToClipboardCommand clicked, but it's not implemented yet...");
        //does not change views; copies url of selected record to system clipboard
    }
    //Event Handler for unlocking database
    private void TryUnlockDatabase(object obj) {
        System.Diagnostics.Debug.WriteLine("TryUnlockDatabase() in MainWindow_ViewModel called...");
    }
    #endregion Misc. Event Handlers

    //public event PropertyChangedEventHandler? PropertyChanged;
    //protected void OnPropertyChanged(string propertyName) {
    //    if (PropertyChanged != null) {
    //        PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //    }
    //}
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

    //methods to set Views and add
}
