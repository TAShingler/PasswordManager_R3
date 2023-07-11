using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordManager_R3.ViewModels;
internal class AddEditGroup_ViewModel : ViewModelBase {
    #region Fields
    private readonly bool _isNewGroup = true;
    private readonly Models.Group _parentGroup;

    //group selected in DataGrid in Databse_View
    private readonly Models.Group? _selectedGroup = null;

    //Group object fields
    private readonly string _groupPath;
    private string _sgName = string.Empty;
    private bool _sgHasExpriationDate = false;
    private string? _sgExpirationDate = string.Empty;
    private string _sgSearchOptions = string.Empty;
    private bool _sgHasNotes = false;
    private string _sgNotes = string.Empty;
    private readonly string _sgCreatedDate = string.Empty;
    private readonly string _sgUpdatedDate = string.Empty;
    private readonly string _sgGuid = string.Empty;

    //delegates
    internal delegate void CreateGroupEventHandler();
    internal delegate void UpdateGroupEventHandler();
    public delegate void CancelAddEditGroupEventHandler();

    //events
    internal event CreateGroupEventHandler? CreateGroup;
    internal event UpdateGroupEventHandler? UpdateGroup;
    public event CancelAddEditGroupEventHandler? CancelAddEditGroup;
    #endregion Fields

    #region Properties
    //Group obj properties
    public string GroupPath {
        get { return _groupPath; }
    }
    public string SgName {
        get { return _sgName; }
        set {
            _sgName = value;
            OnPropertyChanged(nameof(SgName));
        }
    }
    public bool SgHasExpirationDate {
        get { return _sgHasExpriationDate; }
        set {
            _sgHasExpriationDate = value;
            OnPropertyChanged(nameof(SgHasExpirationDate));
        }
    }
    public string? SgExpirationDate {
        get { return _sgExpirationDate; }
        set {
            _sgExpirationDate = value;
            OnPropertyChanged(nameof(SgExpirationDate));
        }
    }
    public string SgSearchOptions {
        get { return _sgSearchOptions; }
        set {
            _sgSearchOptions = value;
            OnPropertyChanged(nameof(SgSearchOptions));
        }
    }
    public bool SgHasNotes {
        get { return _sgHasNotes; }
        set {
            _sgHasNotes = value;
            OnPropertyChanged(nameof(SgHasNotes));
        }
    }
    public string SgNotes {
        get { return _sgNotes; }
        set {
            _sgNotes = value;
            OnPropertyChanged(nameof(SgNotes));
        }
    }
    public string SgCreatedDate {
        get { return _sgCreatedDate; }
    }
    public string SgUpdatedDate {
        get { return _sgUpdatedDate; }
    }
    public string SgGuid {
        get { return _sgGuid; }
    }

    //DelegateCommands
    public Utils.DelegateCommand OkButtonCommand { get; set; }
    public Utils.DelegateCommand CancelButtonCommand { get; set; }
    #endregion Properties

    #region Constructors
    public AddEditGroup_ViewModel(ViewModelBase parentVM, Models.Group? selectedGroup, bool isNewGroup = true) : base(parentVM) {
        //StringBuilder sb = new();
        //sb.Append(selectedGroup.Title);
        //sb.Append(" \u2022 ");

        //_isNewGroup = isNewGroup;

        //if (isNewGroup == false) {
        //    sb.Append();
        //    _selectedGroup = selectedGroup;

        //    SgName = selectedGroup.Title;
        //    SgHasExpirationDate = selectedGroup.HasExpirationDate;
        //    SgExpirationDate = selectedGroup.ExpirationDate != null ? selectedGroup.ExpirationDate.ToString() : string.Empty;
        //    SgSearchOptions = string.Empty; //selectedGroup.SearchOptions;
        //    SgHasNotes = selectedGroup.HasNotes;
        //    SgNotes = selectedGroup.Notes;
        //    _sgCreatedDate = selectedGroup.CreatedDate.ToString();
        //    _sgUpdatedDate = selectedGroup.ModifiedDate.ToString();
        //    _sgGuid = selectedGroup.GUID;
        //}

        ////DelegateCommands
        //OkButtonCommand = new Utils.DelegateCommand(OnOkButtonCommand);
        //CancelButtonCommand = new Utils.DelegateCommand(OnCancelButtonCommand);

        _isNewGroup = isNewGroup;

        if (isNewGroup == false) {
            _groupPath = selectedGroup.Title + " \u2022 Edit Group";
            _selectedGroup = selectedGroup;

            SgName = selectedGroup.Title;
            SgHasExpirationDate = selectedGroup.HasExpirationDate;
            SgExpirationDate = selectedGroup.ExpirationDate != null ? selectedGroup.ExpirationDate.ToString() : string.Empty;
            SgSearchOptions = string.Empty; //selectedGroup.SearchOptions;
            SgHasNotes = selectedGroup.HasNotes;
            SgNotes = selectedGroup.Notes;
            _sgCreatedDate = selectedGroup.CreatedDate.ToString();
            _sgUpdatedDate = selectedGroup.ModifiedDate.ToString();
            _sgGuid = selectedGroup.GUID;
        } else {
            _groupPath = selectedGroup.Title + " \u2022 Create Group";
        }

        OkButtonCommand = new(OnOkButtonCommand);
        CancelButtonCommand = new(OnCancelButtonCommand);
    }
    #endregion Constructors

    #region Other Methods
    private void OnOkButtonCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("AddEditGroup_ViewModel OnOkButtonCommand() executed...");
        DateTime? userExpirationDate;

        if (!String.IsNullOrEmpty(SgExpirationDate)) {
            try {
                userExpirationDate = DateTime.Parse(SgExpirationDate);
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
            $"\n\nSgTitle = {SgName}" +
            $"\nSgHasExpirationDate = {SgHasExpirationDate}" +
            $"\nSgExpriationDate = {SgExpirationDate}" +
            $"\nSgSearchOptions = {SgSearchOptions}" +
            $"\nSgHasNotes = {SgHasNotes}" +
            $"\nSgNotes = {SgNotes}" +
            $"\nSgCreatedDate = {SgCreatedDate}" +
            $"\nSgModifiedDate = {SgUpdatedDate}" +
            $"\nSgGuid = {SgGuid}\n\n");

        //check whether is new record
        if (_isNewGroup == true) { //true
            //create Models.Record obj and set values from corresponding UIElements for created obj
            Models.Group newGroup = new() {
                //ParentGUID = _parentGroup.GUID,
                //ParentGroup = _parentGroup,
                GUID = Guid.NewGuid().ToString(),   //will need method to loop through all objects in database to avoid duplicate GUIDs
                Title = SgName, 
                HasExpirationDate = SgHasExpirationDate,
                ExpirationDate = userExpirationDate,
                HasNotes = SgHasNotes,
                Notes = SgNotes
            };
            System.Diagnostics.Debug.WriteLine("AddEditGroup_ViewModel _isNewGroup = true");

            //write obj to database
            //AppVariables.DatabaseConnection.InsertData(newRecord);

            //invoke CreateRecord event to change view
            CreateGroup?.Invoke();// this, EventArgs.Empty);
        } else {    //false
            //set values from corresponding UIElements for _selectedRecord
            _selectedGroup.Title = SgName;
            _selectedGroup.HasExpirationDate = SgHasExpirationDate;
            _selectedGroup.ExpirationDate = userExpirationDate;
            _selectedGroup.HasNotes = SgHasNotes;
            _selectedGroup.Notes = SgNotes;
            _selectedGroup.ModifiedDate = DateTime.Now;
            System.Diagnostics.Debug.WriteLine("AddEditGroup_ViewModel _isNewRecord = false");

            //write updated obj to database
            //AppVariables.DatabaseConnection.InsertData(newRecord);

            //invoke UpdateRecord event to change view
            UpdateGroup?.Invoke();// this, EventArgs.Empty);
        }

    }
    private void OnCancelButtonCommand(object obj) {
        CancelAddEditGroup?.Invoke();
        //System.Diagnostics.Debug.WriteLine("Group_ViewModel OnCancelButtonCommand() executed...");
    }
    #endregion Other Methods
}
