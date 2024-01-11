using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.ViewModels;
internal class About_ViewModel : ViewModelBase {
    #region Fields
    private string _appName;
    private string _appVersion;
    private string _copyright;
    private string _appDetails;
    #endregion Fields

    #region Properties
    public string AppName {
        get => _appName;
        set {
            _appName = value;
            OnPropertyChanged(nameof(AppName));
        }
    }
    public string AppVersion {
        get => _appVersion;
        set {
            _appVersion = value;
            OnPropertyChanged(nameof(AppVersion));
        }
    }
    public string Copyright {
        get => _copyright;
        set {
            _copyright = value;
            OnPropertyChanged(nameof(Copyright));
        }
    }
    public string AppDetails {
        get => _appDetails;
        set {
            _appDetails = value;
            OnPropertyChanged(nameof(AppDetails));
        }
    }
    #endregion Properties

    #region Constructors
    #endregion Constructors

    #region Other Methods
    #endregion Other Methods
}
