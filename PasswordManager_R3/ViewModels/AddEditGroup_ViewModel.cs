using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.ViewModels;
internal class AddEditGroup_ViewModel : ViewModelBase {
    #region Fields
    //Group object fields
    private readonly Models.Group? _selectedGroup = new();
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
    internal delegate void CancelAddEditGroupEventHandler();

    //events
    internal event CreateGroupEventHandler? CreateGroup;
    internal event UpdateGroupEventHandler? UpdateGroup;
    internal event CancelAddEditGroupEventHandler? CancelAddEditGroup;
    #endregion Fields

    #region Properties
    //Group obj properties
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
    public AddEditGroup_ViewModel(ViewModelBase parentVM, Models.Group? selectedGroup = null) : base(parentVM) {
        //do something
        if (selectedGroup != null) {
            _selectedGroup = selectedGroup;

            SgName = selectedGroup.Title;
            SgHasExpirationDate = selectedGroup.HasExpirationDate;
            SgExpirationDate = selectedGroup.ExpirationDate.ToString();
            SgSearchOptions = string.Empty; //selectedGroup.SearchOptions;
            SgHasNotes = selectedGroup.HasNotes;
            SgNotes = selectedGroup.Notes;
            _sgCreatedDate = selectedGroup.CreatedDate.ToString();
            _sgUpdatedDate = selectedGroup.ModifiedDate.ToString();
            _sgGuid = selectedGroup.GUID;
        }

        //DelegateCommands
        OkButtonCommand = new Utils.DelegateCommand(OnOkButtonCommand);
        CancelButtonCommand = new Utils.DelegateCommand(OnCancelButtonCommand);
    }
    #endregion Constructors

    #region Other Methods
    private void OnOkButtonCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("Group_ViewModel OnOkButtonCommand() executed...");
    }
    private void OnCancelButtonCommand(object obj) {
        CancelAddEditGroup?.Invoke();
        //System.Diagnostics.Debug.WriteLine("Group_ViewModel OnCancelButtonCommand() executed...");
    }
    #endregion Other Methods
}
