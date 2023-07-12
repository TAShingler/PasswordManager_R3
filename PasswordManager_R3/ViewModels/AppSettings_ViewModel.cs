using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.ViewModels;
internal class AppSettings_ViewModel : ViewModelBase {
    #region Fields
    private const int MAX_AUTO_BACKUP_COUNT = 999;
    private const int MIN_AUTO_BACKUP_COUNT = 1;
    private const int MAX_UNLOCK_ATTEMPTS = 999;
    private const int MIN_UNLOCK_ATTEMPTS = 1;
    private const int MAX_TIMEOUT_MINUTES = 1440;
    private const int MIN_TIMEOUT_MINUTES = 1;
    private int _autoBackupCount = 10;
    private int _unlockAttemtps = 10;
    private int _timeoutMinutes = 10;
    private bool _expandAllRadioButtonIsChecked = true;
    private bool _collapseAllRadioButtonIsChecked = false;
    private bool _rememberLastRadioButtonIsChecked = false;
    #endregion Fields

    #region Delegates and Events
    internal delegate void ConfirmSettingsEventHandler();
    internal delegate void CancelSettingsEventHandler();

    internal event ConfirmSettingsEventHandler ConfirmSettings;
    internal event CancelSettingsEventHandler CancelSettings;
    #endregion Delegates and Events

    #region Properties
    public string AutoBackupCount {
        get { return _autoBackupCount.ToString(); }
        set {
            try {
                var valueParsed = int.Parse(value);

                switch (valueParsed) {
                    case < MIN_AUTO_BACKUP_COUNT:
                        _autoBackupCount = MIN_AUTO_BACKUP_COUNT;
                        break;
                    case > MAX_AUTO_BACKUP_COUNT:
                        _autoBackupCount = MAX_AUTO_BACKUP_COUNT;
                        break;
                    default:
                        _autoBackupCount = valueParsed;
                        break;
                }
            } catch(Exception ex) {
                System.Diagnostics.Debug.WriteLine("AutoBackupCount set threw Exception " + ex.ToString());
                //do nothing
            }

            OnPropertyChanged(nameof(AutoBackupCount));
        }
    }
    public string UnlockAttempts {
        get { return _unlockAttemtps.ToString(); }
        set {
            try {
                var valueParsed = int.Parse(value);

                switch (valueParsed) {
                    case < MIN_UNLOCK_ATTEMPTS:
                        _unlockAttemtps = MIN_UNLOCK_ATTEMPTS;
                        break;
                    case > MAX_UNLOCK_ATTEMPTS:
                        _unlockAttemtps = MAX_UNLOCK_ATTEMPTS;
                        break;
                    default:
                        _unlockAttemtps = valueParsed;
                        break;
                }
            } catch(Exception ex) {
                //do nothing
            }

            OnPropertyChanged(nameof(UnlockAttempts));
        }
    }
    public string TimeoutMinutes {
        get { return _timeoutMinutes.ToString(); }
        set {
            try {
                var valueParsed = int.Parse(value);

                switch (valueParsed) {
                    case < MIN_TIMEOUT_MINUTES:
                        _timeoutMinutes = MIN_TIMEOUT_MINUTES;
                        break;
                    case > MAX_TIMEOUT_MINUTES:
                        _timeoutMinutes = MAX_TIMEOUT_MINUTES;
                        break;
                    default:
                        _timeoutMinutes = valueParsed;
                        break;
                }
            } catch(Exception ex) {
                //do nothing
            }

            OnPropertyChanged(nameof(TimeoutMinutes));
        }
    }
    private bool ExpandAllRadioButtonIsChecked {
        get { return _expandAllRadioButtonIsChecked; }
        set {
            _expandAllRadioButtonIsChecked = value;
            OnPropertyChanged(nameof(ExpandAllRadioButtonIsChecked));
        }
    }
    private bool CollapseAllRadioButtonIsChecked {
        get { return _collapseAllRadioButtonIsChecked; }
        set {
            _collapseAllRadioButtonIsChecked = value;
            OnPropertyChanged(nameof(CollapseAllRadioButtonIsChecked));
        }
    }
    private bool RememberLastRadioButtonIsChecked {
        get { return _rememberLastRadioButtonIsChecked; }
        set {
            _rememberLastRadioButtonIsChecked = value;
            OnPropertyChanged(nameof(RememberLastRadioButtonIsChecked));
        }
    }

    //public Enums.TreeDisplayType TreeDisplayType {
    //    get { return Models.AppVariables.TreeDisplayType; }
    //    set {
    //        Models.AppVariables.TreeDisplayType = value;
    //        OnPropertyChanged(nameof(TreeDisplayType));
    //    }
    //}
    //public Enums.TreeExpandCollapseButtonStyle TreeExpandCollapseButtonStyle {
    //    get { return Models.AppVariables.TreeExpandCollapseButtonStyle; }
    //    set {
    //        Models.AppVariables.TreeExpandCollapseButtonStyle = value;
    //        OnPropertyChanged(nameof(TreeExpandCollapseButtonStyle));
    //    }
    //}
    //public Enums.QuickAccessIconSize QuickAccessIconSize {
    //    get { return Models.AppVariables.QuickAccessIconSize; }
    //    set {
    //        Models.AppVariables.QuickAccessIconSize = value;
    //        OnPropertyChanged(nameof(QuickAccessIconSize));
    //    }
    //}

    public Utils.DelegateCommand IncrementAutoBackupCountCommand { get; set; }
    public Utils.DelegateCommand DecrementAutoBackupCountCommand { get; set; }
    public Utils.DelegateCommand IncrementUnlockAttemptsCommand { get; set; }
    public Utils.DelegateCommand DecrementUnlockAttemptsCommand { get; set; }
    public Utils.DelegateCommand IncrementTimeoutMinutesCommand { get; set; }
    public Utils.DelegateCommand DecrementTimeoutMinutesCommand { get; set; }
    public Utils.DelegateCommand ConfirmButtonCommand { get; set; }
    public Utils.DelegateCommand CancelButtonCommand { get; set; }

    public Utils.DelegateCommand DefaultTreeDisplayCommand { get; set; }
    public Utils.DelegateCommand ExpandCollapseButtonsCommand { get; set; }
    public Utils.DelegateCommand QuickAccessIconSizeCommand { get; set; }
    #endregion Properties

    #region Constructors
    internal AppSettings_ViewModel(ViewModelBase parentVM) : base(parentVM) {
        IncrementAutoBackupCountCommand = new(OnIncrementAutoBackupCountCommand);
        DecrementAutoBackupCountCommand = new(OnDecrementAutoBackupCountCommand);
        IncrementUnlockAttemptsCommand = new(OnIncrementUnlockAttemtpsCommand);
        DecrementUnlockAttemptsCommand = new(OnDecrementUnlockAttemtpsCommand);
        IncrementTimeoutMinutesCommand = new(OnIncrementTimeoutMinutesCommand);
        DecrementTimeoutMinutesCommand = new(OnDecrementTimeoutMinutesCommand);
        ConfirmButtonCommand = new(OnConfirmButtonCommand);
        CancelButtonCommand = new(OnCancelButtonCommand);
        DefaultTreeDisplayCommand = new(OnDefaultTreeDisplayCommand);
        ExpandCollapseButtonsCommand = new(OnExpandCollapsButtonsCommand);
        QuickAccessIconSizeCommand = new(OnQuickAccessIconSizeCommand);
    }
    #endregion Constructors

    #region Other Methods
    private void OnIncrementAutoBackupCountCommand(object obj) {
        if (_autoBackupCount >= MAX_AUTO_BACKUP_COUNT)
            return;

        var increment = _autoBackupCount += 1;
        AutoBackupCount = increment.ToString();
    }
    private void OnDecrementAutoBackupCountCommand(object obj) {
        if (_autoBackupCount <= MIN_AUTO_BACKUP_COUNT)
            return;
        
        var decrement = _autoBackupCount -= 1;
        AutoBackupCount = decrement.ToString();
    }
    private void OnIncrementUnlockAttemtpsCommand(object obj) {
        if (_unlockAttemtps >= MAX_UNLOCK_ATTEMPTS)
            return;

        var increment = _unlockAttemtps += 1;
        UnlockAttempts = increment.ToString();
    }
    private void OnDecrementUnlockAttemtpsCommand(object obj) {
        if (_unlockAttemtps <= MIN_UNLOCK_ATTEMPTS)
            return;

        var decrement = _unlockAttemtps -= 1;
        UnlockAttempts = decrement.ToString();
    }
    private void OnIncrementTimeoutMinutesCommand(object obj) {
        if (_timeoutMinutes >= MAX_TIMEOUT_MINUTES)
            return;

        var increment = _timeoutMinutes += 1;
        TimeoutMinutes = increment.ToString();
    }
    private void OnDecrementTimeoutMinutesCommand(object obj) {
        if (_timeoutMinutes <= MIN_TIMEOUT_MINUTES)
            return;

        var decrement = _timeoutMinutes -= 1;
        TimeoutMinutes = decrement.ToString();
    }
    private void OnConfirmButtonCommand(object obj) {
        //ConfirmSettings?.Invoke();
        System.Diagnostics.Debug.WriteLine("ExpandAllRadioButtonIsChecked = " + ExpandAllRadioButtonIsChecked);
        System.Diagnostics.Debug.WriteLine("CollapseAllRadioButtonIsChecked = " + CollapseAllRadioButtonIsChecked);
        System.Diagnostics.Debug.WriteLine("RememberLastRadioButtonIsChecked = " + RememberLastRadioButtonIsChecked);
    }
    private void OnCancelButtonCommand(object obj) {
        CancelSettings?.Invoke();
    }
    private void OnDefaultTreeDisplayCommand(object obj) {//object obj) {
        //System.Diagnostics.Debug.WriteLine("enum val = " + obj.ToString());

        var objAsEnum = (Enums.TreeDisplayType)obj;

        switch (objAsEnum) {
            case Enums.TreeDisplayType.ExpandAll:
                System.Diagnostics.Debug.WriteLine("enum val = Expand All");
                Models.AppVariables.TreeDisplayType = Enums.TreeDisplayType.ExpandAll;
                break;
            case Enums.TreeDisplayType.CollapseAll:
                System.Diagnostics.Debug.WriteLine("enum val = Collapse All");
                Models.AppVariables.TreeDisplayType = Enums.TreeDisplayType.CollapseAll;
                break;
            case Enums.TreeDisplayType.RememberLast:
                System.Diagnostics.Debug.WriteLine("enum val = Remember Last");
                Models.AppVariables.TreeDisplayType = Enums.TreeDisplayType.RememberLast;
                break;
            default:
                System.Diagnostics.Debug.WriteLine("No enum value set...");
                Models.AppVariables.TreeDisplayType = Enums.TreeDisplayType.CollapseAll;
                break;
        }
    }
    private void OnExpandCollapsButtonsCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("enum val = " + obj.ToString());
    }
    private void OnQuickAccessIconSizeCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("enum val = " + obj.ToString());
    }
    #endregion Other Methods
}