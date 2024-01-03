using PasswordManager_R3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.ViewModels;
internal class Database_ViewModel : ViewModelBase {
    #region Fields
    private const string MASK_STRING = "\u25CF\u25CF\u25CF\u25CF\u25CF\u25CF\u25CF\u25CF\u25CF\u25CF\u25CF\u25CF\u25CF\u25CF\u25CF\u25CF";
    private Models.Database_Model _model = new();

    //Models.Group-specific fields
    private Models.Group? _selectedGroup = null;
    private int _sgRowId = -1;

    //Models.Record-specific fields for information pane
    private Models.Record? _selectedRecord = null;
    private int _srRowId = -1;
    private bool _srUsernameMasked;
    private bool _srEmailMasked;
    private bool _srPasswordMasked;
    private bool _srUrlMasked;
    private bool _srNotesMasked;
    private System.Windows.Media.DrawingImage _srIcon;
    //private string _srTitle;
    //private string _srUsername;
    //private string _srEmail;
    //private string _srPassword;
    //private string _srUrl;
    //private string _srNotes;
    //private string _srExpirationDate;
    //private string _srCreatedDate;
    //private string _srModifiedDate;
    //private string _srGuid;

    private Dictionary<int, Models.Group> _groupsFromDb;    //might make readonly
    private Dictionary<int, Models.Record> _recordsFromDb;  //might make readonly

    //ObservableCollection for Group objects
    private System.Collections.ObjectModel.ObservableCollection<Models.Group> _groups;
    //private System.Collections.ObjectModel.ObservableCollection<Models.Record> _records;
    #endregion Fields

    #region Delegates and Events
    //delegates
    internal delegate void SelectedGroupChangedEventHandler(object obj);
    internal delegate void SelectedRecordChangedEventHandler(object obj);
    internal delegate void CreateGroupEventHandler();// Models.Group g);// object sender, EventArgs e);
    internal delegate void UpdateGroupEventHandler();// Models.Group g, int rowId);
    internal delegate void DeleteGroupEventHandler();// Models.Group g, int rowId);
    //events
    internal event SelectedGroupChangedEventHandler? SelectedGroupChanged;
    internal event SelectedRecordChangedEventHandler? SelectedRecordChanged;
    internal event CreateGroupEventHandler CreateGroup;
    internal event UpdateGroupEventHandler UpdateGroup;
    internal event DeleteGroupEventHandler DeleteGroup;
    #endregion Delegates and Events

    #region Properties
    //Models.Group-specific properties
    public Models.Group? SelectedGroup {
        get { return ((ViewModels.MainWindow_ViewModel)ParentVM).SelectedGroup; }
        set {
            ((ViewModels.MainWindow_ViewModel)ParentVM).SelectedGroup = value;
            OnPropertyChanged(nameof(SelectedGroup));
        }
    }   //mybe make internal...
    internal int SgRowId {
        get => ((ViewModels.MainWindow_ViewModel)ParentVM).SgRowId;
        set {
            ((ViewModels.MainWindow_ViewModel)ParentVM).SgRowId = value;
            OnPropertyChanged(nameof(SgRowId));
        }
    }
    public bool CanCreateNewGroup {
        get => ((MainWindow_ViewModel)ParentVM).CanCreateNewGroup;
        set {
            ((MainWindow_ViewModel)ParentVM).CanCreateNewGroup = value;
            OnPropertyChanged(nameof(CanCreateNewGroup));
        }
    }
    public bool CanEditSelectedGroup {
        get => ((MainWindow_ViewModel)ParentVM).CanEditSelectedGroup;
        set {
            ((MainWindow_ViewModel)ParentVM).CanEditSelectedGroup = value;
            OnPropertyChanged(nameof(CanEditSelectedGroup));
        }
    }
    public bool CanDeleteSelectedGroup {
        get => ((MainWindow_ViewModel)ParentVM).CanDeleteSelectedGroup;
        set {
            ((MainWindow_ViewModel)ParentVM).CanDeleteSelectedGroup = value;
            OnPropertyChanged(nameof(CanDeleteSelectedGroup));
        }
    }

    //Models.Record-specific properties for information pane
    public Models.Record? SelectedRecord {
        get { return ((ViewModels.MainWindow_ViewModel)ParentVM).SelectedRecord; }
        set {
            ((ViewModels.MainWindow_ViewModel)ParentVM).SelectedRecord = value;
            OnPropertyChanged(nameof(SelectedRecord));
        }
    }   //maybe make internal...
    internal int SrRowId {
        get => ((ViewModels.MainWindow_ViewModel)ParentVM).SrRowId;
        set {
            ((ViewModels.MainWindow_ViewModel)ParentVM).SrRowId = value;
            OnPropertyChanged(nameof(SrRowId));
        }
    }
    //public Models.Record? SelectedRecord {
    //    get { return _selectedRecord; }
    //    set {
    //        _selectedRecord = value;
    //        OnPropertyChanged(nameof(SelectedRecord));
    //        OnPropertyChanged(nameof(SrTitle));
    //        OnPropertyChanged(nameof(SrUsername));
    //        OnPropertyChanged(nameof(SrEmail));
    //        OnPropertyChanged(nameof(SrPassword));
    //        OnPropertyChanged(nameof(SrUrl));
    //        OnPropertyChanged(nameof(SrNotes));
    //        OnPropertyChanged(nameof(SrExpirationDate));
    //        OnPropertyChanged(nameof(SrCreatedDate));
    //        OnPropertyChanged(nameof(SrModifiedDate));
    //        OnPropertyChanged(nameof(SrGuid));
    //    }
    //}
    public bool SrUsernameMasked {
        get { return _srUsernameMasked; }
        set {
            _srUsernameMasked = value;
            OnPropertyChanged(nameof(SrUsernameMasked));
        }
    }
    public bool SrEmailMasked {
        get { return _srEmailMasked; }
        set {
            _srEmailMasked = value;
            OnPropertyChanged(nameof(SrEmailMasked));
        }
    }
    public bool SrPasswordMasked {
        get { return _srPasswordMasked; }
        set {
            _srPasswordMasked = value;
            OnPropertyChanged(nameof(SrPasswordMasked));
        }
    }
    public bool SrUrlMasked {
        get { return _srUrlMasked; }
        set {
            _srUrlMasked = value;
            OnPropertyChanged(nameof(SrUrlMasked));
        }
    }
    public bool SrNotesMasked {
        get { return _srNotesMasked; }
        set {
            _srNotesMasked = value;
            OnPropertyChanged(nameof(SrNotesMasked));
        }
    }
    public System.Windows.Media.DrawingImage SrIcon {
        get { return _srIcon; }
        set { _srIcon = value;}
    }
    //public string SrTitle {
    //    get {
    //        if (SelectedRecord == null)
    //            return string.Empty;

    //        return SelectedRecord.Title;
    //    }
    //}
    //public string SrUsername {
    //    get {
    //        if (SelectedRecord == null)
    //            return string.Empty;

    //        return SelectedRecord.Username;
    //    }
    //}
    //public string SrEmail {
    //    get {
    //        if (SelectedRecord == null)
    //            return string.Empty;

    //        return SelectedRecord.Email;
    //    }
    //}
    //public string SrPassword {
    //    get {
    //        if (SelectedRecord == null)
    //            return string.Empty;

    //        return SelectedRecord.Password;
    //    }
    //}   //might use SecureString instead...
    //public string SrUrl {
    //    get {
    //        if (SelectedRecord == null)
    //            return string.Empty;

    //        return SelectedRecord.URL;
    //    }
    //}
    //public string SrNotes {
    //    get {
    //        if (SelectedRecord == null)
    //            return string.Empty;

    //        return SelectedRecord.Notes;
    //    }
    //}
    //public string SrExpirationDate {
    //    get {
    //        if (SelectedRecord == null)
    //            return string.Empty;

    //        if (SelectedRecord.ExpirationDate == null)
    //            return string.Empty;

    //        return SelectedRecord.ExpirationDate.ToString();
    //    }
    //}
    //public string SrCreatedDate {
    //    get {
    //        if (SelectedRecord == null)
    //            return string.Empty;

    //        return SelectedRecord.CreatedDate.ToString();
    //    }
    //}
    //public string SrModifiedDate {
    //    get {
    //        if (SelectedRecord == null)
    //            return string.Empty;

    //        return SelectedRecord.ModifiedDate.ToString();
    //    }
    //}
    //public string SrGuid {
    //    get {
    //        if (SelectedRecord == null)
    //            return string.Empty;

    //        return SelectedRecord.GUID;
    //    }
    //}

    //DelegetCommand Properties
    public Utils.DelegateCommand? GroupSelectedItemChangedCommand { get; set; }
    public Utils.DelegateCommand? OnRecordSelectionChangedCommand { get; set; }
    public Utils.DelegateCommand? CopyToClipboardCommand { get; set; }
    public Utils.DelegateCommand? ToggleUsernameMaskCommand { get; set; }
    public Utils.DelegateCommand? ToggleEmailMaskCommand { get; set; }
    public Utils.DelegateCommand? TogglePasswordMaskCommand { get; set; }
    public Utils.DelegateCommand? ToggleUrlMaskCommand { get; set; }
    public Utils.DelegateCommand? ToggleNotesMaskCommand { get; set; }

    public Utils.DelegateCommand? UpdateGroupExpandedState { get; set; }

    public Utils.DelegateCommand? CreateGroupCommand { get; set; }
    public Utils.DelegateCommand? UpdateGroupCommand { get; set; }
    public Utils.DelegateCommand? DeleteGroupCommand { get; set; }

    public Utils.DelegateCommand? TreeViewLoadedCommand { get; set; }

    //ObservableCollection for Group objects
    public System.Collections.ObjectModel.ObservableCollection<Models.Group> Groups {
        get { return _groups; }
        set {
            _groups = value;
            OnPropertyChanged(nameof(Groups));
        }
    }
    //public System.Collections.ObjectModel.ObservableCollection<Models.Record> Records {
    //    get { return _records; }
    //    set {
    //        _records = value;
    //        OnPropertyChanged(nameof(Records));
    //    }
    //}

    //internal Dictionary<int, Models.Group> GroupsFromDb {   //might remove completely
    //    get => _groupsFromDb;
    //    //set => _groupsFromDb = value;
    //}
    //internal Dictionary<int, Models.Record> RecordsFromDb { //might remove completely
    //    get => _recordsFromDb;
    //    //set => _recordsFromDb = value;
    //}

    //TreeView Expander Button properties
    public Enums.TreeExpandCollapseButtonStyle TreeExpandCollapseButtonStyle {
        get { return ((App)App.Current).AppVariables.TreeExpandCollapseButtonStyle; }
    }
    #endregion Properties

    #region Constructors
    public Database_ViewModel(ViewModelBase parentVM) : base(parentVM) {
        _groupsFromDb = ((App)App.Current).DatabaseOps.RetrieveGroupsData();
        _recordsFromDb = ((App)App.Current).DatabaseOps.RetrieveRecordsData();

        foreach (var kvp in _groupsFromDb) {
            kvp.Value.IsSelected = false;
        }

        if (((MainWindow_ViewModel)ParentVM).SelectedGroup != null) {
            var selGrp = _groupsFromDb.Where(pair => pair.Value.GUID == ((MainWindow_ViewModel)ParentVM).SelectedGroup.GUID).Select(pair => pair.Value).FirstOrDefault();
            selGrp.IsSelected = true;
            System.Diagnostics.Debug.WriteLine($"selGrp = {selGrp.Title}");
        } else {
            System.Diagnostics.Debug.WriteLine("SelectedGroup is null; _groupsFromDb.ElementAt(0).Value.IsSelected set to true");
            _groupsFromDb.ElementAt(0).Value.IsSelected = true;
        }
        ////read Group objects from database
        //_groupsFromDb = ((App)App.Current).DatabaseOps.RetrieveGroupsData();

        ////read Record objects from database
        //_recordsFromDb = ((App)App.Current).DatabaseOps.RetrieveRecordsData();

        ////if (((MainWindow_ViewModel)ParentVM).GroupsFromDb.Values != null) {
        //if (_groupsFromDb.Values != null) {
        Groups = GroupsToObsColl();
        //}

        //((MainWindow_ViewModel)ParentVM).TotalGroupsInDatabase = _groupsFromDb.Count;
        //((MainWindow_ViewModel)ParentVM).TotalRecordsInDatabase = _recordsFromDb.Count;

        GroupSelectedItemChangedCommand = new Utils.DelegateCommand(OnGroupSelectionChanged);
        OnRecordSelectionChangedCommand = new Utils.DelegateCommand(OnRecordSelectionChanged);
        CopyToClipboardCommand = new Utils.DelegateCommand(CopyValueToClipboard);
        ToggleUsernameMaskCommand = new Utils.DelegateCommand(ToggleUsernameMask);
        ToggleEmailMaskCommand = new Utils.DelegateCommand(ToggleEmailMask);
        TogglePasswordMaskCommand = new Utils.DelegateCommand(TogglePasswordMask);
        ToggleUrlMaskCommand = new Utils.DelegateCommand(ToggleUrlMask);
        ToggleNotesMaskCommand = new Utils.DelegateCommand(ToggleNotesMask);

        UpdateGroupExpandedState = new Utils.DelegateCommand(OnUpdateGroupExpandedState);

        CreateGroupCommand = new Utils.DelegateCommand(OnCreateGroup);
        UpdateGroupCommand = new Utils.DelegateCommand(OnUpdateGroup);
        DeleteGroupCommand = new Utils.DelegateCommand(OnDeleteGroup);

        TreeViewLoadedCommand = new(OnTreeViewLoadedCommand);

        //Groups.ElementAt(0).IsSelected = true;
        //OnGroupSelectionChanged(Groups[0]);
    }
    #endregion Constructors

    // used for testing purposes
    private void OnUpdateGroupExpandedState(object obj) {
        System.Diagnostics.Debug.WriteLine("obj passed to UpdateGroupExpandedState: " + obj);
    }

    #region Other Methods
    private void OnGroupSelectionChanged(object obj) {
        System.Diagnostics.Debug.WriteLine("Database_ViewModel.OnSelectedGroupChanged called...");
        //System.Diagnostics.Debug.WriteLine("obj.GetType() = " + obj.GetType());
        //do something
        //SelectedGroup = null;
        //System.Diagnostics.Debug.WriteLine("OnGroupSelectionChanged obj type: " + obj.GetType());
        //if (obj is Models.Group)//.GetType().Equals(typeof(Models.Group)))
        //    SelectedGroup = obj as Models.Group;
        //System.Diagnostics.Debug.WriteLine("SelectedGroup title: " + SelectedGroup.Title);

        //get child records
        //Records = SelectedGroup?.ChildrenRecords;

        //System.Diagnostics.Debug.WriteLine($"Currently selected Group: {((Models.Group)obj).Title}");

        if (obj == null) {
            return;
        }

        SgRowId = _groupsFromDb.Where(pair => pair.Value.GUID == ((Models.Group)obj)?.GUID).Select(pair => pair.Key).FirstOrDefault(-1);
        System.Diagnostics.Debug.WriteLine($"obj == null : {(obj == null ? true : false)}");
        System.Diagnostics.Debug.WriteLine($"obj is Group: {obj is Group}");
        //System.Diagnostics.Debug.WriteLine($"SelectedGroupChanged == null : {(SelectedGroupChanged == null ? true : false)}");
        System.Diagnostics.Debug.WriteLine($"obj as Group .Title = {((Models.Group)obj).Title}");

        System.Diagnostics.Debug.WriteLine($"SelectedGroup not yet set. SelectedGroup = {SelectedGroup?.Title}");
        SelectedGroup = obj as Models.Group;
        System.Diagnostics.Debug.WriteLine($"SelectedGroup set. SelectedGroup = {SelectedGroup?.Title}");

        //if (SelectedGroup.ParentGroup is null) {
        //    //System.Diagnostics.Debug.WriteLine("SelectedGroup.ParentGroup == null");
        //    CanDeleteSelectedGroup = false;
        //} else {
        //    CanDeleteSelectedGroup = true;
        //}
        //if (SelectedGroup != null) {
        CanCreateNewGroup = true;
        CanEditSelectedGroup = true;
        CanDeleteSelectedGroup = !(SelectedGroup?.ParentGroup == null);
        //}
        SelectedGroupChanged?.Invoke(obj == null ? true : false);
        //SelectedGroupChanged?.Invoke(obj);
    }
    private void OnRecordSelectionChanged(object obj) { //need to fix -- throwing errors
        //do somehting
        //if (obj == null)
        //    return;
        //System.Diagnostics.Debug.WriteLine("OnRecordSelectionChanged obj type: " + obj.GetType());

        //SrPassword = string.Empty;  //reset password before setting new password
        //SelectedRecord = null;

        //if (obj is Models.Record) {
        //    //Models.Record rec = (Models.Record)obj;
        //    //SrPassword = rec.Password;
        //    SelectedRecord = obj as Models.Record;
        //}
        //System.Diagnostics.Debug.WriteLine("SelectedRecord title: " + SelectedRecord.Title);
        //SelectedRecordChanged?.Invoke(obj);//obj == null ? true : false);

        //if (obj == null) { return; }

        SrRowId = _recordsFromDb.Where(pair => pair.Value.GUID == ((Models.Record)obj)?.GUID).Select(pair => pair.Key).FirstOrDefault(-1);
        SelectedRecord = obj as Models.Record;
        SelectedRecordChanged?.Invoke(obj == null ? true : false);
        //System.Diagnostics.Debug.WriteLine("obj to string: " + obj.ToString());
    }
    private void CopyValueToClipboard(object obj) {
        //do something
        System.Windows.Clipboard.Clear();
        System.Windows.Clipboard.SetText(obj.ToString());
    }
    private void ToggleUsernameMask(object obj) {
        if (_selectedRecord == null) { return; }

        bool objAsBool = (bool)obj;

        if (objAsBool == true) {
            //mask
            //SrUsername = MASK_STRING;
        } else {
            //unmask
            //SrUsername = SelectedRecord.Username;
        }

        System.Diagnostics.Debug.WriteLine("ToggleUsernameMask");
    }
    private void ToggleEmailMask(object obj) {
        if (_selectedRecord == null) { return; }

        bool objAsBool = (bool)obj;

        if (objAsBool == true) {
            //mask
            //SrEmail = MASK_STRING;
        } else {
            //unmask
            //SrEmail = SelectedRecord.Email;
        }
    }
    private void TogglePasswordMask(object obj) {
        if (_selectedRecord == null) { return; }

        bool objAsBool = (bool)obj;

        if (objAsBool == true) {
            //mask
            //SrPassword = MASK_STRING;
        } else {
            //unmask
            //SrPassword = SelectedRecord.Password;
        }
    }
    private void ToggleUrlMask(object obj) {
        if (_selectedRecord == null) { return; }

        bool objAsBool = (bool)obj;

        if (objAsBool == true) {
            //mask
            //SrUrl = MASK_STRING;
        } else {
            //unmask
            //SrUrl = SelectedRecord.URL;
        }
    }
    private void ToggleNotesMask(object obj) {
        if (_selectedRecord == null) { return; }

        bool objAsBool = (bool)obj;

        if (objAsBool == true) {
            //mask
            //SrNotes = MASK_STRING;
        } else {
            //unmask
            //SrNotes = SelectedRecord.Notes;
        }
    }

    //private void OnCreateGroup(object obj) {
    //    //if (obj == null) { System.Diagnostics.Debug.WriteLine("OnCreateGroup obj is null"); return; }

    //    //System.Diagnostics.Debug.WriteLine($"OnCreateGroup obj to string = {obj.ToString()}");
    //    //System.Diagnostics.Debug.WriteLine($"OnCreateGroup obj type = {obj.GetType()}");
    //    //System.Diagnostics.Debug.WriteLine($"OnCreateGroup obj GUID = {((Models.Group)obj).GUID}");
    //    //System.Diagnostics.Debug.WriteLine($"OnCreateGroup obj child groups count = {((Models.Group)obj).ChildrenGroups.Count}");
    //    //System.Diagnostics.Debug.WriteLine($"OnCreateGroup obj child records count = {((Models.Group)obj).ChildrenRecords.Count}");
    //    CreateGroup?.Invoke();// (Models.Group)obj);//, EventArgs.Empty);
    //}
    //private void OnUpdateGroup(object obj) {
    //    //var rowId = _groupsFromDb.Where(pair => pair.Value.GUID == ((Models.Group)obj).GUID).Select(pair => pair.Key).FirstOrDefault();

    //    UpdateGroup?.Invoke();// (Models.Group)obj, rowId);
    //}
    internal void OnDeleteGroup(object obj) {   //need to adjust to not have a Group object passed to this method, since the selected Group and Record objects are being set with Property binding in the View
        System.Diagnostics.Debug.WriteLine($"Database_ViewModel.OnDeleteGroup() called...");
        var objAsGroup = (Models.Group)obj;
        var rowId = _groupsFromDb.Where(pair => pair.Value.GUID == ((Models.Group)obj).GUID).Select(pair => pair.Key).ToList();
        List<Models.Group> groupsToDelete = new();
        List<Models.Record> recordsToDelete = new();

        System.Diagnostics.Debug.WriteLine($"Database_ViewModel.GetGroupsToDelete() called...");
        GetGroupsToDelete(objAsGroup, groupsToDelete);

        foreach (Models.Group grp in groupsToDelete) {
            System.Diagnostics.Debug.WriteLine($"Database_ViewModel.GetRecordsToDelete() called...");
            GetRecordsToDelete(objAsGroup, recordsToDelete);
        }

        var groupsToDeleteRowIDs = _groupsFromDb.Where(pair => groupsToDelete.Contains(pair.Value)).Select(pair => pair.Key).ToList().ToArray();
        var recordsToDeleteRowIDs = _recordsFromDb.Where(pair => recordsToDelete.Contains(pair.Value)).Select(pair => pair.Key).ToList().ToArray();

        foreach (int i in groupsToDeleteRowIDs) {
            System.Diagnostics.Debug.WriteLine($"App.DatabaseOps.DeleteGroupData() called...");
            ((App)App.Current).DatabaseOps.DeleteGroupData(i);
        }

        foreach (int i in recordsToDeleteRowIDs) {
            System.Diagnostics.Debug.WriteLine($"App.DatabaseOps.DeleteRecordData() called...");
            ((App)App.Current).DatabaseOps.DeleteRecordData(i);
        }

        _groupsFromDb.Clear();
        _recordsFromDb.Clear();

        _groupsFromDb = ((App)App.Current).DatabaseOps.RetrieveGroupsData();
        _recordsFromDb = ((App)App.Current).DatabaseOps.RetrieveRecordsData();

        //System.Diagnostics.Debug.WriteLine($"Database_ViewModel.Groups.Clear() called...");
        //Groups.Clear();

        //Groups = new();
        System.Diagnostics.Debug.WriteLine($"Database_ViewModel.GroupsToObsColl() called...");
        Groups = GroupsToObsColl();
        /*OnGroupSelectionChanged(*/
        //Groups.ElementAt(0);//);

        //backup DB
        Utils.FileOperations.DatabaseBackup();
        System.Diagnostics.Debug.WriteLine($"Database_ViewModel.OnDeleteGroup() terminating...");
    }   //should probably handle elsewhere... maybe...
    private void OnCreateGroup(object obj) => CreateGroup?.Invoke();
    private void OnUpdateGroup(object obj) => UpdateGroup?.Invoke();
    //private void OnDeleteGroup(object obj) => DeleteGroup?.Invoke();

    private void OnTreeViewLoadedCommand(object obj) {
        //System.Diagnostics.Debug.WriteLine("OnTreeViewLoadedCommand called...");
        //System.Diagnostics.Debug.WriteLine("obj type = " + obj.GetType());
        //var objAsTreeView = obj as System.Windows.Controls.TreeView;
        //System.Diagnostics.Debug.WriteLine("objAsTreeView.Items.GetItemAt(0) = " + ((Models.Group)objAsTreeView.Items.GetItemAt(0)).IsSelected);
        //Groups.ElementAt(0).IsSelected = true;
        if (SelectedGroup == null)
            OnGroupSelectionChanged(Groups.ElementAt(0));
        else
            SelectedGroupChanged?.Invoke(false);
    }

    private System.Collections.ObjectModel.ObservableCollection<Models.Group> GroupsToObsColl() {
        System.Collections.ObjectModel.ObservableCollection<Models.Group> groupsColl = new();

        ((MainWindow_ViewModel)ParentVM).TotalGroupsInDatabase = _groupsFromDb.Count;
        ((MainWindow_ViewModel)ParentVM).TotalRecordsInDatabase = _recordsFromDb.Count;

        //maybe redo foreach loops in future...
        foreach (Group g in _groupsFromDb.Values) {  //((MainWindow_ViewModel)ParentVM).GroupsFromDb.Values) {
            //add parents to groupsColl
            if (g.ParentGUID == null) {
                //g.ParentGroup = null;
                //((ViewModels.DatabaseViewModel)DataContext).GroupsArrayList.Add(g);
                groupsColl.Add(g);
            }

            foreach (Group child in _groupsFromDb.Values) {  //((MainWindow_ViewModel)ParentVM).GroupsFromDb.Values) {
                if (child.ParentGUID == g.GUID) {
                    child.ParentGroup = g;
                    g.ChildrenGroups.Add(child);
                }
            }
        }

        foreach (Record r in _recordsFromDb.Values) {    //((MainWindow_ViewModel)ParentVM).RecordsFromDb.Values) {
            foreach (Group parent in _groupsFromDb.Values) { //((MainWindow_ViewModel)ParentVM).GroupsFromDb.Values) {
                if (r.ParentGUID == parent.GUID) {
                    r.ParentGroup = parent;
                    parent.ChildrenRecords.Add(r);
                }
            }
        }

        return groupsColl;
    }
    private List<Models.Group> GetGroupsToDelete(Models.Group grp, List<Models.Group> groupsToDelete) {
        if (grp != null) {
            if (grp.ChildrenGroups.Count > 0) {
                foreach (Group child in grp.ChildrenGroups) {
                    groupsToDelete = GetGroupsToDelete(child, groupsToDelete);
                }
            }

            groupsToDelete.Add(grp);
        }

        return groupsToDelete;
    }
    private List<Models.Record> GetRecordsToDelete(Models.Group grp, List<Models.Record> recordsToDelete) {
        if (grp != null) {
            if (grp.ChildrenRecords.Count > 0) {
                foreach (Models.Record child in grp.ChildrenRecords) {
                    recordsToDelete.Add(child);
                }
            }
        }

        return recordsToDelete;
    }

    //add method for expand all Groups
    //add method for collaps all Groups
    //add method for remember Group expanded state - may need to come up with diff. solution
    #endregion Other Methods
}