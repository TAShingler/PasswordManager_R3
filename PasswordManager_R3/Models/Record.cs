using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Models;
[Serializable]
internal class Record : Models.VaultObjectBase {
    #region Fields
    private protected string _username;
    private protected string _email;
    private protected string _password;
    private protected string _url;
    private protected string _tags;
    #endregion Fields

    #region Properties
    public string Username {
        get { return _username; }
        set { _username = value; }
    }
    public string Email {
        get { return _email; }
        set { _email = value; }
    }
    public string Password {  //will need to secure in future; might change back to internal
        get { return _password; }
        set { _password = value; }
    }
    public string URL {
        get { return _url; }
        set { _url = value; }
    }
    public string Tags {
        get { return _tags; }
        set { _tags = value; }
    }
    #endregion Properties

    #region Constructors
    public Record() : base() {
        _username = "TestUsername";// string.Empty;
        _email = "TestEmail";// string.Empty;
        _password = "TestPassword";// string.Empty;
        _url = "TestUrl";// string.Empty;
        _tags = "TestTags";// string.Empty;
    }
    #endregion Constructors

    #region Other Methods

    #endregion Other Methods
}