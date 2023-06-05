using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.ViewModels;
internal class AddEditRecord_ViewModel : ViewModelBase {
    #region Fields
    private bool _isNewRecord = true;

    //record selected in DataGrid in Databse_View
    private Models.Record _selectedRecord = new();

    //fields for Properties View binds to
    private string _srTitle;
    private string _srUsername;
    private string _srEmail;
    private string _srPassword;
    private string _srUrl;
    private string _srTags; //might get rid of tags...
    private bool _srHasExpirationDate;
    private DateTime? _srExpirationDate;
    private bool _srHasNotes;
    private string _srNotes;
    private DateTime? _srCreatedDate;
    private DateTime? _srModifiedDate;
    private string _srGuid;
    #endregion Fields

    #region Properties
    //might make Properties not directly reference Record obj...
    public string SrTitle {
        get { return _srTitle; }
        set {
            _srTitle = value;

            OnPropertyChanged(nameof(SrTitle));
        }
    }
    public string SrUsername {
        get { return _srUsername; }
        set {
            _srUsername = value;
            OnPropertyChanged(nameof(SrUsername));
        }
    }
    public string SrEmail {
        get { return _srEmail; }
        set {
            _srEmail = value;
            OnPropertyChanged(nameof(SrEmail));
        }
    }
    public string SrPassword {
        get { return _srPassword; }
        set {
            _srPassword = value;
            OnPropertyChanged(nameof(SrPassword));
        }
    }
    public string SrUrl {
        get { return _srUrl; }
        set {
            _srUrl = value;
            OnPropertyChanged(nameof(SrUrl));
        }
    }
    public string SrTags {
        get { return _srTags; }
        set {
            _srTags = value;
            OnPropertyChanged(nameof(SrTags));
        }
    }   //might get rid of tags...
    public bool SrHasExpirationDate {
        get { return _srHasExpirationDate; }
        set {
            _srHasExpirationDate = value;
            OnPropertyChanged(nameof(SrHasExpirationDate));
        }
    }
    public string SrExpirationDate {
        get { return _srExpirationDate.ToString(); }
        set {
            _srExpirationDate = DateTime.Parse(value);
            OnPropertyChanged(nameof(SrExpirationDate));
        }
    }
    public bool SrHasNotes {
        get { return _srHasNotes; }
        set {
            _srHasNotes = value;
            OnPropertyChanged(nameof(SrHasNotes));
        }
    }
    public string SrNotes {
        get { return _srNotes; }
        set {
            _srNotes = value;
            OnPropertyChanged(nameof(SrNotes));
        }
    }
    public string SrCreatedDate {
        get { return _srCreatedDate.ToString(); }
        set {
            _srCreatedDate = DateTime.Parse(value);
            OnPropertyChanged(nameof(SrCreatedDate));
        }
    }
    public string SrModifiedDate {
        get { return _srModifiedDate.ToString(); }
        set {
            _srModifiedDate = DateTime.Parse(value);
            OnPropertyChanged(nameof(SrModifiedDate));
        }
    }
    public string SrGuid {
        get { return _srGuid; }
        set {
            _srGuid = value;
        }
    }

    //DelegateCommands
    public Utils.DelegateCommand OkButtonCommand { get; set; }
    public Utils.DelegateCommand CancelButtonCommand { get; set; }
    #endregion Properties

    //Delegates to bubble events to MainWindow_ViewModel
    public delegate void CreateRecordEventHandler(object sender, EventArgs e);
    public delegate void UpdateRecordEventHandler(object sender, EventArgs e);
    public delegate void CancelAddEditRecordHandler();//(object sender, EventArgs e);

    //Events to bubble to MainWindow_ViewModel
    public event CreateRecordEventHandler? CreateRecord; //prob. rename...
    public event UpdateRecordEventHandler? UpdateRecord;
    public event CancelAddEditRecordHandler? CancelAddEditRecord;

    //pass selectedGroup as well? RoutedEventArgs for slectedGroup and Record objects?
    public AddEditRecord_ViewModel(ViewModelBase parentVM, Models.Record? selectedRecord = null) : base(parentVM) {
        if (selectedRecord != null) {
            _isNewRecord = false;
            _selectedRecord = selectedRecord;

            SrTitle = selectedRecord.Title;
            SrUsername = selectedRecord.Username;
            SrEmail = selectedRecord.Email;
            SrPassword = selectedRecord.Password;
            SrUrl = selectedRecord.URL;
            SrTags = selectedRecord.Tags;
            SrHasExpirationDate = selectedRecord.HasExpirationDate;
            SrExpirationDate = selectedRecord.ExpirationDate.ToString();
            SrHasNotes = selectedRecord.HasNotes;
            SrNotes = selectedRecord.Notes;
            SrCreatedDate = selectedRecord.CreatedDate.ToString();
            SrModifiedDate = selectedRecord.ModifiedDate.ToString();
            SrGuid = selectedRecord.GUID;
        }

        SetDelegateCommands();
    }

    private void SetDelegateCommands() {
        OkButtonCommand = new Utils.DelegateCommand(OnOkButtonCommand);
        CancelButtonCommand = new Utils.DelegateCommand(OnCancelButtonCommand);
    }

    //DelegateCommand event handlers
    private void OnOkButtonCommand(object obj) {
        //will need to create event args and bubble to Main Window?? -- will need to bubble regardless to change view
        //CreateRecord?.Invoke(this, EventArgs.Empty);
        //System.Diagnostics.Debug.WriteLine(
        //    $"\n\nSrTitle = {SrTitle}" +
        //    $"\nSrUsername = {SrUsername}" +
        //    $"\nSrEmail = {SrEmail}" +
        //    $"\nSrPassword = {SrPassword}" +
        //    $"\nSrUrl = {SrUrl}" +
        //    $"\nSrTags = {SrTags}" +
        //    $"\nSrHasExpirationDate = {SrHasExpirationDate}" +
        //    $"\nSrExpirationDate = {SrExpirationDate}" +
        //    $"\nSrHasNotes = {SrHasNotes}" +
        //    $"\nSrNotes = {SrNotes}\n\n");

        if (_isNewRecord) {
            CreateRecord?.Invoke(this, EventArgs.Empty);
        } else {
            UpdateRecord?.Invoke(this, EventArgs.Empty);
        }
    }
    private void OnCancelButtonCommand(object obj) {
        CancelAddEditRecord?.Invoke();// this, EventArgs.Empty);
    }
}