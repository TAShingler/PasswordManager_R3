using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.ViewModels;
internal class AddEditRecord_ViewModel : ViewModelBase {
    private Models.Record _record = new();

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

    //might get rid of tags...
    public string Tags {
        get { return _record.Tags; }
        set {
            _record.Email = value;
            OnPropertyChanged(nameof(Tags));
        }
    }
    public bool HasExpirationDate {
        get { return _record.HasExpirationDate; }
        set {
            _record.HasExpirationDate = value;
            OnPropertyChanged(nameof(HasExpirationDate));
        }
    }
    public DateTime? ExpirationDate {
        get { return _record.ExpirationDate; }
        set {
            _record.ExpirationDate = value;
            OnPropertyChanged(nameof(ExpirationDate));
        }
    }
    public bool HasNotes {
        get { return _record.HasNotes; }
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

    //DelegateCommands
    public Utils.DelegateCommand CreateRecordCommand { get; set; }

    //Delegates to bubble events to MainWindow_ViewModel
    public delegate void CreateRecordEventHandler(object sender, EventArgs e);
    public delegate void UpdateRecordEventHandler(object sender, EventArgs e);

    //Events to bubble to MainWindow_ViewModel
    public event CreateRecordEventHandler? CreateRecord; //prob. rename...
    public event UpdateRecordEventHandler? UpdateRecord;

    //pass selectedGroup as well? RoutedEventArgs for slectedGroup and Record objects?
    public AddEditRecord_ViewModel(ViewModelBase parentVM, Models.Record? selectedRecord = null) : base(parentVM) {// ViewModelBase parentVM) : base(parentVM) {
        //do something
        SetDelegateCommands();
        
    }

    private void SetDelegateCommands() {
        CreateRecordCommand = new Utils.DelegateCommand(OnCreateRecord);
    }

    //DelegateCommand event handlers
    private void OnCreateRecord(object obj) {
        //will need to create event args and bubble to Main Window?? -- will need to bubble regardless to change view
        CreateRecord?.Invoke(this, EventArgs.Empty);
    }
    private void OnUpdateRecord(object obj) {
        UpdateRecord?.Invoke(this, EventArgs.Empty);
    }
}