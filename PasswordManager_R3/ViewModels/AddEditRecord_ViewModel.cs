using PasswordManager_R3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordManager_R3.ViewModels;
internal class AddEditRecord_ViewModel : ViewModelBase {
    #region Fields
    private readonly bool _isNewRecord = true;
    private readonly Models.Group _parentGroup;

    //record selected in DataGrid in Databse_View
    private readonly Models.Record? _selectedRecord = null;

    //fields for Properties View binds to
    private string _srTitle = string.Empty;
    private string _srUsername = string.Empty;
    private string _srEmail = string.Empty;
    private string _srPassword = string.Empty;
    private string _srUrl = string.Empty;
    private string _srTags = string.Empty;  //might get rid of tags...
    private bool _srHasExpirationDate = false;
    private string _srExpirationDate = string.Empty;
    private bool _srHasNotes = false;
    private string _srNotes = string.Empty;
    private readonly string _srCreatedDate = string.Empty;
    private readonly string _srModifiedDate = string.Empty;
    private readonly string _srGuid = string.Empty;
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
        get {
            if (_selectedRecord != null)
                return _selectedRecord.URL;

            return _srUrl; }
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
        get { return _srExpirationDate; }
        set {
            _srExpirationDate = value;

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
        get { return _srCreatedDate; }
        //set {
        //    _srCreatedDate = value;

        //    OnPropertyChanged(nameof(SrCreatedDate));
        //}
    }
    public string SrModifiedDate {
        get { return _srModifiedDate; }
        //set {
        //    _srModifiedDate = value;

        //    OnPropertyChanged(nameof(SrModifiedDate));
        //}
    }
    public string SrGuid {
        get { return _srGuid; }
        //set {
        //    _srGuid = value;

        //    OnPropertyChanged(nameof(SrGuid));
        //}
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
            SrExpirationDate = selectedRecord != null ? selectedRecord.ExpirationDate.ToString() : string.Empty;
            SrHasNotes = selectedRecord.HasNotes;
            SrNotes = selectedRecord.Notes;
            _srCreatedDate = selectedRecord.CreatedDate.ToString();
            _srModifiedDate = selectedRecord.ModifiedDate.ToString();
            _srGuid = selectedRecord.GUID;
        }

        SetDelegateCommands();
    }
    public AddEditRecord_ViewModel(ViewModelBase parentVM, Models.Group parentGroup, Models.Record? selectedRecord = null) {
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
            SrExpirationDate = selectedRecord != null ? selectedRecord.ExpirationDate.ToString() : string.Empty;
            SrHasNotes = selectedRecord.HasNotes;
            SrNotes = selectedRecord.Notes;
            _srCreatedDate = selectedRecord.CreatedDate.ToString();
            _srModifiedDate = selectedRecord.ModifiedDate.ToString();
            _srGuid = selectedRecord.GUID;
        }

        _parentGroup = parentGroup;
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
        DateTime? userExpirationDate;

        if (!String.IsNullOrEmpty(SrExpirationDate)) {
            try {
                userExpirationDate = DateTime.Parse(SrExpirationDate);
            } catch (FormatException ex) {
                MessageBox.Show(
                    "Expriation Date entered is not formatted correctly.",
                    "Expriation Date Format Exception",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
        } else {
            userExpirationDate = null;
        }

        System.Diagnostics.Debug.WriteLine(
            $"\n\nSrTitle = {SrTitle}" +
            $"\nSrUsername = {SrUsername}" +
            $"\nSrEmail = {SrEmail}" +
            $"\nSrPassword = {SrPassword}" +
            $"\nSrUrl = {SrUrl}" +
            $"\nSrTags = {SrTags}" +
            $"\nSrHasExpirationDate = {SrHasExpirationDate}" +
            $"\nSrExpirationDate = {SrExpirationDate}" +
            $"\nSrHasNotes = {SrHasNotes}" +
            $"\nSrNotes = {SrNotes}" +
            $"\nSrCreatedDate = {SrCreatedDate}" +
            $"\nSrModifiedDate = {SrModifiedDate}" +
            $"\nSrGuid = {SrGuid}\n\n");

        //check whether is new record
        if (_isNewRecord == true) { //true
            //create Models.Record obj and set values from corresponding UIElements for created obj
            Models.Record newRecord = new() {
                ParentGUID = _parentGroup.GUID,
                ParentGroup = _parentGroup,
                GUID = Guid.NewGuid().ToString(),   //will need method to loop through all objects in database to avoid duplicate GUIDs
                Title = SrTitle,
                Username = SrUsername,
                Email = SrEmail,
                Password = SrPassword,
                URL = SrUrl,
                Tags = SrTags,
                HasExpirationDate = SrHasExpirationDate,
                ExpirationDate = userExpirationDate,
                HasNotes = SrHasNotes,
                Notes = SrNotes
            };
            System.Diagnostics.Debug.WriteLine("AddEditRecord_ViewModel _isNewRecord = true");

            //write obj to database
            //AppVariables.DatabaseConnection.InsertData(newRecord);

            //invoke CreateRecord event to change view
            CreateRecord?.Invoke(this, EventArgs.Empty);
        } else {    //false
            //set values from corresponding UIElements for _selectedRecord
            _selectedRecord.Title = SrTitle;
            _selectedRecord.Username = SrUsername;
            _selectedRecord.Email = SrEmail;
            _selectedRecord.Password = SrPassword;
            _selectedRecord.URL = SrUrl;
            _selectedRecord.Tags = SrTags;
            _selectedRecord.HasExpirationDate = SrHasExpirationDate;
            _selectedRecord.ExpirationDate = userExpirationDate;
            _selectedRecord.HasNotes = SrHasNotes;
            _selectedRecord.Notes = SrNotes;
            _selectedRecord.ModifiedDate = DateTime.Now;
            System.Diagnostics.Debug.WriteLine("AddEditRecord_ViewModel _isNewRecord = false");

            //write updated obj to database
            //AppVariables.DatabaseConnection.InsertData(newRecord);

            //invoke UpdateRecord event to change view
            UpdateRecord?.Invoke(this, EventArgs.Empty);
        }
    }
    private void OnCancelButtonCommand(object obj) {
        CancelAddEditRecord?.Invoke();// this, EventArgs.Empty);
    }
}