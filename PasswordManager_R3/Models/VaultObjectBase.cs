using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Models;
internal class VaultObjectBase {
    //might change fields to protected...
    #region Fields
    private Group? _parentGroup;
    private string? _parentGuid;
    private string _guid;
    private string _title;  //might change to _name instead...
    private bool _hasExpirationDate;
    private DateTime? _expirationDate;
    private DateTime _createdDate;
    private DateTime _modifiedDate;
    private bool _hasNotes;
    private string _notes;
    #endregion Fields

    //might make all properties internal
    #region Properties
    public Group? ParentGroup {
        get { return _parentGroup; }
        set { _parentGroup = value; }
    }
    public string? ParentGUID {
        get { return _parentGuid; }
        set { _parentGuid = value; }
    }
    public string GUID {
        get { return _guid; }
        set { _guid = value; }
    }
    public string Title {
        get { return _title; }
        set { _title = value; }
    }
    public bool HasExpirationDate {
        get { return _hasExpirationDate; }
        set { _hasExpirationDate = value; }
    }
    public DateTime? ExpirationDate {
        get { return _expirationDate; }
        set { _expirationDate = value; }
    }
    public DateTime CreatedDate {
        get { return _createdDate; }
        set { _createdDate = value; }
    }
    public DateTime ModifiedDate {
        get { return _modifiedDate; }
        set { _modifiedDate = value; }
    }
    public bool HasNotes {
        get { return _hasNotes; }
        set { _hasNotes = value; }
    }
    public string Notes {
        get { return _notes; }
        set { _notes = value; }
    }
    #endregion Properties

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
}
