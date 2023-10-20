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
    internal delegate void UpdateGroupEventHandler(Models.Group g);
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

    //TreeView Expander Button properties
    public Enums.TreeExpandCollapseButtonStyle TreeExpandCollapseButtonStyle {
        get { return ((App)App.Current).AppVariables.TreeExpandCollapseButtonStyle; }
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


        /* FOR TESTING PURPOSES ONLY
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
                    HasNotes = true, Notes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Elit ullamcorper dignissim cras tincidunt. Sapien et ligula ullamcorper malesuada proin libero nunc consequat. Turpis in eu mi bibendum neque. Facilisi morbi tempus iaculis urna. Vitae nunc sed velit dignissim sodales ut eu sem. Id venenatis a condimentum vitae. Mauris in aliquam sem fringilla ut morbi tincidunt augue interdum. Tellus molestie nunc non blandit massa enim nec dui nunc. Quis risus sed vulputate odio ut.\r\n\r\nEnim eu turpis egestas pretium aenean pharetra magna ac placerat. Quisque id diam vel quam elementum pulvinar etiam non quam. Erat imperdiet sed euismod nisi porta lorem mollis. Tortor pretium viverra suspendisse potenti nullam ac tortor vitae. Ultrices vitae auctor eu augue ut lectus arcu bibendum at. Netus et malesuada fames ac turpis. Ipsum nunc aliquet bibendum enim facilisis gravida neque convallis. Arcu cursus euismod quis viverra nibh cras pulvinar mattis nunc. Pellentesque elit ullamcorper dignissim cras tincidunt lobortis. Et molestie ac feugiat sed lectus vestibulum mattis. Nunc lobortis mattis aliquam faucibus. Eros in cursus turpis massa tincidunt dui ut ornare lectus. Sit amet tellus cras adipiscing. Et molestie ac feugiat sed. Nunc vel risus commodo viverra maecenas accumsan. Velit egestas dui id ornare. Volutpat commodo sed egestas egestas fringilla phasellus. Orci a scelerisque purus semper eget duis at. Maecenas ultricies mi eget mauris. Enim sit amet venenatis urna cursus eget.\r\n\r\nTurpis egestas pretium aenean pharetra magna ac placerat vestibulum lectus. Cursus mattis molestie a iaculis at erat pellentesque. Mauris vitae ultricies leo integer malesuada nunc vel. Malesuada proin libero nunc consequat interdum varius. Lectus vestibulum mattis ullamcorper velit sed ullamcorper morbi. Ipsum dolor sit amet consectetur adipiscing elit. Duis ut diam quam nulla porttitor massa. Placerat vestibulum lectus mauris ultrices eros in cursus turpis. Adipiscing vitae proin sagittis nisl. Commodo nulla facilisi nullam vehicula ipsum. Amet aliquam id diam maecenas ultricies mi eget.\r\n\r\nDonec ultrices tincidunt arcu non sodales neque sodales ut. Viverra vitae congue eu consequat ac. Cursus eget nunc scelerisque viverra mauris in. Urna molestie at elementum eu facilisis. Dui nunc mattis enim ut tellus. Quisque id diam vel quam elementum pulvinar etiam. Dignissim sodales ut eu sem. Mattis rhoncus urna neque viverra justo nec ultrices dui. A iaculis at erat pellentesque adipiscing commodo elit. Ultricies integer quis auctor elit sed vulputate mi sit. Pharetra et ultrices neque ornare. Quis risus sed vulputate odio ut enim. Morbi enim nunc faucibus a. Morbi tincidunt augue interdum velit euismod in pellentesque massa placerat. Eu ultrices vitae auctor eu augue ut lectus. Vitae justo eget magna fermentum iaculis. Quisque sagittis purus sit amet volutpat consequat mauris nunc congue. Dictumst vestibulum rhoncus est pellentesque elit ullamcorper. Ultrices neque ornare aenean euismod elementum nisi.\r\n\r\nPorttitor lacus luctus accumsan tortor posuere ac ut consequat. Ipsum dolor sit amet consectetur adipiscing elit. Dui accumsan sit amet nulla facilisi morbi. Sed vulputate odio ut enim blandit volutpat maecenas volutpat blandit. Lobortis mattis aliquam faucibus purus in massa tempor. Pretium quam vulputate dignissim suspendisse in est ante in. Donec enim diam vulputate ut pharetra sit amet. Laoreet id donec ultrices tincidunt arcu. Maecenas ultricies mi eget mauris pharetra et ultrices neque. Facilisis mauris sit amet massa vitae tortor condimentum lacinia. Faucibus in ornare quam viverra. Id volutpat lacus laoreet non curabitur gravida arcu ac. Commodo odio aenean sed adipiscing diam. Massa sed elementum tempus egestas sed sed risus pretium. Pellentesque eu tincidunt tortor aliquam nulla. Id neque aliquam vestibulum morbi blandit cursus risus at. Senectus et netus et malesuada fames. Pellentesque diam volutpat commodo sed egestas egestas fringilla phasellus.\r\n\r\nCursus risus at ultrices mi tempus imperdiet nulla malesuada pellentesque. Auctor eu augue ut lectus arcu bibendum at varius vel. Et ultrices neque ornare aenean euismod. Fringilla phasellus faucibus scelerisque eleifend donec. Auctor neque vitae tempus quam pellentesque nec nam aliquam. Quam quisque id diam vel quam elementum pulvinar etiam non. Enim lobortis scelerisque fermentum dui. In fermentum et sollicitudin ac orci phasellus. Quis lectus nulla at volutpat diam ut venenatis tellus. Adipiscing elit ut aliquam purus sit amet luctus venenatis lectus. At ultrices mi tempus imperdiet nulla malesuada pellentesque elit eget. Gravida neque convallis a cras semper auctor neque vitae. Non odio euismod lacinia at quis. Arcu cursus euismod quis viverra nibh. Egestas dui id ornare arcu odio. Cras fermentum odio eu feugiat. Lectus urna duis convallis convallis tellus id interdum. Volutpat est velit egestas dui id ornare arcu odio ut.\r\n\r\nVulputate enim nulla aliquet porttitor lacus luctus accumsan tortor posuere. Turpis nunc eget lorem dolor sed viverra. Egestas erat imperdiet sed euismod nisi porta. Penatibus et magnis dis parturient. Dignissim cras tincidunt lobortis feugiat. Orci a scelerisque purus semper eget. Faucibus nisl tincidunt eget nullam non nisi est. Nunc id cursus metus aliquam eleifend mi in nulla posuere. Gravida quis blandit turpis cursus in hac habitasse. Nunc faucibus a pellentesque sit amet porttitor eget dolor. Metus dictum at tempor commodo ullamcorper a lacus vestibulum. Tincidunt tortor aliquam nulla facilisi cras fermentum odio eu. Placerat in egestas erat imperdiet sed euismod nisi porta. Vel risus commodo viverra maecenas accumsan lacus vel facilisis. Ultricies lacus sed turpis tincidunt id. In nisl nisi scelerisque eu. Sapien faucibus et molestie ac feugiat sed.\r\n\r\nVel facilisis volutpat est velit egestas dui id ornare. Eleifend quam adipiscing vitae proin sagittis. Donec ac odio tempor orci dapibus ultrices in. Arcu ac tortor dignissim convallis. Quisque sagittis purus sit amet volutpat consequat mauris nunc. Est sit amet facilisis magna etiam. Nibh nisl condimentum id venenatis. Volutpat blandit aliquam etiam erat. Faucibus pulvinar elementum integer enim neque volutpat ac tincidunt vitae. Tristique magna sit amet purus gravida. Eget egestas purus viverra accumsan in. In mollis nunc sed id. Senectus et netus et malesuada. Et ligula ullamcorper malesuada proin. Potenti nullam ac tortor vitae purus faucibus ornare suspendisse. Arcu cursus vitae congue mauris rhoncus aenean vel elit. Risus at ultrices mi tempus. Egestas integer eget aliquet nibh praesent tristique.\r\n\r\nDui sapien eget mi proin sed libero enim sed. Vitae sapien pellentesque habitant morbi. Congue eu consequat ac felis donec. Quam id leo in vitae. Massa tincidunt nunc pulvinar sapien et ligula ullamcorper malesuada proin. Sit amet purus gravida quis. Non blandit massa enim nec dui nunc mattis enim ut. Cursus vitae congue mauris rhoncus aenean vel. Massa eget egestas purus viverra accumsan in nisl. Mauris vitae ultricies leo integer malesuada nunc vel. Vestibulum sed arcu non odio euismod lacinia at. Tempor nec feugiat nisl pretium fusce id velit. Nunc id cursus metus aliquam. Sit amet volutpat consequat mauris nunc congue nisi vitae. At erat pellentesque adipiscing commodo elit. Elit scelerisque mauris pellentesque pulvinar. Fermentum dui faucibus in ornare quam viverra orci.\r\n\r\nFaucibus purus in massa tempor nec feugiat. Pretium quam vulputate dignissim suspendisse. Ultrices mi tempus imperdiet nulla malesuada pellentesque elit eget gravida. Maecenas accumsan lacus vel facilisis. Odio tempor orci dapibus ultrices in iaculis nunc sed augue. Accumsan sit amet nulla facilisi morbi tempus iaculis urna id. Tortor id aliquet lectus proin nibh nisl condimentum id venenatis. Laoreet suspendisse interdum consectetur libero id faucibus. Sociis natoque penatibus et magnis dis. Mi tempus imperdiet nulla malesuada. Tortor at auctor urna nunc id. Faucibus nisl tincidunt eget nullam non nisi est sit. Facilisis magna etiam tempor orci eu lobortis elementum nibh. Hac habitasse platea dictumst vestibulum rhoncus est pellentesque elit. Enim nulla aliquet porttitor lacus luctus. Dui faucibus in ornare quam. Posuere urna nec tincidunt praesent semper feugiat nibh sed pulvinar. Aliquet eget sit amet tellus cras. Vitae turpis massa sed elementum tempus egestas sed sed."
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

        */

        Groups = GroupsAsObsColl();

        //Groups = new System.Collections.ObjectModel.ObservableCollection<Models.Group>(((MainWindow_ViewModel)ParentVM).GroupsFromDb.Values);
        //Records = new System.Collections.ObjectModel.ObservableCollection<Models.Record>(((MainWindow_ViewModel)ParentVM).RecordsFromDb.Values);

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

        //get child records
        Records = SelectedGroup?.ChildrenRecords;

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
        UpdateGroup?.Invoke(SelectedGroup);
    }
    private void OnDeleteGroup(object obj) {
        System.Diagnostics.Debug.WriteLine("OnDeleteGroup called");
        System.Diagnostics.Debug.WriteLine($"OnCreateGroup obj Title = {((Models.Group)obj).Title}");

        //DeleteGroup?.Invoke(obj, EventArgs.Empty);  //may not need to elevate since ObservableCollections are in DatabaseVM
    }

    private System.Collections.ObjectModel.ObservableCollection<Models.Group> GroupsAsObsColl() {
        System.Collections.ObjectModel.ObservableCollection<Models.Group> groupsColl = new();

        //maybe redo foreach loops in future...
        foreach (Group g in ((MainWindow_ViewModel)ParentVM).GroupsFromDb.Values) {
            //add parents to groupsColl
            if (g.ParentGUID == null) {
                //g.ParentGroup = null;
                //((ViewModels.DatabaseViewModel)DataContext).GroupsArrayList.Add(g);
                groupsColl.Add(g);
            }

            foreach (Group child in ((MainWindow_ViewModel)ParentVM).GroupsFromDb.Values) {
                if (child.ParentGUID == g.GUID) {
                    child.ParentGroup = g;
                    g.ChildrenGroups.Add(child);
                }
            }
        }

        foreach (Record r in ((MainWindow_ViewModel)ParentVM).RecordsFromDb.Values) {
            foreach (Group parent in ((MainWindow_ViewModel)ParentVM).GroupsFromDb.Values) {
                if (r.ParentGUID == parent.GUID) {
                    r.ParentGroup = parent;
                    parent.ChildrenRecords.Add(r);
                }
            }
        }

        return groupsColl;
    }
    #endregion Other Methods
}