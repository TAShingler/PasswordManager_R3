using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.ViewModels;
internal class AddEditRecord_ViewModel : ViewModelBase {
    private Models.Record _record = new Models.Record();

    //might make Properties not directly reference Record obj...
    public string Title {
        get { return _record.Title; }
        set {
            _record.Title = value;

            OnPropertyChanged(nameof(Title));
        }
    }

    public string Username {
        get { return _record.Username; }
        set {
            _record.Username = value;
            OnPropertyChanged(nameof(Username));
        }
    }

    public string Email {
        get { return _record.Email; }
        set {
            _record.Email = value;
            OnPropertyChanged(nameof(Email));
        }
    }

    public string Password {
        get { return _record.Password; }
        set {
            _record.Email = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    public string URL {
        get { return _record.URL; }
        set {
            _record.Email = value;
            OnPropertyChanged(nameof(URL));
        }
    }

    //might get rid of...
    public string Tags {
        get { return _record.Tags; }
        set {
            _record.Email = value;
            OnPropertyChanged(nameof(Tags));
        }
    }

    public bool HasExpirationDate {
        get { return _record._HasExpirationDate; }
        set {
            _record._HasExpirationDate = value;
            OnPropertyChanged(nameof(HasExpirationDate));
        }
    }

    public DateTime ExpirationDate {
        get { return _record.ExpirationDate; }
        set {
            _record.Email = value;
            OnPropertyChanged(nameof(ExpirationDate));
        }
    }

    public bool HasNotes {
        get { return _record._HasNotes; }
        set {
            _record.HasNotes = value;
            OnPropertyChanged(nameof(HasNotes));
        }
    }

    public string Notes {
        get { return _record.Notes; }
        set {
            _record.Email = value;
            OnPropertyChanged(nameof(Notes));
        }
    }

    public AddEditRecord_ViewModel(ViewModelBase parentVM) : base(parentVM) {
        //do something
    }
}