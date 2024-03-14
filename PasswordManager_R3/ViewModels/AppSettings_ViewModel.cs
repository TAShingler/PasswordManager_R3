using Microsoft.Win32;
using PasswordManager_R3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PasswordManager_R3.ViewModels;
internal class AppSettings_ViewModel : ViewModelBase {
    #region Fields
    private const int MAX_AUTO_BACKUP_COUNT = 999;
    private const int MIN_AUTO_BACKUP_COUNT = 1;
    private const int MAX_UNLOCK_ATTEMPTS = 999;
    private const int MIN_UNLOCK_ATTEMPTS = 1;
    private const int MAX_TIMEOUT_MINUTES = 1440;
    private const int MIN_TIMEOUT_MINUTES = 1;
    private int _autoBackupCount = ((App)App.Current).AppVariables.AutoBackupCount;
    private int _unlockAttempts = ((App)App.Current).AppVariables.UnlockAttempts;
    private int _timeoutMinutes = ((App)App.Current).AppVariables.TimeoutMinutes;
    private Enums.TreeDisplayType _treeDisplayType = ((App)App.Current).AppVariables.TreeDisplayType;
    private Enums.TreeExpandCollapseButtonStyle _treeExpandCollapseButtonStyle = ((App)App.Current).AppVariables.TreeExpandCollapseButtonStyle;
    private Enums.QuickAccessIconSize _quickAccessIconSize = ((App)App.Current).AppVariables.QuickAccessIconSize;
    private bool _allowAutoBackups = ((App)App.Current).AppVariables.AllowAutoBackups;
    private bool _expandAllRadioButtonIsChecked = ((App)App.Current).AppVariables.TreeDisplayType.Equals(Enums.TreeDisplayType.ExpandAll);
    private bool _collapseAllRadioButtonIsChecked= ((App)App.Current).AppVariables.TreeDisplayType.Equals(Enums.TreeDisplayType.CollapseAll);
    private bool _rememberLastRadioButtonIsChecked = ((App)App.Current).AppVariables.TreeDisplayType.Equals(Enums.TreeDisplayType.RememberLast);
    private bool _plusMinusSignsRadioButtonIsChecked = ((App)App.Current).AppVariables.TreeExpandCollapseButtonStyle.Equals(Enums.TreeExpandCollapseButtonStyle.PlusMinusSigns);
    private bool _arrowsRadioButtonIsChecked = ((App)App.Current).AppVariables.TreeExpandCollapseButtonStyle.Equals(Enums.TreeExpandCollapseButtonStyle.Arrows);
    private bool _foldersRadioButtonIsChecked = ((App)App.Current).AppVariables.TreeExpandCollapseButtonStyle.Equals(Enums.TreeExpandCollapseButtonStyle.Folders);
    private bool _quickAccessSmallRadioButtonIsChecked = ((App)App.Current).AppVariables.QuickAccessIconSize.Equals(Enums.QuickAccessIconSize.Small);
    private bool _quickAccessMediumRadioButtonIsChecked = ((App)App.Current).AppVariables.QuickAccessIconSize.Equals(Enums.QuickAccessIconSize.Medium);
    private bool _quickAccessLargeRadioButtonIsChecked = ((App)App.Current).AppVariables.QuickAccessIconSize.Equals(Enums.QuickAccessIconSize.Large);
    private bool _eraseDatabaseAfterSetAmountAttempts = ((App)App.Current).AppVariables.EraseDatabaseAfterSetAmountAttempts;
    private bool _logDeletedItems = ((App)App.Current).AppVariables.LogDeletedItems;
    private string _backupLocation = ((App)App.Current).AppVariables.BackupLocation;
    private bool _displayInfoPane = ((App)App.Current).AppVariables.DisplayInfoPane;
    private bool _displayGroupsTree = ((App)App.Current).AppVariables.DisplayGroupsTree;
    private bool _areDatabaseUsernamesMasked = ((App)App.Current).AppVariables.AreDatabaseUsernamesMasked;
    private bool _areDatabaseEmailsMasked = ((App)App.Current).AppVariables.AreDatabaseEmailsMasked;
    private bool _areDatabasePasswordsMasked = ((App)App.Current).AppVariables.AreDatabasePasswordsMasked;
    private bool _areDatabaseUrlsMasked = ((App)App.Current).AppVariables.AreDatabaseUrlsMasked;
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
        get { return _unlockAttempts.ToString(); }
        set {
            try {
                var valueParsed = int.Parse(value);

                switch (valueParsed) {
                    case < MIN_UNLOCK_ATTEMPTS:
                        _unlockAttempts = MIN_UNLOCK_ATTEMPTS;
                        break;
                    case > MAX_UNLOCK_ATTEMPTS:
                        _unlockAttempts = MAX_UNLOCK_ATTEMPTS;
                        break;
                    default:
                        _unlockAttempts = valueParsed;
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
    public bool AllowAutoBackups {
        get { return _allowAutoBackups; }
        set {
            _allowAutoBackups = value;
            OnPropertyChanged(nameof(AllowAutoBackups));
        }
    }
    public bool ExpandAllRadioButtonIsChecked {
        get { return _expandAllRadioButtonIsChecked; }
        set {
            _expandAllRadioButtonIsChecked = value;
            OnPropertyChanged(nameof(ExpandAllRadioButtonIsChecked));
        }
    }
    public bool CollapseAllRadioButtonIsChecked {
        get { return _collapseAllRadioButtonIsChecked; }
        set {
            _collapseAllRadioButtonIsChecked = value;
            OnPropertyChanged(nameof(CollapseAllRadioButtonIsChecked));
        }
    }
    public bool RememberLastRadioButtonIsChecked {
        get { return _rememberLastRadioButtonIsChecked; }
        set {
            _rememberLastRadioButtonIsChecked = value;
            OnPropertyChanged(nameof(RememberLastRadioButtonIsChecked));
        }
    }
    public bool PlusMinusSignsRadioButtonIsChecked {
        get { return _plusMinusSignsRadioButtonIsChecked; }
        set {
            _plusMinusSignsRadioButtonIsChecked = value;
            OnPropertyChanged(nameof(PlusMinusSignsRadioButtonIsChecked));
        }
    }
    public bool ArrowsRadioButtonIsChecked {
        get { return _arrowsRadioButtonIsChecked; }
        set {
            _arrowsRadioButtonIsChecked = value;
            OnPropertyChanged(nameof(ArrowsRadioButtonIsChecked));
        }
    }
    public bool FoldersRadioButtonIsChecked {
        get { return _foldersRadioButtonIsChecked; }
        set {
            _foldersRadioButtonIsChecked = value;
            OnPropertyChanged(nameof(FoldersRadioButtonIsChecked));
        }
    }
    public bool QuickAccessSmallRadioButtonIsChecked {
        get { return _quickAccessSmallRadioButtonIsChecked; }
        set {
            _quickAccessSmallRadioButtonIsChecked = value;
            OnPropertyChanged(nameof(QuickAccessSmallRadioButtonIsChecked));
        }
    }
    public bool QuickAccessMediumRadioButtonIsChecked {
        get { return _quickAccessMediumRadioButtonIsChecked; }
        set {
            _quickAccessMediumRadioButtonIsChecked = value;
            OnPropertyChanged(nameof(QuickAccessMediumRadioButtonIsChecked));
        }
    }
    public bool QuickAccessLargeRadioButtonIsChecked {
        get { return _quickAccessLargeRadioButtonIsChecked; }
        set {
            _quickAccessLargeRadioButtonIsChecked = value;
            OnPropertyChanged(nameof(QuickAccessLargeRadioButtonIsChecked));
        }
    }
    public bool EraseDatabaseAfterSetAmountAttempts {
        get { return _eraseDatabaseAfterSetAmountAttempts; }
        set {
            _eraseDatabaseAfterSetAmountAttempts = value;
            OnPropertyChanged(nameof(EraseDatabaseAfterSetAmountAttempts));
        }
    }
    public bool LogDeletedItems {
        get { return _logDeletedItems; }
        set {
            _logDeletedItems = value;
            OnPropertyChanged(nameof(LogDeletedItems));
        }
    }
    public string BackupLocation {
        get { return _backupLocation; }
        set {
            _backupLocation = value;
            OnPropertyChanged(nameof(BackupLocation));
        }
    }
    public bool DisplayInfoPane {
        get { return _displayInfoPane; }
        set {
            _displayInfoPane = value;

            //update  property in MainWindow_ViewModel
            ((ViewModels.MainWindow_ViewModel)this.ParentVM).IsRecordDetailsPaneEnabled = value;

            OnPropertyChanged(nameof(DisplayInfoPane));
        }
    }
    public bool DisplayGroupsTree {
        get => _displayGroupsTree;
        set {
            _displayGroupsTree = value;

            //update  property in MainWindow_ViewModel
            ((ViewModels.MainWindow_ViewModel)this.ParentVM).IsGroupsTreePaneEnabled = value;

            OnPropertyChanged(nameof(DisplayGroupsTree));
        }
    }
    public bool AreDatabaseUsernamesMasked {
        get => _areDatabaseUsernamesMasked;
        set {
            _areDatabaseUsernamesMasked = value;

            //update  property in MainWindow_ViewModel
            ((ViewModels.MainWindow_ViewModel)this.ParentVM).AreDatabaseUsernamesMasked = value;

            OnPropertyChanged(nameof(AreDatabaseUsernamesMasked));
        }
    }
    public bool AreDatabaseEmailsMasked {
        get => _areDatabaseEmailsMasked;
        set {
            _areDatabaseEmailsMasked = value;

            //update  property in MainWindow_ViewModel
            ((ViewModels.MainWindow_ViewModel)this.ParentVM).AreDatabaseEmailsMasked = value;

            OnPropertyChanged(nameof(AreDatabaseEmailsMasked));
        }
    }
    public bool AreDatabasePasswordsMasked {
        get => _areDatabasePasswordsMasked;
        set {
            _areDatabasePasswordsMasked = value;

            //update  property in MainWindow_ViewModel
            ((ViewModels.MainWindow_ViewModel)this.ParentVM).AreDatabasePasswordsMasked = value;

            OnPropertyChanged(nameof(AreDatabasePasswordsMasked));
        }
    }
    public bool AreDatabaseUrlsMasked {
        get => _areDatabaseUrlsMasked;
        set {
            _areDatabaseUrlsMasked = value;

            //update  property in MainWindow_ViewModel
            ((ViewModels.MainWindow_ViewModel)this.ParentVM).AreDatabaseUrlsMasked = value;

            OnPropertyChanged(nameof(AreDatabaseUrlsMasked));
        }
    }

    public Utils.DelegateCommand IncrementAutoBackupCountCommand { get; set; }
    public Utils.DelegateCommand DecrementAutoBackupCountCommand { get; set; }
    public Utils.DelegateCommand IncrementUnlockAttemptsCommand { get; set; }
    public Utils.DelegateCommand DecrementUnlockAttemptsCommand { get; set; }
    public Utils.DelegateCommand IncrementTimeoutMinutesCommand { get; set; }
    public Utils.DelegateCommand DecrementTimeoutMinutesCommand { get; set; }
    public Utils.DelegateCommand ConfirmButtonCommand { get; set; }
    public Utils.DelegateCommand CancelButtonCommand { get; set; }
    public Utils.DelegateCommand BackupLocationButtonCommand { get; set; }

    public Utils.DelegateCommand DefaultTreeDisplayCommand { get; set; }
    public Utils.DelegateCommand ExpandCollapseButtonsCommand { get; set; }
    public Utils.DelegateCommand QuickAccessIconSizeCommand { get; set; }
    #endregion Properties

    #region Constructors
    internal AppSettings_ViewModel(ViewModelBase parentVM) : base(parentVM) {
        //set DelegateCommands
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
        BackupLocationButtonCommand = new(OnBackupLocationButtonCommand);
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
        if (_unlockAttempts >= MAX_UNLOCK_ATTEMPTS)
            return;

        var increment = _unlockAttempts += 1;
        UnlockAttempts = increment.ToString();
    }
    private void OnDecrementUnlockAttemtpsCommand(object obj) {
        if (_unlockAttempts <= MIN_UNLOCK_ATTEMPTS)
            return;

        var decrement = _unlockAttempts -= 1;
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
        //tabitem 1
        ((App)App.Current).AppVariables.AllowAutoBackups = _allowAutoBackups;
        ((App)App.Current).AppVariables.AutoBackupCount = _autoBackupCount;
        ((App)App.Current).AppVariables.BackupLocation = _backupLocation;

        //tabitem 2
        ((App)App.Current).AppVariables.EraseDatabaseAfterSetAmountAttempts = _eraseDatabaseAfterSetAmountAttempts;
        ((App)App.Current).AppVariables.UnlockAttempts = _unlockAttempts;
        ((App)App.Current).AppVariables.TimeoutMinutes = _timeoutMinutes;
        ((App)App.Current).AppVariables.LogDeletedItems = _logDeletedItems;

        //tabitem 3
        ((App)App.Current).AppVariables.TreeDisplayType = _treeDisplayType;
        ((App)App.Current).AppVariables.TreeExpandCollapseButtonStyle = _treeExpandCollapseButtonStyle;
        ((App)App.Current).AppVariables.QuickAccessIconSize = _quickAccessIconSize;
        ((App)App.Current).AppVariables.DisplayGroupsTree = _displayGroupsTree;
        ((App)App.Current).AppVariables.DisplayInfoPane = _displayInfoPane;
        ((App)App.Current).AppVariables.AreDatabaseUsernamesMasked = _areDatabaseUsernamesMasked;
        ((App)App.Current).AppVariables.AreDatabaseEmailsMasked = AreDatabaseEmailsMasked;
        ((App)App.Current).AppVariables.AreDatabasePasswordsMasked = AreDatabasePasswordsMasked;
        ((App)App.Current).AppVariables.AreDatabaseUrlsMasked = AreDatabaseUrlsMasked;

        //System.Diagnostics.Debug.WriteLine(
        //    "\n_allowAutoBackups = " + _allowAutoBackups +
        //    "\n_autoBackupCount = " + _autoBackupCount +
        //    "\n_backupLocation = " + _backupLocation +
        //    "\n_eraseDatabaseAfterSetAmountAttempts = " + _eraseDatabaseAfterSetAmountAttempts +
        //    "\n_unlockAttempts = " + _unlockAttempts +
        //    "\n_timeoutMinutes = " + _timeoutMinutes +
        //    "\n_logDeletedItems = " + _logDeletedItems +
        //    "\n_treeDisplayType = " + _treeDisplayType +
        //    "\n_treeExpandCollapseButtonStyle = " + _treeExpandCollapseButtonStyle +
        //    "\n_displayInfoPane = " + _displayInfoPane +
        //    "\n_quickAccessIconSize = " + _quickAccessIconSize +
        //    "\n");

        Utils.FileOperations.WriteAppVariablesToFile();

        ConfirmSettings?.Invoke();
    }
    private void OnCancelButtonCommand(object obj) {
        CancelSettings?.Invoke();
    }
    private void OnBackupLocationButtonCommand(object obj) {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        System.Windows.Forms.FolderBrowserDialog selectDialog = new();
        selectDialog.RootFolder = Environment.SpecialFolder.MyDocuments;
        selectDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var result = selectDialog.ShowDialog();

        //System.Diagnostics.Debug.WriteLine("DialogResult = " + result.ToString());
        //if (result == false)
        if (result.Equals(System.Windows.Forms.DialogResult.Cancel))
            return;

        //BackupLocation = saveFileDialog.FileName;
        BackupLocation = selectDialog.SelectedPath;
    }

    private void OnDefaultTreeDisplayCommand(object obj) {//object obj) {
        //System.Diagnostics.Debug.WriteLine("enum val = " + obj.ToString());

        var objAsEnum = (Enums.TreeDisplayType)obj;
        _treeDisplayType = objAsEnum;
    }
    private void OnExpandCollapsButtonsCommand(object obj) {
        //System.Diagnostics.Debug.WriteLine("enum val = " + obj.ToString());
        var objAsEnum = (Enums.TreeExpandCollapseButtonStyle)obj;
        _treeExpandCollapseButtonStyle = objAsEnum;
    }
    private void OnQuickAccessIconSizeCommand(object obj) {
        //System.Diagnostics.Debug.WriteLine("enum val = " + obj.ToString());
        var objAsEnum = (Enums.QuickAccessIconSize)obj;
        _quickAccessIconSize = objAsEnum;
    }
    #endregion Other Methods
}