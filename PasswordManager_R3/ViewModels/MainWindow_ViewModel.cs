using System;
using System.Windows;

namespace PasswordManager_R3.ViewModels;
internal class MainWindow_ViewModel : ViewModelBase {
    #region Fields
    //Window Properites
    private WindowState _winState = WindowState.Normal;
    private ViewModels.ViewModelBase? _selectedViewModel;
    private int _totalGroupsInDatabase = 5;
    private int _totalRecordsInDatabase = 23;
    private Models.Group _selectedGroup;
    private Models.Record _selectedRecord;

    //Window UIElement properties
    private Visibility _windowStatusBarVisibility = Visibility.Visible;
    private Visibility _windowStatusBarContentGridVisibility = Visibility.Hidden;

    //Quick Access Bar Button Visibility fields
    private bool _buttonLockDatabaseIsEnabled = false;
    private bool _buttonAddRecordIsEnabled = false;
    private bool _buttonEditRecordIsEnabled = false;
    private bool _buttonDeleteRecordIsEnabled = false;
    private bool _buttonUsernameToClipboardIsEnabled = false;
    private bool _buttonPasswordToClipboardIsEnabled = false;
    private bool _buttonUrlToClipboardIsEnabled = false;
    private bool _buttonPasswordGeneratorIsEnabled = false;
    private bool _buttonAppSettingsIsEnabled = false;
    #endregion Fields

    #region Properties
    //Window properties
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
    public int TotalGroupsInDatabase {
        get { return _totalGroupsInDatabase; }
        set {
            _totalGroupsInDatabase = value;
            OnPropertyChanged(nameof(TotalGroupsInDatabase));
        }
    }
    public int TotalRecordsInDatabase {
        get { return _totalRecordsInDatabase; }
        set {
            _totalRecordsInDatabase = value;
            OnPropertyChanged(nameof(TotalRecordsInDatabase));
        }
    }
    public Models.Group SelectedGroup {
        get { return _selectedGroup; }
        set {
            _selectedGroup = value;
            OnPropertyChanged(nameof(SelectedGroup));
        }
    }
    public Models.Record SelectedRecord {
        get { return _selectedRecord; }
        set {
            _selectedRecord = value;
            OnPropertyChanged(nameof(SelectedRecord));
        }
    }

    //Window UIElement properties
    public Visibility WindowStatusBarVisibility {
        get { return _windowStatusBarVisibility; }
        set {
            _windowStatusBarVisibility = value;
            OnPropertyChanged(nameof(WindowStatusBarVisibility));
        }
    }
    public Visibility WindowStatusBarContentGridVisibility {
        get { return _windowStatusBarContentGridVisibility; }
        set {
            _windowStatusBarContentGridVisibility = value;
            OnPropertyChanged(nameof(WindowStatusBarContentGridVisibility));
        }
    }

    //Quick Access Bar Button Visibility Properties
    public bool ButtonLockDatabaseIsEnabled {
        get { return _buttonLockDatabaseIsEnabled; }
        set {
            _buttonLockDatabaseIsEnabled = value;
            OnPropertyChanged(nameof(ButtonLockDatabaseIsEnabled));
        }
    }
    public bool ButtonAddRecordIsEnabled {
        get { return _buttonAddRecordIsEnabled; }
        set {
            _buttonAddRecordIsEnabled = value;
            OnPropertyChanged(nameof(ButtonAddRecordIsEnabled));
        }
    }
    public bool ButtonEditRecordIsEnabled {
        get { return _buttonEditRecordIsEnabled; }
        set {
            _buttonEditRecordIsEnabled = value;
            OnPropertyChanged(nameof(ButtonEditRecordIsEnabled));
        }
    }
    public bool ButtonDeleteRecordIsEnabled {
        get { return _buttonDeleteRecordIsEnabled; }
        set {
            _buttonDeleteRecordIsEnabled = value;
            OnPropertyChanged(nameof(ButtonDeleteRecordIsEnabled));
        }
    }
    public bool ButtonUsernameToClipboardIsEnabled {
        get { return _buttonUsernameToClipboardIsEnabled; }
        set {
            _buttonUsernameToClipboardIsEnabled = value;
            OnPropertyChanged(nameof(ButtonUsernameToClipboardIsEnabled));
        }
    }
    public bool ButtonPasswordToClipboardIsEnabled {
        get { return _buttonPasswordToClipboardIsEnabled; }
        set {
            _buttonPasswordToClipboardIsEnabled = value;
            OnPropertyChanged(nameof(ButtonPasswordToClipboardIsEnabled));
        }
    }
    public bool ButtonUrlToClipboardIsEnabled {
        get { return _buttonUrlToClipboardIsEnabled; }
        set {
            _buttonUrlToClipboardIsEnabled = value;
            OnPropertyChanged(nameof(ButtonUrlToClipboardIsEnabled));
        }
    }
    public bool ButtonPasswordGeneratorIsEnabled {
        get { return _buttonPasswordGeneratorIsEnabled; }
        set {
            _buttonPasswordGeneratorIsEnabled = value;
            OnPropertyChanged(nameof(ButtonPasswordGeneratorIsEnabled));
        }
    }
    public bool ButtonAppSettingsIsEnabled {
        get { return _buttonAppSettingsIsEnabled; }
        set {
            _buttonAppSettingsIsEnabled = value;
            OnPropertyChanged(nameof(ButtonAppSettingsIsEnabled));
        }
    }

    //Delegates for Commands to handle events
    public Utils.DelegateCommand? MinimizeWindowCommand {
        get;
        set;
    }
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
    #endregion Properties

    #region Constructors
    public MainWindow_ViewModel() : base() {
        //System.Diagnostics.Debug.WriteLine("Does master password exist: " + Utils.FileOperations.DoesMasterPasswordExist(@"C:\ProgramData\PasswordManager_R2\Data\master_pass.dat"));
        //WinState = WindowState.Normal;

        //Set delegates for commands
        SetDelegateCommands();
        OnLockDatabaseCommand(new object());

        //LockScreen_ViewModel lockScreen_ViewModel = new LockScreen_ViewModel();
        //lockScreen_ViewModel.ParentVM = this;

        //SelectedViewModel = new ViewModels.LockScreen_ViewModel(); //Views.LockScreen_View();
        //((LockScreen_ViewModel)SelectedViewModel).DatabaseUnlocked += new LockScreen_ViewModel.UnlockDatabaseDelegate(TryUnlockDatabase);
        //SelectedViewModel.ParentVM = this;


        /*  Default to LockScreen_View  */
        //LockScreen_ViewModel lockScreenVM = new LockScreen_ViewModel(this);
        //lockScreenVM.DatabaseUnlocked += OnSetDatabaseView;
        //lockScreenVM.WindowClosed += OnWindowCloseCommand;
        //SelectedViewModel = lockScreenVM;

        /*  Default to Database_View  */
        //Database_ViewModel databaseVM = new Database_ViewModel(this);
        //databaseVM.GroupSelectionChanged += OnGroupSelectionChanged;
        //databaseVM.RecordSelectionChanged += OnRecordSelectionChanged;
        //SelectedViewModel = databaseVM;

        /*  Default to AddEditRecord_View  */
        //AddEditRecord_ViewModel addEditRecordVM = new(this, new Models.Record() {
        //    Title = "TestTitle",
        //    Username = "TestUsername",
        //    Email = "TestEmail",
        //    Password = "TestPassword",
        //    URL = "TestUrl",
        //    Tags = "TestTags",
        //    HasExpirationDate = true,
        //    ExpirationDate = DateTime.Today.AddDays(45),
        //    HasNotes = false,
        //    Notes = "some notes that should not exist",
        //    GUID = Guid.NewGuid().ToString()
        //});
        ////AddEditRecord_ViewModel addEditRecordVM = new(this);// this);
        //addEditRecordVM.CreateRecord += void (object obj, EventArgs e) => { System.Diagnostics.Debug.WriteLine("Test"); };
        //SelectedViewModel = addEditRecordVM;

        /*  Default to AddEditGroup_View  */
        //AddEditGroup_ViewModel addEditGroupVM = new AddEditGroup_ViewModel(this, new Models.Group() {
        //    Title = "TestGroup1",
        //    HasExpirationDate = true,
        //    ExpirationDate = DateTime.Now.AddDays(30),
        //    HasNotes = true,
        //    Notes = "Here are some notes. I will probably delete them.",
        //    GUID = Guid.NewGuid().ToString()
        //});
        //AddEditGroup_ViewModel addEditGroupVM = new(this);
        //SelectedViewModel = addEditGroupVM;

        //ButtonLockDatabaseIsEnabled = false;
        //ButtonAddRecordIsEnabled = false;
        //ButtonEditRecordIsEnabled = false;
        //ButtonDeleteRecordIsEnabled = false;
        //ButtonUsernameToClipboardIsEnabled = false;
        //ButtonPasswordToClipboardIsEnabled = false;
        //ButtonUrlToClipboardIsEnabled = false;
        //ButtonPasswordGeneratorIsEnabled = false;
        //ButtonAppSettingsIsEnabled = false;
    }
    #endregion Constructors

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

    #region Quick Access Bar Event Handlers
    /*
    private void OnLockDatabaseButtonCommand(object obj) {
        //LockScreen_ViewModel lockScreenVM = new(this);
        //lockScreenVM.DatabaseUnlocked += OnSetDatabaseView;
        //SelectedViewModel = lockScreenVM;
        OnLockDatabaseButtonCommand(obj);
    }
    private void OnCreateRecordButtonCommand(object obj) {
        OnAddRecordCommand(obj);
    }
    private void OnUpdateRecordButtonComand(object obj) {
        //do something
        AddEditGroup_ViewModel addEditGroupVM = new(this);
        SelectedViewModel = addEditGroupVM;
    }
    private void OnDeleteRecordButtonCommand(object obj) {
        //do something
    }
    private void OnCopyUsernameToClipboardButtonCommand(object obj) {
        //do something
    }
    private void OnCopyPasswordToClipboardButtonCommand(object obj) {
        //do something
    }
    private void OnCopyUrlToClipboardButtonCommand(object obj) {
        //do something
    }
    private void OnPasswordGeneratorButtonCommand(object obj) {
        //do something
    }
    private void OnAppSettingsButtonCommand(object obj) {
        //do something
    }
    */
    #endregion Quick Access Bar Event Handlers

    #region Change CurrentView Event Handlers
    //Set CurrentView to LockScreen_View event handler
    private void OnLockDatabaseCommand(object obj) {
        Utils.EncryptionTools.Key = null;
        LockScreen_ViewModel lockScreenVM = new(this);

        //check that master pass file exists and has a size greater than 0 bytes before setting 
        //if (Utils.FileOperations.DoesMasterPasswordExist(string.Empty) == false) {
        //    lockScreenVM = new(this, false);
        //}

        lockScreenVM.DatabaseUnlocked += OnSetDatabaseView;
        lockScreenVM.WindowClosed += OnWindowCloseCommand;
        SelectedViewModel = lockScreenVM;

        ButtonLockDatabaseIsEnabled = false;
        ButtonAddRecordIsEnabled = false;
        ButtonEditRecordIsEnabled = false;
        ButtonDeleteRecordIsEnabled = false;
        ButtonUsernameToClipboardIsEnabled = false;
        ButtonPasswordToClipboardIsEnabled = false;
        ButtonUrlToClipboardIsEnabled = false;
        ButtonPasswordGeneratorIsEnabled = false;
        ButtonAppSettingsIsEnabled = false;
        WindowStatusBarContentGridVisibility = Visibility.Hidden;
    }
    //Set CurrentView to Database_View event handler
    private void OnSetDatabaseView() {
        //Database_ViewModel databaseVM = new Database_ViewModel(this);
        ////add event handlers
        //SelectedViewModel = databaseVM;
        //System.Diagnostics.Debug.WriteLine("OnSetDatabaseView() not implemented...");
        //System.Diagnostics.Debug.WriteLine("OnSetDatabaseView() obj to string: " + obj.ToString());


        Database_ViewModel databaseVM = new Database_ViewModel(this);
        databaseVM.SelectedRecordChanged += OnSelectedRecordChanged;
        databaseVM.SelectedGroupChanged += OnSelectedGroupChanged;
        databaseVM.CreateGroup += (Models.Group g) => {
            System.Diagnostics.Debug.WriteLine("MainWindow: databaseVM.UpdateGroup event elevated");

            //set Edit Group View
            //AddEditGroup_ViewModel addEditGroupVM = new AddEditGroup_ViewModel(this, s);
            //SelectedViewModel = addEditGroupVM;
        };
        databaseVM.UpdateGroup += (object s, EventArgs e) => { System.Diagnostics.Debug.WriteLine("MainWindow: databaseVM.UpdateGroup event elevated"); };
        databaseVM.DeleteGroup += (object s, EventArgs e) => { System.Diagnostics.Debug.WriteLine("MainWindow: databaseVM.DeleteGroup event elevated"); };
        SelectedViewModel = databaseVM;

        ButtonLockDatabaseIsEnabled = true;
        ButtonAddRecordIsEnabled = false;
        ButtonEditRecordIsEnabled = false;
        ButtonDeleteRecordIsEnabled = false;
        ButtonUsernameToClipboardIsEnabled = false;
        ButtonPasswordToClipboardIsEnabled = false;
        ButtonUrlToClipboardIsEnabled = false;
        ButtonPasswordGeneratorIsEnabled = false;
        ButtonAppSettingsIsEnabled = true;
        WindowStatusBarContentGridVisibility = Visibility.Visible;
    }
    //Set CurrentView to AddEditRecord_View (to add record to database) event handler
    private void OnAddRecordCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onAddRecordCommand clicked, but it's not implemented yet...");
        //CurrentView = new ViewModels.AddEditRecord_ViewModel();

        AddEditRecord_ViewModel addEditRecordVM = new AddEditRecord_ViewModel(this, ((ViewModels.Database_ViewModel)SelectedViewModel).SelectedGroup);
        addEditRecordVM.CreateRecord += (object obj, EventArgs e) => { System.Diagnostics.Debug.WriteLine("Test"); };
        addEditRecordVM.CancelAddEditRecord += OnSetDatabaseView;
        SelectedViewModel = addEditRecordVM;

        //AddEditGroup_ViewModel addEditGroupVM = new(this);
        //addEditGroupVM.CreateGroup += () => {
        //    System.Diagnostics.Debug.WriteLine("addEditGroup_ViewModel Ok Button clicked to create group");
        //};
        //addEditGroupVM.UpdateGroup += () => {
        //    System.Diagnostics.Debug.WriteLine("addEditGroup_ViewModel Ok Button clicked to update group");
        //};
        //addEditGroupVM.CancelAddEditGroup += OnSetDatabaseView;
        //SelectedViewModel = addEditGroupVM;

        ButtonLockDatabaseIsEnabled = false;
        ButtonAddRecordIsEnabled = false;
        ButtonEditRecordIsEnabled = false;
        ButtonDeleteRecordIsEnabled = false;
        ButtonUsernameToClipboardIsEnabled = false;
        ButtonPasswordToClipboardIsEnabled = false;
        ButtonUrlToClipboardIsEnabled = false;
        ButtonPasswordGeneratorIsEnabled = false;
        ButtonAppSettingsIsEnabled = false;
        WindowStatusBarContentGridVisibility = Visibility.Hidden;
    }
    //Set CurrentView to AddEditRecord_View (to edit record in database) event handler
    private void OnEditRecordCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onEditRecordCommand clicked, but it's not implemented yet...");
        //CurrentView = new ViewModels.AddEditRecord_ViewModel(); //will need to have 2 constructors -- one for add record, the other for edit record
        //AddEditRecord_ViewModel addEditRecordVM = new AddEditRecord_ViewModel(this);// this);
        //SelectedViewModel = addEditRecordVM;

        //AddEditGroup_ViewModel addEditGroupVM = new(this);
        //addEditGroupVM.CancelAddEditGroup += OnSetDatabaseView;
        //SelectedViewModel = addEditGroupVM;

        /*  Edit Record View  */
        AddEditRecord_ViewModel addEditRecordVM = new(this, new Models.Record() {
            Title = "Record passed to ViewModel Title",
            Username = "Record passed to ViewModel Username",
            Email = "Record passed to ViewModel Email",
            Password = "Record passed to ViewModel Password",
            URL = "Record passed to ViewModel Url",
            Notes = "Record passed to ViewModel Notes",
            CreatedDate = DateTime.Now,
            ExpirationDate = DateTime.Now.AddMonths(6),
            GUID = Guid.NewGuid().ToString(),
            HasNotes = true,
            HasExpirationDate = true,
            ModifiedDate = DateTime.Now.AddDays(8),
            Tags = "Record passed to ViewModel Tags"
        });
        addEditRecordVM.CreateRecord += (object obj, EventArgs e) => {
            System.Diagnostics.Debug.WriteLine("Edited object saved to database");
        };
        addEditRecordVM.CancelAddEditRecord += OnSetDatabaseView;
        SelectedViewModel = addEditRecordVM;

        /*  Edit Group View  */
        //AddEditGroup_ViewModel addEditGroupVM = new(
        //    this,
        //    new() { Title="test group title",
        //        ExpirationDate = DateTime.Now,
        //        HasExpirationDate = true,
        //        HasNotes = true,
        //        Notes = "test group notes...",
        //        GUID = Guid.NewGuid().ToString()
        //});
        //addEditGroupVM.CreateGroup += () => {
        //    System.Diagnostics.Debug.WriteLine("addEditGroup_ViewModel Ok Button clicked to create group");
        //};
        //addEditGroupVM.UpdateGroup += () => {
        //    System.Diagnostics.Debug.WriteLine("addEditGroup_ViewModel Ok Button clicked to update group");
        //};
        //addEditGroupVM.CancelAddEditGroup += OnSetDatabaseView;
        //SelectedViewModel = addEditGroupVM;

        ButtonLockDatabaseIsEnabled = false;
        ButtonAddRecordIsEnabled = false;
        ButtonEditRecordIsEnabled = false;
        ButtonDeleteRecordIsEnabled = false;
        ButtonUsernameToClipboardIsEnabled = false;
        ButtonPasswordToClipboardIsEnabled = false;
        ButtonUrlToClipboardIsEnabled = false;
        ButtonPasswordGeneratorIsEnabled = false;
        ButtonAppSettingsIsEnabled = false;
        WindowStatusBarContentGridVisibility = Visibility.Hidden;
    }
    //Set CurrentView to PasswordGenerator_View event handler
    private void OnGeneratePasswordCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onGeneratePasswordCommand clicked, but it's not implemented yet...");
        //CurrentView = new ViewModels.PasswordGenerator_ViewModel();

        ButtonLockDatabaseIsEnabled = false;
        ButtonAddRecordIsEnabled = false;
        ButtonEditRecordIsEnabled = false;
        ButtonDeleteRecordIsEnabled = false;
        ButtonUsernameToClipboardIsEnabled = false;
        ButtonPasswordToClipboardIsEnabled = false;
        ButtonUrlToClipboardIsEnabled = false;
        ButtonPasswordGeneratorIsEnabled = false;
        ButtonAppSettingsIsEnabled = false;
    }
    //Set CurrentView to AppSettings_View event handler
    private void OnAppSettingsCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onAppSettingsCommand clicked, but it's not implemented yet...");
        //CurrentView = new ViewModels.AppSettings_ViewModel();

        ButtonLockDatabaseIsEnabled = false;
        ButtonAddRecordIsEnabled = false;
        ButtonEditRecordIsEnabled = false;
        ButtonDeleteRecordIsEnabled = false;
        ButtonUsernameToClipboardIsEnabled = false;
        ButtonPasswordToClipboardIsEnabled = false;
        ButtonUrlToClipboardIsEnabled = false;
        ButtonPasswordGeneratorIsEnabled = false;
        ButtonAppSettingsIsEnabled = false;
        WindowStatusBarContentGridVisibility = Visibility.Hidden;
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
    //Event handler to handle GroupSelected event propogated in Database_ViewModel and bubbled to MainWindow_ViewModel
    private void OnSelectedGroupChanged(object obj) {
        //do somehting
        //SelectedGroup = (Models.Group)obj;

        System.Diagnostics.Debug.WriteLine("OnSelectedGroupChanged obj: " + obj);

        bool objAsBool = (bool)obj;

        if (objAsBool == false) {    //ViewModels.Database_ViewModel.SelectedRecord is null
            ButtonAddRecordIsEnabled = true;
        } else {    //ViewModels.Database_ViewModel.SelectedRecord is not null
            ButtonEditRecordIsEnabled = false;
        }
    }
    //Event handler to handle RecordSelected event propogated in Database_ViewModel and bubbled to MainWindow_ViewModel
    private void OnSelectedRecordChanged(object obj) {
        //do something
        //if (SelectedRecord != null) {
        //    SelectedRecord = (Models.Record)obj;
        //} else {
        //    SelectedRecord = null;
        //}
        System.Diagnostics.Debug.WriteLine("OnSelectedRecordChanged obj: " + obj);


        bool objAsBool = (bool)obj;

        if (objAsBool == false) {    //ViewModels.Database_ViewModel.SelectedRecord is null
            ButtonEditRecordIsEnabled = true;
            ButtonDeleteRecordIsEnabled = true;
            ButtonUsernameToClipboardIsEnabled = true;
            ButtonPasswordToClipboardIsEnabled = true;
            ButtonUrlToClipboardIsEnabled = true;
        } else {    //ViewModels.Database_ViewModel.SelectedRecord is not null
            ButtonEditRecordIsEnabled = false;
            ButtonDeleteRecordIsEnabled = false;
            ButtonUsernameToClipboardIsEnabled = false;
            ButtonPasswordToClipboardIsEnabled = false;
            ButtonUrlToClipboardIsEnabled = false;
        }
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
