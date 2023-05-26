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
    private ViewModels.ViewModelBase? _selectedViewModel;

    //Public Properties
    public WindowState WinState { 
        get { return _winState; }
        set {
            _winState = value;
            OnPropertyChanged(nameof(WinState));
        }
    }
    public ViewModels.ViewModelBase? SelectedViewModel {
        get { return _selectedViewModel; }
        set {
            _selectedViewModel = value;
            OnPropertyChanged(nameof(SelectedViewModel));
        }
    }

    //Delegates for Commands to handle events
    public Utils.DelegateCommand? MinimizeWindowCommand { get; set; }
    public Utils.DelegateCommand? MaximizeRestoreWindowCommand { get; set; }
    public Utils.DelegateCommand? CloseWindowCommand { get; set; }
    public Utils.DelegateCommand? LockDatabaseCommand { get; set; }
    public Utils.DelegateCommand? AddRecordCommand { get; set; }
    public Utils.DelegateCommand? EditRecordCommand { get; set; }
    public Utils.DelegateCommand? DeleteRecordCommand { get; set; }
    public Utils.DelegateCommand? CopyUsernameToClipboardCommand { get; set; }
    public Utils.DelegateCommand? CopyPasswordToClipboardCommand { get; set; }
    public Utils.DelegateCommand? CopyUrlToClipboardCommand { get; set; }
    public Utils.DelegateCommand? GeneratePasswordCommand { get; set; }
    public Utils.DelegateCommand? AppSettingsCommand { get; set; }



    public MainWindow_ViewModel() : base() {
        //System.Diagnostics.Debug.WriteLine("Does master password exist: " + Utils.FileOperations.DoesMasterPasswordExist(@"C:\ProgramData\PasswordManager_R2\Data\master_pass.dat"));
        //WinState = WindowState.Normal;

        //Set delegates for commands
        SetDelegateCommands();

        //LockScreen_ViewModel lockScreen_ViewModel = new LockScreen_ViewModel();
        //lockScreen_ViewModel.ParentVM = this;

        //SelectedViewModel = new ViewModels.LockScreen_ViewModel(); //Views.LockScreen_View();
        //((LockScreen_ViewModel)SelectedViewModel).DatabaseUnlocked += new LockScreen_ViewModel.UnlockDatabaseDelegate(TryUnlockDatabase);
        //SelectedViewModel.ParentVM = this;


        /*  Default to LockScreen_View  */
        //LockScreen_ViewModel lockScreenVM = new LockScreen_ViewModel(this);
        //lockScreenVM.DatabaseUnlocked += OnSetDatabaseView;
        //lockScreenVM.WindowClosed += onWindowCloseCommand;
        //SelectedViewModel = lockScreenVM;

        /*  Default to Database_View  */
        Database_ViewModel databaseVM = new Database_ViewModel(this);
        SelectedViewModel = databaseVM;

        /*  Default to AddEditRecord_View  */
        //AddEditRecord_ViewModel addEditRecordVM = new AddEditRecord_ViewModel();// this);
        //addEditRecordVM.CreateRecord += void (object obj, EventArgs e) => { System.Diagnostics.Debug.WriteLine("Test"); };
        //SelectedViewModel = addEditRecordVM;

        /*  Default to AddEditGroup_View  */
        //AddEditGroup_ViewModel addEditGroupVM = new AddEditGroup_ViewModel(this);
        //SelectedViewModel = addEditGroupVM;
    }

    #region Title Bar Event Handlers
    //Minimize Window event handler
    private void OnMinimizeWindowCommand(object obj) {
        WinState = WindowState.Minimized;
    }
    //Maximize and Restore Window event handler
    private void OnMaximizeRestoreWindowCommand(object obj) {
        if (WinState == WindowState.Normal) {
            WinState = WindowState.Maximized;
        } else {
            WinState = WindowState.Normal;
        }
    }
    //Close Window event handler
    private void OnWindowCloseCommand(object obj) {
        Window win = (Window)obj;
        win.Close();
    }
    #endregion Title Bar Event Handlers

    #region Change CurrentView Event Handlers
    //Set CurrentView to LockScreen_View event handler
    private void OnLockDatabaseCommand(object obj) {
        Utils.EncryptionTools.Key = null;
        LockScreen_ViewModel lockScreenVM = new(this);

        //check that master pass file exists and has a size greater than 0 bytes before setting 
        if (Utils.FileOperations.DoesMasterPasswordExist(string.Empty) == false) {
            lockScreenVM = new(this, false);
        }

        lockScreenVM.DatabaseUnlocked += OnSetDatabaseView;
        lockScreenVM.WindowClosed += OnWindowCloseCommand;
        SelectedViewModel = lockScreenVM;
    }
    //Set CurrentView to Database_View event handler
    private void OnSetDatabaseView() {
        //Database_ViewModel databaseVM = new Database_ViewModel(this);
        ////add event handlers
        //SelectedViewModel = databaseVM;
        //System.Diagnostics.Debug.WriteLine("OnSetDatabaseView() not implemented...");
        //System.Diagnostics.Debug.WriteLine("OnSetDatabaseView() obj to string: " + obj.ToString());


        Database_ViewModel databaseVM = new Database_ViewModel(this);
        SelectedViewModel = databaseVM;
    }
    //Set CurrentView to AddEditRecord_View (to add record to database) event handler
    private void OnAddRecordCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onAddRecordCommand clicked, but it's not implemented yet...");
        //CurrentView = new ViewModels.AddEditRecord_ViewModel();
        AddEditRecord_ViewModel addEditRecordVM = new AddEditRecord_ViewModel();// this);
        addEditRecordVM.CreateRecord += void (object obj, EventArgs e) => { System.Diagnostics.Debug.WriteLine("Test"); };
        SelectedViewModel = addEditRecordVM;
    }
    //Set CurrentView to AddEditRecord_View (to edit record in database) event handler
    private void OnEditRecordCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onEditRecordCommand clicked, but it's not implemented yet...");
        //CurrentView = new ViewModels.AddEditRecord_ViewModel(); //will need to have 2 constructors -- one for add record, the other for edit record
        AddEditRecord_ViewModel addEditRecordVM = new AddEditRecord_ViewModel();// this);
        SelectedViewModel = addEditRecordVM;
    }
    //Set CurrentView to PasswordGenerator_View event handler
    private void OnGeneratePasswordCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onGeneratePasswordCommand clicked, but it's not implemented yet...");
        //CurrentView = new ViewModels.PasswordGenerator_ViewModel();
    }
    //Set CurrentView to AppSettings_View event handler
    private void OnAppSettingsCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onAppSettingsCommand clicked, but it's not implemented yet...");
        //CurrentView = new ViewModels.AppSettings_ViewModel();
    }
    #endregion Change CurrentView Event Handlers

    #region Misc. Event Handlers
    //Event Handler to delete record from DB
    private void OnDeleteRecordCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onDeleteRecordCommand clicked, but it's not implemented yet...");
        //does not change views, but might need to add code to refresh DB -- will figure ot for sure later
    }
    //Event Handler to copy username of selected record to the system clipboard
    private void OnCopyUsernameToClipboardCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onCopyUsernameToClipboardCommand clicked, but it's not implemented yet...");
        //does not change views; copies username of selected record to system clipboard
    }
    //Event Handler to copy password of selected record to the system clipboard
    private void OnCopyPasswordToClipboardCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onCopyPasswordToClipboardCommand clicked, but it's not implemented yet...");
        //does not change views; copies password of selected record to system clipboard
    }
    //Event Handler to copy URL of selected record to the system clipboard
    private void OnCopyUrlToClipboardCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onCopyUrlToClipboardCommand clicked, but it's not implemented yet...");
        //does not change views; copies url of selected record to system clipboard
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
        MinimizeWindowCommand = new Utils.DelegateCommand(OnMinimizeWindowCommand);
        MaximizeRestoreWindowCommand = new Utils.DelegateCommand(OnMaximizeRestoreWindowCommand);
        CloseWindowCommand = new Utils.DelegateCommand(OnWindowCloseCommand);

        LockDatabaseCommand = new Utils.DelegateCommand(OnLockDatabaseCommand);
        AddRecordCommand = new Utils.DelegateCommand(OnAddRecordCommand);
        EditRecordCommand = new Utils.DelegateCommand(OnEditRecordCommand);
        DeleteRecordCommand = new Utils.DelegateCommand(OnDeleteRecordCommand);
        CopyUsernameToClipboardCommand = new Utils.DelegateCommand(OnCopyUsernameToClipboardCommand);
        CopyPasswordToClipboardCommand = new Utils.DelegateCommand(OnCopyPasswordToClipboardCommand);
        CopyUrlToClipboardCommand = new Utils.DelegateCommand(OnCopyUrlToClipboardCommand);
        GeneratePasswordCommand = new Utils.DelegateCommand(OnGeneratePasswordCommand);
        AppSettingsCommand = new Utils.DelegateCommand(OnAppSettingsCommand);
    }
}
