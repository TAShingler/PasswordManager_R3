using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Models;
internal class VaultObjectBase : System.ComponentModel.INotifyPropertyChanged {
    #region Fields
    private protected Group? _parentGroup;
    private protected string? _parentGuid;
    private protected string _guid;
    private protected string _title;  //might change to _name instead...
    private protected bool _hasExpirationDate;
    private protected DateTime? _expirationDate;
    private protected DateTime _createdDate;
    private protected DateTime _modifiedDate;
    private protected bool _hasNotes;
    private protected string _notes;
    #endregion Fields

    //might make all properties internal
    #region Properties
    [Newtonsoft.Json.JsonIgnore]
    public Group? ParentGroup {
        get { return _parentGroup; }
        set { _parentGroup = value; }
    }
    [Newtonsoft.Json.JsonProperty("ParentGuid")]
    public string? ParentGUID {
        get { return _parentGuid; }
        set { _parentGuid = value; }
    }
    [Newtonsoft.Json.JsonProperty("Guid")]
    public string GUID {
        get { return _guid; }
        set { _guid = value; }
    }
    [Newtonsoft.Json.JsonProperty("Title")]
    public string Title {
        get { return _title; }
        set { _title = value; }
    }
    [Newtonsoft.Json.JsonProperty("HasExpirationDate")]
    public bool HasExpirationDate {
        get { return _hasExpirationDate; }
        set { _hasExpirationDate = value; }
    }
    [Newtonsoft.Json.JsonProperty("ExpirationDate")]
    public DateTime? ExpirationDate {
        get { return _expirationDate; }
        set { _expirationDate = value; }
    }
    [Newtonsoft.Json.JsonProperty("CreatedDate")]
    public DateTime CreatedDate {
        get { return _createdDate; }
        set { _createdDate = value; }
    }
    [Newtonsoft.Json.JsonProperty("ModifiedDate")]
    public DateTime ModifiedDate {
        get { return _modifiedDate; }
        set { _modifiedDate = value; }
    }
    [Newtonsoft.Json.JsonProperty("HasNotes")]
    public bool HasNotes {
        get { return _hasNotes; }
        set { _hasNotes = value; }
    }
    [Newtonsoft.Json.JsonProperty("Notes")]
    public string Notes {
        get { return _notes; }
        set { _notes = value; }
    }
#endregion Properties

    #region Events
    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion Events

    #region Constructors
    public VaultObjectBase() {
            _parentGroup = null;
            _parentGuid = null;
            _guid = string.Empty;
            _title = string.Empty;
            _hasExpirationDate = false;
            _expirationDate = null;
            _createdDate = DateTime.Now;
            _modifiedDate = DateTime.Now;
            _hasNotes = false;
            _notes = string.Empty;
        }
    #endregion Constructors

    #region Event Handlers
    protected void OnPropertyChanged(string propertyName) {
        if (PropertyChanged != null) {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    #endregion Event Handlers

    #region Other Methods

    #endregion Other MEthods
}
