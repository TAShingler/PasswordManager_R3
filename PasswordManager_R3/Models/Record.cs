﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Models;
internal class Record {
    private string _title;
    private string _username;
    private string _email;
    private string _password;
    private string _url;
    private string _tags;
    private bool _hasExpirationDate;
    private DateTime _expirationDate;
    private bool _hasNotes;
    private string _notes;

    public string Title {
        get { return _title; }
        set { _title = value; }
    }
    public string Username {
        get { return _username; }
        set { _username = value; }
    }
    public string Email {
        get { return _email; }
        set { _email = value; }
    }
    internal string Password {
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
    public bool HasExpirationDate {
        get { return _hasExpirationDate; }
        set { _hasExpirationDate = value; }
    }
    public DateTime ExpirationDate {
        get { return _expirationDate; }
        set { _expirationDate = value; }
    }
    public bool HasNotes {
        get { return _hasNotes; }
        set { _hasNotes = value; }
    }
    public string Notes {
        get { return _notes; }
        set { _notes = value; }

    }

    public Record() {
        _title = "TestTitle";// string.Empty;
        _username = "TestUSername";// string.Empty;
        _email = "TestEmail";// string.Empty;
        _password = "TestPassword";// string.Empty;
        _url = "TestUrl";// string.Empty;
        _tags = "TestTags";// string.Empty;
        _hasExpirationDate = false;
        _expirationDate = DateTime.MinValue;
        _hasNotes = false;
        _notes = "TestNotes";// string.Empty;
    }
}