using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Models;
[Serializable]
internal class AppVariables {    //might change from static and instantiate in App.xaml.cs class as global
    #region Fields
    //private Utils.DatabaseOperations? _databaseConnection = null;
    //private static string _dbPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\PasswordManager_R3\\Data\\";    //might change to readonly/constant?

    private Enums.TreeDisplayType _treeDisplayType = Enums.TreeDisplayType.ExpandAll;
    private Enums.TreeExpandCollapseButtonStyle _treeExpandCollapseButtonStyle = Enums.TreeExpandCollapseButtonStyle.Arrows;
    private Enums.QuickAccessIconSize _quickAccessIconSize = Enums.QuickAccessIconSize.Small;

    //tabitem 1
    private bool _allowAutoBackups = true;
    private int _autoBackupCount = 10;
    private string _backupLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Auto Backups"; //SpecialDirectories.MyDocuments + "\\Backups";

    //tabitem 2
    private bool _eraseDatabaseAfterSetAmountAttempts = true;
    private int _unlockAttempts = 10;
    private int _timeoutMinutes = 1;
    private bool _logDeletedItems = true;
    private bool _AreDatabaseUsernamesMasked = true;
    private bool _AreDatabaseEmailsMasked = true;
    private bool _areDatabasePasswordsMasked = true;
    private bool _AreDatabaseUrlsMasked = true;

    //tabitem 3
    private bool _displyInfoPane = true;
    #endregion Fields

    #region Properties
    //[Newtonsoft.Json.JsonIgnore]
    //internal Utils.DatabaseOperations? DatabaseConnection {
    //    get { return _databaseConnection; }
    //    set { _databaseConnection = value; }
    //}
    //internal static string DbPath {
    //    get { return _dbPath; }
    //    set { _dbPath = value; }
    //}
    [Newtonsoft.Json.JsonProperty("TreeDisplayType")]
    internal Enums.TreeDisplayType TreeDisplayType {
        get { return _treeDisplayType; }
        set { _treeDisplayType = value; }
    }
    [Newtonsoft.Json.JsonProperty("TreeExpandCollapseButtonStyle")]
    internal Enums.TreeExpandCollapseButtonStyle TreeExpandCollapseButtonStyle {
        get { return _treeExpandCollapseButtonStyle; }
        set { _treeExpandCollapseButtonStyle = value; }
    }
    [Newtonsoft.Json.JsonProperty("QuickAccessIconSize")]
    internal Enums.QuickAccessIconSize QuickAccessIconSize {
        get { return _quickAccessIconSize; }
        set { _quickAccessIconSize = value; }
    }

    //tabitem 1
    [Newtonsoft.Json.JsonProperty("AllowAutoBackups")]
    internal bool AllowAutoBackups {
        get { return _allowAutoBackups; }
        set { _allowAutoBackups = value; }
    }
    [Newtonsoft.Json.JsonProperty("AutoBackupCount")]
    internal int AutoBackupCount {
        get { return _autoBackupCount; }
        set { _autoBackupCount = value; }
    }
    [Newtonsoft.Json.JsonProperty("BackupLocation")]
    internal string BackupLocation {
        get { return _backupLocation; }
        set { _backupLocation = value; }
    }

    //tabitem 2
    [Newtonsoft.Json.JsonProperty("EraseDatabaseAfterSetAmountAttempts")]
    internal bool EraseDatabaseAfterSetAmountAttempts {
        get { return _eraseDatabaseAfterSetAmountAttempts; }
        set { _eraseDatabaseAfterSetAmountAttempts = value; }
    }
    [Newtonsoft.Json.JsonProperty("UnlockAttempts")]
    internal int UnlockAttempts {
        get { return _unlockAttempts; }
        set { _unlockAttempts = value; }
    }
    [Newtonsoft.Json.JsonProperty("TimeoutMinutes")]
    internal int TimeoutMinutes {
        get { return _timeoutMinutes; }
        set { _timeoutMinutes = value; }
    }
    [Newtonsoft.Json.JsonProperty("LogDeletedItems")]
    internal bool LogDeletedItems {
        get { return _logDeletedItems; }
        set { _logDeletedItems = value; }
    }
    [Newtonsoft.Json.JsonProperty("IsUsernameMasked")]
    internal bool AreDatabaseUsernamesMasked {
        get { return _AreDatabaseUsernamesMasked; }
        set { _AreDatabaseUsernamesMasked = value; }
    }
    [Newtonsoft.Json.JsonProperty("IsEmailMasked")]
    internal bool AreDatabaseEmailsMasked {
        get { return _AreDatabaseEmailsMasked; }
        set { _AreDatabaseEmailsMasked = value; }
    }
    [Newtonsoft.Json.JsonProperty("IsPasswordMasked")]
    internal bool AreDatabasePasswordsMasked {
        get { return _areDatabasePasswordsMasked; }
        set { _areDatabasePasswordsMasked = value; }
    }
    [Newtonsoft.Json.JsonProperty("IsUrlMasked")]
    internal bool AreDatabaseUrlsMasked {
        get { return _AreDatabaseUrlsMasked; }
        set { _AreDatabaseUrlsMasked = value; }
    }

    //tabitem 3
    [Newtonsoft.Json.JsonProperty("DisplayInfoPane")]
    internal bool DisplayInfoPane {
        get { return _displyInfoPane; }
        set { _displyInfoPane = value; }
    }
    #endregion Properties

    #region Other Methods
    //internal void ClearValues() {
    //    _databaseConnection = null;
    //}
    #endregion Other Methods
}
