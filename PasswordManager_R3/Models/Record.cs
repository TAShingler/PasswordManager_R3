﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Models;
[Serializable]
internal class Record : Models.VaultObjectBase {
    #region Fields
    private protected int _icon;
    private protected string _username;
    private protected string _email;
    private protected string _password;
    private protected string _passwordMasked = "\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022";
    private protected string _url;
    private protected string _tags;
    private protected bool _isUsernameMasked;
    private protected bool _isEmailMasked;
    private protected bool _isPasswordMasked;
    private protected bool _isUrlMasked;
    #endregion Fields

    #region Properties
    [Newtonsoft.Json.JsonProperty("Icon")]
    public int Icon {
        get => _icon;
        set => _icon = value;
    }
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
    [Newtonsoft.Json.JsonIgnore]
    public string PasswordMasked {
        get => _passwordMasked;
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
    [Newtonsoft.Json.JsonProperty("IsUsernameMasked")]
    public bool IsUsernameMasked {
        get => _isUsernameMasked;
        set {
            _isUsernameMasked = value;
            OnPropertyChanged(nameof(IsUsernameMasked));
        }
    }
    [Newtonsoft.Json.JsonProperty("IsEmailMasked")]
    public bool IsEmailMasked {
        get => _isEmailMasked;
        set {
            _isEmailMasked = value;
            OnPropertyChanged(nameof(IsEmailMasked));
        }
    }
    [Newtonsoft.Json.JsonProperty("IsPasswordMasked")]
    public bool IsPasswordMasked {
        get => _isPasswordMasked;
        set {
            _isPasswordMasked = value;
            OnPropertyChanged(nameof(IsPasswordMasked));
        }
    }
    [Newtonsoft.Json.JsonProperty("IsUrlMasked")]
    public bool IsUrlMasked {
        get => _isUrlMasked;
        set {
            _isUrlMasked = value;
            OnPropertyChanged(nameof(IsUrlMasked));
        }
    }
    #endregion Properties

    #region Constructors
    public Record() : base() {
        _icon = 29;
        _username = string.Empty;
        _email = string.Empty;
        _password = string.Empty;
        _url = string.Empty;
        _tags = string.Empty;
        _isUsernameMasked = true;
        _isEmailMasked = true;
        _isPasswordMasked = true;
        _isUrlMasked = true;
    }
    #endregion Constructors

    #region Other Methods

    #endregion Other Methods
}