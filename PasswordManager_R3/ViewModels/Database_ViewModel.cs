using PasswordManager_R3.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.ViewModels;
internal class Database_ViewModel : ViewModelBase { //}, System.Collections.Specialized.INotifyCollectionChanged {
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
    internal delegate void DeleteGroupEventHandler(object obj);// Models.Group g, int rowId);
    //events
    internal event SelectedGroupChangedEventHandler? SelectedGroupChanged;
    internal event SelectedRecordChangedEventHandler? SelectedRecordChanged;
    internal event CreateGroupEventHandler CreateGroup;
    internal event UpdateGroupEventHandler UpdateGroup;
    internal event DeleteGroupEventHandler DeleteGroup;
    #endregion Delegates and Events

    #region Properties
    //Database content values masking
    //public bool AreDatabaseUsernamesMasked {
    //    get => ((ViewModels.MainWindow_ViewModel)this.ParentVM).AreDatabaseUsernamesMasked; //((App)App.Current).AppVariables.AreDatabaseUsernamesMasked;
    //    set {
    //        //((ViewModels.MainWindow_ViewModel)this.ParentVM).AreDatabaseUsernamesMasked = value;
    //        OnPropertyChanged(nameof(this.AreDatabaseUsernamesMasked));
    //    }
    //}
    //public bool AreDatabaseEmailsMasked {
    //    get => ((App)App.Current).AppVariables.AreDatabaseEmailsMasked;
    //}
    //public bool AreDatabasePasswordsMasked {
    //    get => ((App)App.Current).AppVariables.AreDatabasePasswordsMasked;
    //}
    //public bool AreDatabaseUrlsMasked {
    //    get => ((App)App.Current).AppVariables.AreDatabaseUrlsMasked;
    //}
    //public bool IsGroupsTreePaneEnabled {
    //    get => false;
    //}

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

    public Dictionary<int, Models.Group> GroupsFromDb {
        get => _groupsFromDb;
        private set {
            _groupsFromDb = value;
            OnPropertyChanged(nameof(GroupsFromDb));
        }
    }
    public Dictionary<int, Models.Record> RecordsFromDb {
        get => _recordsFromDb;
        private set {
            _recordsFromDb = value;
            OnPropertyChanged(nameof(RecordsFromDb));
        }
    }

    //ObservableCollection for Group objects
    public System.Collections.ObjectModel.ObservableCollection<Models.Group> Groups {
        get { return _groups; }
        set {
            _groups = value;
            OnPropertyChanged(nameof(Database_ViewModel.Groups));
        }
    }

    //TreeView Expander Button properties
    public Enums.TreeExpandCollapseButtonStyle TreeExpandCollapseButtonStyle {
        get { return ((App)App.Current).AppVariables.TreeExpandCollapseButtonStyle; }
    }
    #endregion Properties

    #region Constructors
    public Database_ViewModel(ViewModelBase parentVM) : base(parentVM) {
        //RetrieveDatabaseData();

        /*foreach (var kvp in _groupsFromDb) {
            kvp.Value.IsSelected = false;
        }

        if (((MainWindow_ViewModel)ParentVM).SelectedGroup != null) {
            var selGrp = _groupsFromDb.Where(pair => pair.Value.GUID == ((MainWindow_ViewModel)ParentVM).SelectedGroup.GUID).Select(pair => pair.Value).FirstOrDefault();
            selGrp.IsSelected = true;
            System.Diagnostics.Debug.WriteLine($"selGrp = {selGrp.Title}");
        } else {
            System.Diagnostics.Debug.WriteLine("SelectedGroup is null; _groupsFromDb.ElementAt(0).Value.IsSelected set to true");
            _groupsFromDb.ElementAt(0).Value.IsSelected = true;
        }*/

        Groups = GroupsToObsColl();
        if (SelectedGroup != null) {
            System.Diagnostics.Debug.WriteLine($"Database_ViewModel constructor: SelectedGroup = {SelectedGroup.Title}");
            //Groups.Where(g => g.GUID == SelectedGroup.GUID).Select(g => g).FirstOrDefault().IsSelected = true;
            SelectedGroup = _groupsFromDb.Where(pair => pair.Key == ((MainWindow_ViewModel)ParentVM).SgRowId).Select(pair => pair.Value).FirstOrDefault();
            //var sGroup = _groupsFromDb.Where(pair => pair.Key == ((MainWindow_ViewModel)ParentVM).SgRowId).Select(pair => pair.Value).FirstOrDefault();
            //System.Diagnostics.Debug.WriteLine($"Database_ViewModel.sGroup hash == {sGroup.GetHashCode()}\n\tsGroup hash == ParentVM.SelectedGroup hash {sGroup.GetHashCode() == ((MainWindow_ViewModel)ParentVM).SelectedGroup.GetHashCode()}");
        } else {
            Groups.ElementAt(0).IsSelected = true;
        }

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
    }
    #endregion Constructors

    // used for testing purposes
    private void OnUpdateGroupExpandedState(object obj) {
        System.Diagnostics.Debug.WriteLine("obj passed to UpdateGroupExpandedState: " + obj);
    }

    #region Other Methods
    private void OnGroupSelectionChanged(object obj) {
        System.Diagnostics.Debug.WriteLine("Database_ViewModel.OnSelectedGroupChanged called...");

        if (obj == null) {
            return;
        }

        SgRowId = _groupsFromDb.Where(pair => pair.Value.GUID == ((Models.Group)obj)?.GUID).Select(pair => pair.Key).FirstOrDefault(-1);
        System.Diagnostics.Debug.WriteLine($"SgRowId = {SgRowId}");
        System.Diagnostics.Debug.WriteLine($"obj == null : {(obj == null ? true : false)}");
        System.Diagnostics.Debug.WriteLine($"obj is Group: {obj is Group}");
        //System.Diagnostics.Debug.WriteLine($"SelectedGroupChanged == null : {(SelectedGroupChanged == null ? true : false)}");
        System.Diagnostics.Debug.WriteLine($"obj as Group .Title = {((Models.Group)obj).Title}");

        System.Diagnostics.Debug.WriteLine($"SelectedGroup not yet set. SelectedGroup = {SelectedGroup?.Title}");
        SelectedGroup = obj as Models.Group;
        System.Diagnostics.Debug.WriteLine($"SelectedGroup set. SelectedGroup = {SelectedGroup?.Title}");

        CanCreateNewGroup = true;
        CanEditSelectedGroup = true;
        CanDeleteSelectedGroup = !(SelectedGroup?.ParentGroup == null);
        SelectedGroupChanged?.Invoke(obj == null ? true : false);
    }
    private void OnRecordSelectionChanged(object obj) { //need to fix -- throwing errors

        SrRowId = _recordsFromDb.Where(pair => pair.Value.GUID == ((Models.Record)obj)?.GUID).Select(pair => pair.Key).FirstOrDefault(-1);
        SelectedRecord = obj as Models.Record;
        SelectedRecordChanged?.Invoke(obj == null ? true : false);
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
    internal void OnDeleteGroup(object obj) {   //need to adjust to not have a Group object passed to this method, since the selected Group and Record objects are being set with Property binding in the View
        System.Diagnostics.Debug.WriteLine($"Database_ViewModel.OnDeleteGroup() called...");
        var objAsGroup = (Models.Group)obj;
        System.Diagnostics.Debug.WriteLine($"Database_ViewModel.OnDeleteGroup().objAsGroup == {objAsGroup.Title}");
        System.Diagnostics.Debug.WriteLine($"Database_ViewModel.OnDeleteGroup().objAsGroup.ChildrenGroups.Count == {objAsGroup.ChildrenGroups.Count}");
        //var rowId = _groupsFromDb.Where(pair => pair.Value.GUID == ((Models.Group)obj).GUID).Select(pair => pair.Key).ToList();
        List<Models.Group> groupsToDelete = new();
        List<Models.Record> recordsToDelete = new();

        System.Diagnostics.Debug.WriteLine($"Database_ViewModel.GetGroupsToDelete() called...");
        GetGroupsToDelete(objAsGroup, groupsToDelete);
        System.Diagnostics.Debug.WriteLine($"Database_ViewModel.groupsToDelete.Count = {groupsToDelete.Count}");
        foreach (var g in groupsToDelete) {
            System.Diagnostics.Debug.WriteLine($"g = {g.Title}\tg.GetHashCode() = {g.GetHashCode()}");
        }
        foreach (KeyValuePair<int, Models.Group> kvp in _groupsFromDb) {
            System.Diagnostics.Debug.WriteLine($"kvp.Value.Title = {kvp.Value.Title}\tkvp.Value.GetHashCode() = {kvp.Value.GetHashCode()}");
            System.Diagnostics.Debug.WriteLine($"groupsToDelete.Contains(kvp.Value) = {groupsToDelete.Contains(kvp.Value)}");
        }
        //}

        foreach (Models.Group grp in groupsToDelete) {
            System.Diagnostics.Debug.WriteLine($"Database_ViewModel.GetRecordsToDelete() called...");
            GetRecordsToDelete(objAsGroup, recordsToDelete);
        }
        System.Diagnostics.Debug.WriteLine($"Database_ViewModel.recordsToDelete.Count = {recordsToDelete.Count}");

        var groupsToDeleteRowIDs = _groupsFromDb.Where(pair => groupsToDelete.Contains(pair.Value)).Select(pair => pair.Key).ToList();//.ToArray();
        var recordsToDeleteRowIDs = _recordsFromDb.Where(pair => recordsToDelete.Contains(pair.Value)).Select(pair => pair.Key).ToList();//.ToArray();
        System.Diagnostics.Debug.WriteLine($"Database_ViewModel.OnDeleteGroup().groupsToDeleteRowIDs.Length == {groupsToDeleteRowIDs.Count}");
        System.Diagnostics.Debug.WriteLine($"Database_ViewModel.OnDeleteGroup().recordsToDeleteRowIDs.Length == {recordsToDeleteRowIDs.Count}");

        System.Diagnostics.Debug.WriteLine("\nList of indexes for Groups to delete...");
        foreach (var g in groupsToDeleteRowIDs) {
            System.Diagnostics.Debug.WriteLine($"\t{g}");
        }
        System.Diagnostics.Debug.WriteLine("\nList of indexes for Records to delete...");
        foreach (var r in recordsToDeleteRowIDs) {
            System.Diagnostics.Debug.WriteLine($"\t{r}");
        }

        //backup database - might make first statement in block...
        Utils.FileOperations.DatabaseBackup();

        System.Diagnostics.Debug.WriteLine($"\nDatabase_ViewModel.OnDeleteGroup() delete Groups foreach loop...");
        //int curInd = 0;
        foreach (int rowId in groupsToDeleteRowIDs) {
            //delete Group from DB
            ((App)App.Current).DatabaseOps.DeleteGroupData(rowId);

            //remove Group from Dictionary
            //GroupsFromDb.Remove(i);
            //System.Diagnostics.Debug.WriteLine($"\tcurrent index = {curInd++}; foreach item = {i}; item from _groupsFromDb = {_groupsFromDb.Where(pair => pair.Key == i).Select(pair => pair.Value.Title).FirstOrDefault()}");
        }

        System.Diagnostics.Debug.WriteLine($"\nDatabase_ViewModel.OnDeleteGroup() delete Records foreach loop...");
        foreach (int rowId in recordsToDeleteRowIDs) {
            //delete record from DB
            ((App)App.Current).DatabaseOps.DeleteRecordData(rowId);

            //remove Record from Dictionary
            //RecordsFromDb.Remove(i);
            //System.Diagnostics.Debug.WriteLine($"\tcurrent index = {curInd++}; foreach item = {i}; item from _recordsFromDb = {_recordsFromDb.Where(pair => pair.Key == i).Select(pair => pair.Value.Title).FirstOrDefault()}");
        }

        //SelectedGroup = null;
        //SelectedRecord = null;
        Groups = GroupsToObsColl();
        //DeleteGroup?.Invoke(obj);
    }   //should probably handle elsewhere... maybe...
    internal void OnCreateGroup(object obj) => CreateGroup?.Invoke();
    internal void OnUpdateGroup(object obj) => UpdateGroup?.Invoke();
    //private void OnDeleteGroup(object obj) => DeleteGroup?.Invoke();

    private void OnTreeViewLoadedCommand(object obj) {
        //System.Diagnostics.Debug.WriteLine("OnTreeViewLoadedCommand called...");
        //System.Diagnostics.Debug.WriteLine("obj type = " + obj.GetType());
        //var objAsTreeView = obj as System.Windows.Controls.TreeView;
        //System.Diagnostics.Debug.WriteLine("objAsTreeView.Items.GetItemAt(0) = " + ((Models.Group)objAsTreeView.Items.GetItemAt(0)).IsSelected);
        //Groups.ElementAt(0).IsSelected = true;
        if (SelectedGroup == null) {
            System.Diagnostics.Debug.WriteLine($"Groups.ElementAt(0) = {Groups.ElementAt(0).Title}");
            OnGroupSelectionChanged(Groups.ElementAt(0));
            //SelectedGroupChanged?.Invoke(true);
        } else {
            SelectedGroupChanged?.Invoke(false);
        }
    }

    private void RetrieveDatabaseData() {
        _groupsFromDb = ((App)App.Current).DatabaseOps.RetrieveGroupsData();
        _recordsFromDb = ((App)App.Current).DatabaseOps.RetrieveRecordsData();
    }
    private System.Collections.ObjectModel.ObservableCollection<Models.Group> GroupsToObsColl() {
        System.Diagnostics.Debug.WriteLine("\n\nGroupsToObsColl() called...");
        RetrieveDatabaseData();

        System.Collections.ObjectModel.ObservableCollection<Models.Group> groupsColl = new();

        ((MainWindow_ViewModel)ParentVM).TotalGroupsInDatabase = _groupsFromDb.Count;
        //System.Diagnostics.Debug.WriteLine($"_groupsFromDb.Count = {_groupsFromDb.Count}");
        ((MainWindow_ViewModel)ParentVM).TotalRecordsInDatabase = _recordsFromDb.Count;
        //System.Diagnostics.Debug.WriteLine($"_recordsFromDb.Count = {_recordsFromDb.Count}");

        //System.Diagnostics.Debug.WriteLine($"\nAdd groups to ObsColl...");
        //maybe redo foreach loops in future...
        foreach (Group g in _groupsFromDb.Values) {  //((MainWindow_ViewModel)ParentVM).GroupsFromDb.Values) {
            //System.Diagnostics.Debug.WriteLine($"g.Title = {g.Title}");
            //add parents to groupsColl
            if (g.ParentGUID == null) {
                //System.Diagnostics.Debug.WriteLine($"\tg.ParentGUID == null");
                //g.ParentGroup = null;
                //((ViewModels.DatabaseViewModel)DataContext).GroupsArrayList.Add(g);
                groupsColl.Add(g);
                //g.IsSelected = true;
            }
            //else {
            //    g.IsSelected = false;
            //}

            foreach (Group child in _groupsFromDb.Values) {  //((MainWindow_ViewModel)ParentVM).GroupsFromDb.Values) {
                //System.Diagnostics.Debug.WriteLine($"\tchild.Title = {child.Title}");
                if (child.ParentGUID == g.GUID) {
                    //System.Diagnostics.Debug.WriteLine($"\tchild.ParentGUID == g.GUID");
                    child.ParentGroup = g;
                    g.ChildrenGroups.Add(child);
                    //System.Diagnostics.Debug.WriteLine($"\tchild added to g.ChildrenGroups");
                }
                //child.IsSelected = false;
            }

            //g.IsSelected = false;
            if (SelectedGroup != null) {
                if (g.GUID == SelectedGroup.GUID)
                    g.IsSelected = true;
                else
                    g.IsSelected = false;
            } else {
                g.IsSelected = false;
            }
        }

        //System.Diagnostics.Debug.WriteLine($"Add Records to ObsColl");
        foreach (Record r in _recordsFromDb.Values) {    //((MainWindow_ViewModel)ParentVM).RecordsFromDb.Values) {
            //System.Diagnostics.Debug.WriteLine($"r.Title = {r.Title}");
            foreach (Group parent in _groupsFromDb.Values) { //((MainWindow_ViewModel)ParentVM).GroupsFromDb.Values) {
                //System.Diagnostics.Debug.WriteLine($"\tparent.Title = {parent.Title}");
                if (r.ParentGUID == parent.GUID) {
                    //System.Diagnostics.Debug.WriteLine($"\t\tr.ParentGUID == parent.GUID");
                    r.ParentGroup = parent;
                    parent.ChildrenRecords.Add(r);
                    //System.Diagnostics.Debug.WriteLine($"\t\tr added to parent.ChildrenRecords");
                }
            }
        }

        System.Diagnostics.Debug.WriteLine("GrupsToObsColl() terminating...");
        return groupsColl;
    }
    internal List<Models.Group> GetGroupsToDelete(Models.Group grp, List<Models.Group> groupsToDelete) {
        if (grp != null) {
            if (grp.ChildrenGroups.Count > 0) {
                foreach (Group child in grp.ChildrenGroups) {
                    groupsToDelete = GetGroupsToDelete(child, groupsToDelete);
                }
            }

            groupsToDelete.Add(grp);
        }

        return groupsToDelete;
    }   //will probably move to MainWindow_ViewModel or use delegate if unable to keep delete event in Database_View
    internal List<Models.Record> GetRecordsToDelete(Models.Group grp, List<Models.Record> recordsToDelete) {
        if (grp != null) {
            if (grp.ChildrenRecords.Count > 0) {
                foreach (Models.Record child in grp.ChildrenRecords) {
                    recordsToDelete.Add(child);
                }
            }
        }

        return recordsToDelete;
    }   //will probably move to MainWindow_ViewModel or use delegate

    //add method for expand all Groups
    //add method for collaps all Groups
    //add method for remember Group expanded state - may need to come up with diff. solution
    #endregion Other Methods
}