﻿using System;
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
    #endregion Fields

    #region Properties
    public System.Collections.ObjectModel.ObservableCollection<Group> ChildrenGroups {
        get { return _childrenGroups; }
        set { _childrenGroups = value; }
    }
    public System.Collections.ObjectModel.ObservableCollection<Record> ChildrenRecords {
        get { return _childrenRecords; }
        set { _childrenRecords = value; }
    }
    #endregion Properties

    #region Constructors
    public Group() : base() {
        _childrenGroups = new System.Collections.ObjectModel.ObservableCollection<Group>();
        _childrenRecords = new System.Collections.ObjectModel.ObservableCollection<Record>();
    }
    #endregion Constructors

    #region Other Methods

    #endregion Other Methods
}