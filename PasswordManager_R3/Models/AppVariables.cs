using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Models;
internal static class AppVariables {
    #region Fields
    private static Utils.DatabaseOperations? _databaseConnection = null;

    private static Enums.TreeDisplayType _treeDisplayType = Enums.TreeDisplayType.ExpandAll;
    private static Enums.TreeExpandCollapseButtonStyle _treeExpandCollapseButtonStyle = Enums.TreeExpandCollapseButtonStyle.Arrows;
    private static Enums.QuickAccessIconSize _quickAccessIconSize = Enums.QuickAccessIconSize.Small;
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
    #endregion Properties

    #region Other Methods
    internal static void ClearValues() {
        _databaseConnection = null;
    }
    #endregion Other Methods
}
