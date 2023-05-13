using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordManager_R3.ViewModels;
internal class ViewModelBase : INotifyPropertyChanged {
    private ViewModelBase? _parentVM;
    public ViewModelBase? ParentVM {
        get { return _parentVM; }
        set {
            if (_parentVM != value) {
                _parentVM = value;
                OnPropertyChanged(nameof(ParentVM));
            }
        }
    }

    public ViewModelBase() {
        _parentVM = null;
    }

    public ViewModelBase(ViewModelBase? parentVM) {
        ParentVM = parentVM;
    }



    //private DependencyProperty? _parentDependency;
    //public DependencyProperty? ParentDependency { 
    //    get { return _parentDependency; }
    //    set {
    //        if ( _parentDependency != value) {
    //            _parentDependency = value;
    //            OnPropertyChanged(nameof(ParentDependency));
    //        }
    //    }
    //}

    // INotifyPropertyChanged implementation
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName) {
        if (PropertyChanged != null) {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
