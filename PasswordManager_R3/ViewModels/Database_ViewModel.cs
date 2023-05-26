using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.ViewModels;
internal class Database_ViewModel : ViewModelBase {
    #region Fields
    private const string MASK_STRING = "\u25CF\u25CF\u25CF\u25CF\u25CF\u25CF\u25CF\u25CF\u25CF\u25CF\u25CF\u25CF\u25CF\u25CF\u25CF\u25CF";
    //Models.Group-specific fields
    private Models.Group? _selectedGroup;

    //Models.Record-specific fields for information pane
    private Models.Record? _selectedRecord;
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
    #endregion Fields

    #region Delegates and Events
    //delegates
    internal delegate void RecordSelectedEventHandler(object sender, EventArgs e);
    //events
    internal event RecordSelectedEventHandler? RecordSelected;
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
        get { return _srTitle; }
        set {
            _srTitle = value;
            OnPropertyChanged(nameof(SrTitle));
        }
    }
    public string SrUsername {
        get { return _srUsername; }
        set {
            _srUsername = value;
            OnPropertyChanged(nameof(SrUsername));
        }
    }
    public string SrEmail {
        get { return _srEmail; }
        set {
            _srEmail = value;
            OnPropertyChanged(nameof(SrEmail));
        }
    }
    public string SrPassword {
        get { return _srPassword; }
        set {
            _srPassword = value;
            OnPropertyChanged(nameof(SrPassword));
        }
    }
    public string SrUrl {
        get { return _srUrl; }
        set {
            _srUrl = value;
            OnPropertyChanged(nameof(SrUrl));
        }
    }
    public string SrNotes {
        get { return _srNotes; }
        set {
            _srNotes = value;
            OnPropertyChanged(nameof(SrNotes));
        }
    }
    public string SrExpirationDate {
        get { return _srExpirationDate; }
        set {
            _srExpirationDate = value;
            OnPropertyChanged(nameof(SrExpirationDate));
        }
    }
    public string SrCreatedDate {
        get { return _srCreatedDate; }
        set {
            _srCreatedDate = value;
            OnPropertyChanged(nameof(SrCreatedDate));
        }
    }
    public string SrModifiedDate {
        get { return _srModifiedDate; }
        set {
            _srModifiedDate = value;
            OnPropertyChanged(nameof(SrModifiedDate));
        }
    }
    public string SrGuid {
        get { return _srGuid; }
        set {
            _srGuid = value;
            OnPropertyChanged(nameof(SrGuid));
        }
    }

    //ObservableCollection for Group objects
    public System.Collections.ObjectModel.ObservableCollection<Models.Group> Groups {
        get { return _groups; }
        set {
            _groups = value;
            OnPropertyChanged(nameof(Groups));
        }
    }
    #endregion Properties

    #region Constructors
    public Database_ViewModel(ViewModelBase parentVM) : base(parentVM) {
        //do something
        SrTitle = MASK_STRING;// "TestTitle";
        SrUsername = MASK_STRING;//"TestUsername";
        SrEmail = MASK_STRING;//"TestEmail";
        SrPassword = MASK_STRING;//"TestPassword";
        SrUrl = MASK_STRING;//"TestUrl";
        SrNotes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Nulla facilisi nullam vehicula ipsum a arcu cursus vitae. Pellentesque diam volutpat commodo sed egestas egestas. Feugiat nisl pretium fusce id velit ut tortor. Senectus et netus et malesuada. Aliquet eget sit amet tellus cras adipiscing enim eu. Et netus et malesuada fames. Vestibulum mattis ullamcorper velit sed ullamcorper morbi. Sed euismod nisi porta lorem mollis aliquam ut porttitor. Ultricies leo integer malesuada nunc vel risus. Feugiat pretium nibh ipsum consequat nisl.\nPorttitor rhoncus dolor purus non enim praesent elementum. Blandit turpis cursus in hac habitasse platea dictumst quisque sagittis. Imperdiet sed euismod nisi porta lorem. Tortor vitae purus faucibus ornare suspendisse sed.Eleifend quam adipiscing vitae proin sagittis nisl rhoncus mattis.Integer enim neque volutpat ac.Egestas sed sed risus pretium quam vulputate.Sagittis aliquam malesuada bibendum arcu vitae elementum.Et malesuada fames ac turpis egestas sed tempus urna.Arcu vitae elementum curabitur vitae.A scelerisque purus semper eget duis. Id neque aliquam vestibulum morbi blandit cursus risus at ultrices.";//"TestNotes";
        SrExpirationDate = MASK_STRING;//"TestExpirationDate";
        SrCreatedDate = MASK_STRING;//"TestCreatedDate";
        SrModifiedDate = MASK_STRING;//"TestModifiedDate";
        SrGuid = MASK_STRING;//"TestGuid";
    }
    #endregion Constructors

    #region Other Methods

    #endregion Other Methods
}