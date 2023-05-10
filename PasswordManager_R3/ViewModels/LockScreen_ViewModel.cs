using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordManager_R3.ViewModels;
internal class LockScreen_ViewModel : INotifyPropertyChanged {
    //Private Fields
    

    //Public Properties
    public Classes.DelegateCommand UnlockDatabaseCommand { get; set; }
    public Classes.DelegateCommand CloseWindowCommand { get; set; }

    //Constructors
    public LockScreen_ViewModel() {
        UnlockDatabaseCommand = new Classes.DelegateCommand(onUnlockDatabaseCommand);
        CloseWindowCommand = new Classes.DelegateCommand(onCloseWindowCommand);
    }

    //Event Handlers
    private void onUnlockDatabaseCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onUnlockDatabaseCommand() called; functionality currently not implemented...");
        //compare entered password with stored password
            //unlock DB if true
            //decrement attempts by 1 if false

        //bubble change view to MainWindow -- don't know how I am going to do that yet...
    }
    private void onCloseWindowCommand(object obj) {
        Window win = (Window)obj;
        win.Close();
    }

    //Other methods


    // INotifyPropertyChanged implementation
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName) {
        if (PropertyChanged != null) {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
