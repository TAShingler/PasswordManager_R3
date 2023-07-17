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

    private static Enums.TreeDisplayType _treeDisplayType = Enums.TreeDisplayType.ExpandAll;
    private static Enums.TreeExpandCollapseButtonStyle _treeExpandCollapseButtonStyle = Enums.TreeExpandCollapseButtonStyle.Arrows;
    private static Enums.QuickAccessIconSize _quickAccessIconSize = Enums.QuickAccessIconSize.Small;

    //tabitem 1
    private static bool _allowAutoBackups = true;
    private static string _databaseBackupsPath = SpecialDirectories.MyDocuments + "\\Backups";

    //tabitem 2
    private static bool _eraseDatabaseAfterSetAmountAttempts = true;
    private static bool _logDeletedItems = true;

    //tabitem 3
    private static bool _displyInfoPane = true;
    #endregion Fields

    #region Properties
    internal static Utils.DatabaseOperations? DatabaseConnection {
        get { return _databaseConnection; }
        set { _databaseConnection = value; }
    }

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
    internal static string DatabaseBackupsPath {
        get { return _databaseBackupsPath; }
        set { _databaseBackupsPath = value; }
    }

    //tabitem 2
    internal static bool EraseDatabaseAfterSetAmountAttempts {
        get { return _eraseDatabaseAfterSetAmountAttempts; }
        set { _eraseDatabaseAfterSetAmountAttempts = value; }
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
