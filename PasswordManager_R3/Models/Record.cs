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
    [Newtonsoft.Json.JsonProperty("Username")]
    public string Username {
        get { return _username; }
        set { _username = value; }
    }
    [Newtonsoft.Json.JsonProperty("Email")]
    public string Email {
        get { return _email; }
        set { _email = value; }
    }
    [Newtonsoft.Json.JsonProperty("Password")]
    public string Password {  //will need to secure in future; might change back to internal
        get { return _password; }
        set { _password = value; }
    }
    [Newtonsoft.Json.JsonProperty("Url")]
    public string URL {
        get { return _url; }
        set { _url = value; }
    }
    [Newtonsoft.Json.JsonProperty("Tags")]
    public string Tags {
        get { return _tags; }
        set { _tags = value; }
    }   //will probably remove... does not seem necessary...
    #endregion Properties

    #region Constructors
    public Record() : base() {
        _username = string.Empty;
        _email = string.Empty;
        _password = string.Empty;
        _url = string.Empty;
        _tags = string.Empty;
    }
    #endregion Constructors

    #region Other Methods

    #endregion Other Methods
}