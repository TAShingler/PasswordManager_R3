using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3;
internal static class AppVariables {
    #region Fields
    private static Utils.DatabaseOperations? _databaseConnection = null;
    //private static string _dbPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\PasswordManager_R3\\Data\\";    //might change to readonly/constant?

    private static Enums.TreeDisplayType _treeDisplayType = Enums.TreeDisplayType.ExpandAll;
    private static Enums.TreeExpandCollapseButtonStyle _treeExpandCollapseButtonStyle = Enums.TreeExpandCollapseButtonStyle.Arrows;
    private static Enums.QuickAccessIconSize _quickAccessIconSize = Enums.QuickAccessIconSize.Small;

    //tabitem 1
    private static bool _allowAutoBackups = true;
    private static int _autoBackupCount = 10;
    private static string _backupLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Auto Backups"; //SpecialDirectories.MyDocuments + "\\Backups";

    //tabitem 2
    private static bool _eraseDatabaseAfterSetAmountAttempts = true;
    private static int _unlockAttemtps = 10;
    private static int _timeoutMinutes = 1;
    private static bool _logDeletedItems = true;

    //tabitem 3
    private static bool _displyInfoPane = true;
    #endregion Fields

    #region Properties
    internal static Utils.DatabaseOperations? DatabaseConnection {
        get { return _databaseConnection; }
        set { _databaseConnection = value; }
    }
    //internal static string DbPath {
    //    get { return _dbPath; }
    //    set { _dbPath = value; }
    //}

    internal static Enums.TreeDisplayType TreeDisplayType {
        get { return _treeDisplayType; }
        set { _treeDisplayType = value; }
    }
    internal static Enums.TreeExpandCollapseButtonStyle TreeExpandCollapseButtonStyle {
        get { return _treeExpandCollapseButtonStyle; }
        set { _treeExpandCollapseButtonStyle = value; }
    }
    internal static Enums.QuickAccessIconSize QuickAccessIconSize {
        get { return _quickAccessIconSize; }
        set { _quickAccessIconSize = value; }
    }

    //tabitem 1
    internal static bool AllowAutoBackups {
        get { return _allowAutoBackups; }
        set { _allowAutoBackups = value; }
    }
    internal static int AutoBackupCount {
        get { return _autoBackupCount; }
        set { _autoBackupCount = value; }
    }
    internal static string BackupLocation {
        get { return _backupLocation; }
        set { _backupLocation = value; }
    }

    //tabitem 2
    internal static bool EraseDatabaseAfterSetAmountAttempts {
        get { return _eraseDatabaseAfterSetAmountAttempts; }
        set { _eraseDatabaseAfterSetAmountAttempts = value; }
    }
    internal static int UnlockAttemtps {
        get { return _unlockAttemtps; }
        set { _unlockAttemtps = value; }
    }
    internal static int TimeoutMinutes {
        get { return _timeoutMinutes; }
        set { _timeoutMinutes = value; }
    }
    internal static bool LogDeletedItems {
        get { return _logDeletedItems; }
        set { _logDeletedItems = value; }
    }

    //tabitem 3
    internal static bool DisplyInfoPane {
        get { return _displyInfoPane; }
        set { _displyInfoPane = value; }
    }
    #endregion Properties

    #region Other Methods
    internal static void ClearValues() {
        _databaseConnection = null;
    }
    #endregion Other Methods
}
