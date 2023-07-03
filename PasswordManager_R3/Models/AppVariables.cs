using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Models;
internal static class AppVariables {
    private static Utils.DatabaseOperations? _databaseConnection = null;

    internal static Utils.DatabaseOperations? DatabaseConnection {
        get { return _databaseConnection; }
        set { _databaseConnection = value; }
    }
    
    internal static void ClearValues() {
        _databaseConnection = null;
    }
}
