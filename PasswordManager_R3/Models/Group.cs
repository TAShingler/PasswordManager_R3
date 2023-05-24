using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Models;
internal class Group : Models.VaultObjectBase {
    #region FIELDS
    //private Group? _parent;
    //private string? _parentGuid;    //might use Guid struct instead
    private System.Collections.ObjectModel.ObservableCollection<Group> _childrenGroups;
    private System.Collections.ObjectModel.ObservableCollection<Record> _childrenRecords;
    //private string _guid;
    //private string _name;
    //private bool _hasExpirationDate;
    //private DateTime? _expirationDate;
    //private bool _hasNotes;
    //private string _notes;
    #endregion FIELDS

    #region PROPERTIES
    //public Group? Parent {
    //    get { return _parent; }
    //    set { _parent = value; }
    //}
    //public string? ParentGuid {
    //    get { return _parentGuid; }
    //    set { _parentGuid = value; }
    //}
    public System.Collections.ObjectModel.ObservableCollection<Group> ChildrenGroups {
        get { return _childrenGroups; }
        set { _childrenGroups = value; }
    }
    public System.Collections.ObjectModel.ObservableCollection<Record> ChildrenRecords {
        get { return _childrenRecords; }
        set { _childrenRecords = value; }
    }
    //public string Guid {
    //    get { return _guid; }
    //    set { _guid = value; }
    //}
    //public string Name {
    //    get { return _name; }
    //    set { _name = value; }
    //}
    //public bool HasExpriationDate {
    //    get { return _hasExpirationDate; }
    //    set { _hasExpirationDate = value; }
    //}
    //public DateTime? ExpirationDate {
    //    get { return _expirationDate; }
    //    set { _expirationDate = value; }
    //}
    //public bool HasNotes {
    //    get { return _hasNotes; }
    //    set { _hasNotes = value; }
    //}
    //public string Notes {
    //    get { return _notes; }
    //    set { _notes = value; }
    //}
    #endregion PROPERTIES

    #region CONSTRUCTORS
    public Group() : base() {
        //_parent = null;
        //_parentGuid = null;
        _childrenGroups = new System.Collections.ObjectModel.ObservableCollection<Group>();
        _childrenRecords = new System.Collections.ObjectModel.ObservableCollection<Record>();
        //_guid = string.Empty;
        //_name = string.Empty;
        //_hasExpirationDate = false;
        //_expirationDate = null;
        //_hasNotes = false;
        //_notes = string.Empty;
    }
    #endregion CONSTRUCTORS

    #region OTHER METHODS

    #endregion OTHER METHODS
}