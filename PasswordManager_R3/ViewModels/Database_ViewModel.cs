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

    //Models.Record-specific fields for information pane
    private Models.Record? _selectedRecord = null;
    private bool _srUsernameMasked;
    private bool _srEmailMasked;
    private bool _srPasswordMasked;
    private bool _srUrlMasked;
    private bool _srNotesMasked;
    private System.Windows.Media.DrawingImage _srIcon;
    private string _srTitle;
    private string _srUsername;
    private string _srEmail;
    private string _srPassword;
    private string _srUrl;
    private string _srNotes;
    private string _srExpirationDate;
    private string _srCreatedDate;
    private string _srModifiedDate;
    private string _srGuid;

    //ObservableCollection for Group objects
    private System.Collections.ObjectModel.ObservableCollection<Models.Group> _groups;
    private System.Collections.ObjectModel.ObservableCollection<Models.Record> _records;
    #endregion Fields

    #region Delegates and Events
    //delegates
    internal delegate void SelectedGroupChangedEventHandler(object obj);
    internal delegate void SelectedRecordChangedEventHandler(object obj);
    internal delegate void CreateGroupEventHandler(Models.Group g);// object sender, EventArgs e);
    internal delegate void UpdateGroupEventHandler(object sender, EventArgs e);
    internal delegate void DeleteGroupEventHandler(object sender, EventArgs e);
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
        get { return _selectedGroup; }
        set {
            _selectedGroup = value;
            OnPropertyChanged(nameof(SelectedGroup));
        }
    }

    //Models.Record-specific properties for information pane
    public Models.Record? SelectedRecord {
        get { return _selectedRecord; }
        set {
            _selectedRecord = value;
            OnPropertyChanged(nameof(SelectedRecord));
            OnPropertyChanged(nameof(SrTitle));
            OnPropertyChanged(nameof(SrUsername));
            OnPropertyChanged(nameof(SrEmail));
            OnPropertyChanged(nameof(SrPassword));
            OnPropertyChanged(nameof(SrUrl));
            OnPropertyChanged(nameof(SrNotes));
            OnPropertyChanged(nameof(SrExpirationDate));
            OnPropertyChanged(nameof(SrCreatedDate));
            OnPropertyChanged(nameof(SrModifiedDate));
            OnPropertyChanged(nameof(SrGuid));
        }
    }
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
    public string SrTitle {
        get {
            if (SelectedRecord == null)
                return string.Empty;

            return SelectedRecord.Title;
        }
    }
    public string SrUsername {
        get {
            if (SelectedRecord == null)
                return string.Empty;

            return SelectedRecord.Username;
        }
    }
    public string SrEmail {
        get {
            if (SelectedRecord == null)
                return string.Empty;

            return SelectedRecord.Email;
        }
    }
    public string SrPassword {
        get {
            if (SelectedRecord == null)
                return string.Empty;

            return SelectedRecord.Password;
        }
    }   //might use SecureString instead...
    public string SrUrl {
        get {
            if (SelectedRecord == null)
                return string.Empty;

            return SelectedRecord.URL;
        }
    }
    public string SrNotes {
        get {
            if (SelectedRecord == null)
                return string.Empty;

            return SelectedRecord.Notes;
        }
    }
    public string SrExpirationDate {
        get {
            if (SelectedRecord == null)
                return string.Empty;

            if (SelectedRecord.ExpirationDate == null)
                return string.Empty;

            return SelectedRecord.ExpirationDate.ToString();
        }
    }
    public string SrCreatedDate {
        get {
            if (SelectedRecord == null)
                return string.Empty;

            return SelectedRecord.CreatedDate.ToString();
        }
    }
    public string SrModifiedDate {
        get {
            if (SelectedRecord == null)
                return string.Empty;

            return SelectedRecord.ModifiedDate.ToString();
        }
    }
    public string SrGuid {
        get {
            if (SelectedRecord == null)
                return string.Empty;

            return SelectedRecord.GUID;
        }
    }

    //DelegetCommand Properties
    public Utils.DelegateCommand? GroupSelectedItemChangedCommand { get; set; }
    public Utils.DelegateCommand? OnRecordSelectionChangedCommand { get; set; }
    public Utils.DelegateCommand? CopyToClipboardCommand { get; set; }
    public Utils.DelegateCommand? ToggleUsernameMaskCommand { get; set; }
    public Utils.DelegateCommand? ToggleEmailMaskCommand { get; set; }
    public Utils.DelegateCommand? TogglePasswordMaskCommand { get; set; }
    public Utils.DelegateCommand? ToggleUrlMaskCommand { get; set; }
    public Utils.DelegateCommand? ToggleNotesMaskCommand { get; set; }

    public Utils.DelegateCommand? SomeCommand { get; set; }

    public Utils.DelegateCommand? CreateGroupCommand { get; set; }
    public Utils.DelegateCommand? UpdateGroupCommand { get; set; }
    public Utils.DelegateCommand? DeleteGroupCommand { get; set; }

    //ObservableCollection for Group objects
    public System.Collections.ObjectModel.ObservableCollection<Models.Group> Groups {
        get { return _groups; }
        set {
            _groups = value;
            OnPropertyChanged(nameof(Groups));
        }
    }
    public System.Collections.ObjectModel.ObservableCollection<Models.Record> Records {
        get { return _records; }
        set {
            _records = value;
            OnPropertyChanged(nameof(Records));
        }
    }
    #endregion Properties

    #region Constructors
    public Database_ViewModel(ViewModelBase parentVM) : base(parentVM) {
        //do something
        //SelectedRecord = new Models.Record() {
        //    Title = "TestTitle",
        //    Username = "TestUsername",
        //    Email = "TestEmail",
        //    Password = "TestPassword",
        //    URL = "TestUrl",
        //    Notes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Nulla facilisi nullam vehicula ipsum a arcu cursus vitae. Pellentesque diam volutpat commodo sed egestas egestas. Feugiat nisl pretium fusce id velit ut tortor. Senectus et netus et malesuada. Aliquet eget sit amet tellus cras adipiscing enim eu. Et netus et malesuada fames. Vestibulum mattis ullamcorper velit sed ullamcorper morbi. Sed euismod nisi porta lorem mollis aliquam ut porttitor. Ultricies leo integer malesuada nunc vel risus. Feugiat pretium nibh ipsum consequat nisl.\nPorttitor rhoncus dolor purus non enim praesent elementum. Blandit turpis cursus in hac habitasse platea dictumst quisque sagittis. Imperdiet sed euismod nisi porta lorem. Tortor vitae purus faucibus ornare suspendisse sed.Eleifend quam adipiscing vitae proin sagittis nisl rhoncus mattis.Integer enim neque volutpat ac.Egestas sed sed risus pretium quam vulputate.Sagittis aliquam malesuada bibendum arcu vitae elementum.Et malesuada fames ac turpis egestas sed tempus urna.Arcu vitae elementum curabitur vitae.A scelerisque purus semper eget duis. Id neque aliquam vestibulum morbi blandit cursus risus at ultrices.",//"TestNotes";
        //    ExpirationDate = DateTime.Now, //"TestExpirationDate",
        //    CreatedDate = DateTime.Today, //"TestCreatedDate",
        //    ModifiedDate = DateTime.MinValue, //"TestModifiedDate",
        //    GUID = "TestGuid"
        //};

        Groups = new();

        Models.Group root = new() { Title = "Root", GUID = Guid.NewGuid().ToString() };
        root.ChildrenRecords = new() {
                new Models.Record() {
                    Title = "TestTitle",
                    Username = "TestUsername",
                    Password = "TestPassword",
                    Email = "TestEmail",
                    GUID = Guid.NewGuid().ToString(),
                    HasExpirationDate = true,
                    ExpirationDate = DateTime.Now.AddDays(30),
                    HasNotes = true, Notes = "here are some notes..."
                },
                new Models.Record() {
                    Title = "TestTitle2",
                    Username = "TestUsername2",
                    Password = "TestPassword2",
                    Email = "TestEmail",
                    GUID = Guid.NewGuid().ToString(),
                    HasExpirationDate = true,
                    ExpirationDate = DateTime.Now.AddDays(30),
                    HasNotes = true, Notes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Nunc sed id semper risus. Justo eget magna fermentum iaculis eu non diam phasellus vestibulum. Ipsum dolor sit amet consectetur adipiscing elit ut. Diam volutpat commodo sed egestas egestas. Tortor pretium viverra suspendisse potenti nullam ac tortor vitae purus. Vel orci porta non pulvinar neque laoreet suspendisse. Pretium fusce id velit ut tortor pretium. Platea dictumst vestibulum rhoncus est pellentesque elit ullamcorper. Eget magna fermentum iaculis eu non diam. Non curabitur gravida arcu ac tortor. Eget gravida cum sociis natoque penatibus et magnis dis. Vel eros donec ac odio.\n\nAliquam etiam erat velit scelerisque in dictum non consectetur a. Ac turpis egestas maecenas pharetra convallis posuere morbi leo urna. Volutpat odio facilisis mauris sit amet. Platea dictumst vestibulum rhoncus est pellentesque elit. Purus ut faucibus pulvinar elementum integer enim neque. At quis risus sed vulputate. Sagittis orci a scelerisque purus semper eget duis at tellus. Pellentesque id nibh tortor id aliquet lectus proin nibh nisl. Congue nisi vitae suscipit tellus mauris a diam maecenas. Et molestie ac feugiat sed lectus vestibulum mattis ullamcorper velit. Morbi tristique senectus et netus et. Vestibulum lorem sed risus ultricies. Faucibus nisl tincidunt eget nullam non nisi est sit amet. Gravida rutrum quisque non tellus orci. Velit sed ullamcorper morbi tincidunt ornare massa. Purus sit amet luctus venenatis."
                },
                new Models.Record() {
                    Title = "TestTitle",
                    Username = "TestUsername",
                    Password = "TestPassword",
                    Email = "TestEmail",
                    GUID = Guid.NewGuid().ToString(),
                    HasExpirationDate = true,
                    ExpirationDate = DateTime.Now.AddDays(30),
                    HasNotes = true, Notes = "here are some notes..."
                },
                new Models.Record() {
                    Title = "TestTitle",
                    Username = "TestUsername",
                    Password = "TestPassword",
                    Email = "TestEmail",
                    GUID = Guid.NewGuid().ToString(),
                    HasExpirationDate = true,
                    ExpirationDate = DateTime.Now.AddDays(30),
                    HasNotes = true, Notes = "here are some notes..."
                },
                new Models.Record() {
                    Title = "TestTitle",
                    Username = "TestUsername",
                    Password = "TestPassword",
                    Email = "TestEmail",
                    GUID = Guid.NewGuid().ToString(),
                    HasExpirationDate = true,
                    ExpirationDate = DateTime.Now.AddDays(30),
                    HasNotes = true, Notes = "here are some notes..."
                },
                new Models.Record() {
                    Title = "TestTitle",
                    Username = "TestUsername",
                    Password = "TestPassword",
                    Email = "TestEmail",
                    GUID = Guid.NewGuid().ToString(),
                    HasExpirationDate = true,
                    ExpirationDate = DateTime.Now.AddDays(30),
                    HasNotes = true, Notes = "here are some notes..."
                },
                new Models.Record() {
                    Title = "TestTitle",
                    Username = "TestUsername",
                    Password = "TestPassword",
                    Email = "TestEmail",
                    GUID = Guid.NewGuid().ToString(),
                    HasExpirationDate = true,
                    ExpirationDate = DateTime.Now.AddDays(30),
                    HasNotes = true, Notes = "here are some notes..."
                },
                new Models.Record() {
                    Title = "TestTitle",
                    Username = "TestUsername",
                    Password = "TestPassword",
                    Email = "TestEmail",
                    GUID = Guid.NewGuid().ToString(),
                    HasExpirationDate = true,
                    ExpirationDate = DateTime.Now.AddDays(30),
                    HasNotes = true, Notes = "here are some notes..."
                },
                new Models.Record() {
                    Title = "TestTitle",
                    Username = "TestUsername",
                    Password = "TestPassword",
                    Email = "TestEmail",
                    GUID = Guid.NewGuid().ToString(),
                    HasExpirationDate = true,
                    ExpirationDate = DateTime.Now.AddDays(30),
                    HasNotes = true, Notes = "here are some notes..."
                },
                new Models.Record() {
                    Title = "TestTitle",
                    Username = "TestUsername",
                    Password = "TestPassword",
                    Email = "TestEmail",
                    GUID = Guid.NewGuid().ToString(),
                    HasExpirationDate = true,
                    ExpirationDate = DateTime.Now.AddDays(30),
                    HasNotes = true, Notes = "here are some notes..."
                },
                new Models.Record() {
                    Title = "TestTitle",
                    Username = "TestUsername",
                    Password = "TestPassword",
                    Email = "TestEmail",
                    GUID = Guid.NewGuid().ToString(),
                    HasExpirationDate = true,
                    ExpirationDate = DateTime.Now.AddDays(30),
                    HasNotes = true, Notes = "here are some notes..."
                },
                new Models.Record() {
                    Title = "TestTitle",
                    Username = "TestUsername",
                    Password = "TestPassword",
                    Email = "TestEmail",
                    GUID = Guid.NewGuid().ToString(),
                    HasExpirationDate = true,
                    ExpirationDate = DateTime.Now.AddDays(30),
                    HasNotes = true, Notes = "here are some notes..."
                },
                new Models.Record() {
                    Title = "TestTitle",
                    Username = "TestUsername",
                    Password = "TestPassword",
                    Email = "TestEmail",
                    GUID = Guid.NewGuid().ToString(),
                    HasExpirationDate = true,
                    ExpirationDate = DateTime.Now.AddDays(30),
                    HasNotes = true, Notes = "here are some notes..."
                },
                new Models.Record() {
                    Title = "TestTitle",
                    Username = "TestUsername",
                    Password = "TestPassword",
                    Email = "TestEmail",
                    GUID = Guid.NewGuid().ToString(),
                    HasExpirationDate = true,
                    ExpirationDate = DateTime.Now.AddDays(30),
                    HasNotes = true, Notes = "here are some notes..."
                },
                new Models.Record() {
                    Title = "TestTitle",
                    Username = "TestUsername",
                    Password = "TestPassword",
                    Email = "TestEmail",
                    GUID = Guid.NewGuid().ToString(),
                    HasExpirationDate = true,
                    ExpirationDate = DateTime.Now.AddDays(30),
                    HasNotes = true, Notes = "here are some notes..."
                }
            };
        root.ChildrenGroups = new() {
            new Models.Group() {
                ParentGroup = root,
                Title="Item1",
                ChildrenGroups = new() {
                    new() {
                        ChildrenGroups=new() {
                            new() {
                                ChildrenGroups = new() {
                                    new() {
                                        ChildrenGroups = new() {
                                            new() {
                                                ChildrenGroups = new() {
                                                    new() {
                                                        ChildrenGroups=new() {
                                                            new() {
                                                                ChildrenGroups = new() {
                                                                    new() {
                                                                        ChildrenGroups = new() {
                                                                            new() {
                                                                                ChildrenGroups = new() {
                                                                                    new() {
                                                                                        ChildrenGroups=new() {
                                                                                            new() {
                                                                                                ChildrenGroups = new() {
                                                                                                    new() {
                                                                                                        ChildrenGroups = new() {
                                                                                                            new()
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            },
            new Models.Group() {
                Title="Item2"
            }
        };

        Groups.Add(root);
        Groups.Add(new() {
            GUID = Guid.NewGuid().ToString()
        });
        Groups.Add(new());
        Groups.Add(new());
        Groups.Add(new());
        Groups.Add(new());
        Groups.Add(new());
        Groups.Add(new());
        Groups.Add(new());
        Groups.Add(new());
        Groups.Add(new());
        Groups.Add(new());
        Groups.Add(new());
        Groups.Add(new());
        Groups.Add(new());
        Groups.Add(new());
        Groups.Add(new());
        //Groups.Add(new());
        //Groups.Add(new());
        //Groups.Add(new());
        //Groups.Add(new());
        //Groups.Add(new());
        //Groups.Add(new());
        //Groups.Add(new());
        //Groups.Add(new());
        //Groups.Add(new());
        //Groups.Add(new());
        //Groups.Add(new());
        //Groups.Add(new());
        //Groups.Add(new());
        //Groups.Add(new());
        //Groups.Add(new());
        //Groups.Add(new());
        //Groups.Add(new());
        //Groups.Add(new());
        //Groups.Add(new());
        //Groups.Add(new());
        //Groups.Add(new());
        //Groups.Add(new());
        //Groups.Add(new());

        //SrTitle = SelectedRecord.Title;
        //SrUsername = SelectedRecord.Username;
        //SrEmail = SelectedRecord.Email;
        //SrPassword = SelectedRecord.Password;
        //SrUrl = SelectedRecord.URL;
        //SrNotes = SelectedRecord.Notes;
        //SrExpirationDate = SelectedRecord.HasExpirationDate ? SelectedRecord.ExpirationDate.ToString() : string.Empty; //"TestExpirationDate",
        //SrCreatedDate = SelectedRecord.CreatedDate.ToString(); //"TestCreatedDate",
        //SrModifiedDate = SelectedRecord.ModifiedDate.ToString(); //"TestModifiedDate",
        //SrGuid = "TestGuid";

        GroupSelectedItemChangedCommand = new Utils.DelegateCommand(OnGroupSelectionChanged);
        OnRecordSelectionChangedCommand = new Utils.DelegateCommand(OnRecordSelectionChanged);
        CopyToClipboardCommand = new Utils.DelegateCommand(CopyValueToClipboard);
        ToggleUsernameMaskCommand = new Utils.DelegateCommand(ToggleUsernameMask);
        ToggleEmailMaskCommand = new Utils.DelegateCommand(ToggleEmailMask);
        TogglePasswordMaskCommand = new Utils.DelegateCommand(TogglePasswordMask);
        ToggleUrlMaskCommand = new Utils.DelegateCommand(ToggleUrlMask);
        ToggleNotesMaskCommand = new Utils.DelegateCommand(ToggleNotesMask);

        SomeCommand = new Utils.DelegateCommand(OnSomeCommand);

        CreateGroupCommand = new Utils.DelegateCommand(OnCreateGroup);
        UpdateGroupCommand = new Utils.DelegateCommand(OnUpdateGroup);
        DeleteGroupCommand = new Utils.DelegateCommand(OnDeleteGroup);
    }
    #endregion Constructors

    // used for testing purposes
    private void OnSomeCommand(object obj) {
        //System.Diagnostics.Debug.WriteLine(obj.ToString());
        if (SelectedGroup != null)
            System.Diagnostics.Debug.WriteLine($"SelectedGroup.Title: {SelectedGroup.Title}");
        else
            System.Diagnostics.Debug.WriteLine("SelectedGroup is null");

        if (SelectedRecord != null)
            System.Diagnostics.Debug.WriteLine($"SelectedRecord.Title: {SelectedRecord.Title}");
        else
            System.Diagnostics.Debug.WriteLine("SelectedRecord is null");

    }

    #region Other Methods
    private void OnGroupSelectionChanged(object obj) {
        //do something
        SelectedGroup = null;
        //System.Diagnostics.Debug.WriteLine("OnGroupSelectionChanged obj type: " + obj.GetType());
        if (obj is Models.Group)//.GetType().Equals(typeof(Models.Group)))
            SelectedGroup = obj as Models.Group;
        //System.Diagnostics.Debug.WriteLine("SelectedGroup title: " + SelectedGroup.Title);
        SelectedGroupChanged?.Invoke(obj == null ? true : false);
    }
    private void OnRecordSelectionChanged(object obj) { //need to fix -- throwing errors
        //do somehting
        //if (obj == null)
        //    return;
        //System.Diagnostics.Debug.WriteLine("OnRecordSelectionChanged obj type: " + obj.GetType());

        //SrPassword = string.Empty;  //reset password before setting new password
        SelectedRecord = null;

        if (obj is Models.Record) {
            //Models.Record rec = (Models.Record)obj;
            //SrPassword = rec.Password;
            SelectedRecord = obj as Models.Record;
        }
        //System.Diagnostics.Debug.WriteLine("SelectedRecord title: " + SelectedRecord.Title);
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

    private void OnCreateGroup(object obj) {
        //if (obj == null) { System.Diagnostics.Debug.WriteLine("OnCreateGroup obj is null"); return; }

        //System.Diagnostics.Debug.WriteLine($"OnCreateGroup obj to string = {obj.ToString()}");
        //System.Diagnostics.Debug.WriteLine($"OnCreateGroup obj type = {obj.GetType()}");
        //System.Diagnostics.Debug.WriteLine($"OnCreateGroup obj GUID = {((Models.Group)obj).GUID}");
        //System.Diagnostics.Debug.WriteLine($"OnCreateGroup obj child groups count = {((Models.Group)obj).ChildrenGroups.Count}");
        //System.Diagnostics.Debug.WriteLine($"OnCreateGroup obj child records count = {((Models.Group)obj).ChildrenRecords.Count}");
        CreateGroup?.Invoke((Models.Group)obj);//, EventArgs.Empty);
    }
    private void OnUpdateGroup(object obj) {
        System.Diagnostics.Debug.WriteLine("OnUpdateGroup called");
        System.Diagnostics.Debug.WriteLine($"OnCreateGroup obj Title = {((Models.Group)obj).Title}");
        UpdateGroup?.Invoke(obj, EventArgs.Empty);
    }
    private void OnDeleteGroup(object obj) {
        System.Diagnostics.Debug.WriteLine("OnDeleteGroup called");
        System.Diagnostics.Debug.WriteLine($"OnCreateGroup obj Title = {((Models.Group)obj).Title}");

        //DeleteGroup?.Invoke(obj, EventArgs.Empty);  //may not need to elevate since ObservableCollections are in DatabaseVM
    }
    #endregion Other Methods
}