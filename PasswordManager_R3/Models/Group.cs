using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Models;
[Serializable]
internal class Group : Models.VaultObjectBase {
    #region Fields
    private protected System.Collections.ObjectModel.ObservableCollection<Group> _childrenGroups;
    private protected System.Collections.ObjectModel.ObservableCollection<Record> _childrenRecords;
    private bool _isGroupExpanded = true;
    #endregion Fields

    #region Properties
    [Newtonsoft.Json.JsonIgnore]
    public System.Collections.ObjectModel.ObservableCollection<Group> ChildrenGroups {
        get { return _childrenGroups; }
        set { _childrenGroups = value; }
    }
    [Newtonsoft.Json.JsonIgnore]
    public System.Collections.ObjectModel.ObservableCollection<Record> ChildrenRecords {
        get { return _childrenRecords; }
        set { _childrenRecords = value; }
    }
    [Newtonsoft.Json.JsonIgnore]
    public bool IsGroupExpanded {
        get => _isGroupExpanded;
        set => _isGroupExpanded = value;
    }
    #endregion Properties

    #region Constructors
    public Group() : base() {
        _childrenGroups = new System.Collections.ObjectModel.ObservableCollection<Group>();
        _childrenRecords = new System.Collections.ObjectModel.ObservableCollection<Record>();
        _isGroupExpanded = true;
    }
    #endregion Constructors

    #region Other Methods

    #endregion Other Methods
}