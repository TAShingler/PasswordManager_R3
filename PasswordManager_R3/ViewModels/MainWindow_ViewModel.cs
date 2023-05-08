using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordManager_R3.ViewModels;
internal class MainWindow_ViewModel : System.ComponentModel.INotifyPropertyChanged {
    private WindowState _winState = WindowState.Normal;
    private string _testText = "Initial string";

    public WindowState WinState { 
        get { return _winState; }
        set {
            _winState = value;
            OnPropertyChanged("WinState");
        }
    }
    public string TestText {
        get { return _testText; }
        set {
            _testText = value;
            OnPropertyChanged("TestText");
        }
    }
    public Classes.DelegateCommand TestDelegateCommand { get; set; }

    public MainWindow_ViewModel() {
        //WinState = WindowState.Normal;
        TestDelegateCommand = new Classes.DelegateCommand(onTestDelegateCommand);
    }

    public void onTestDelegateCommand(object obj) {
        if (WinState == WindowState.Normal) {
            WinState = WindowState.Maximized;
        } else {
            WinState = WindowState.Normal;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName) {
        if (PropertyChanged != null) {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    //protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null) { }  -- could be useful in future
    //protected Action<PropertyChangedEventArgs> RaisePropertyChanged() {
    //    return args => PropertyChanged?.Invoke(this, args);
    //}
}
