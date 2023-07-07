using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.ViewModels;
internal class AppSettings_ViewModel : ViewModelBase {
    private const int MAX_AUTO_BACKUP_COUNT = 999;
    private const int MIN_AUTO_BACKUP_COUNT = 1;
    private const int MAX_UNLOCK_ATTEMPTS = 999;
    private const int MIN_UNLOCK_ATTEMPTS = 1;
    private const int MAX_TIMEOUT_MINUTES = 1440;
    private const int MIN_TIMEOUT_MINUTES = 1;
    private int _autoBackupCount = 10;
    private int _unlockAttemtps = 10;
    private int _timeoutMinutes = 10;

    public int AutoBackupCount {
        get { return _autoBackupCount; }
        set {
            _autoBackupCount = value;
            OnPropertyChanged(nameof(AutoBackupCount));
        }
    }
    public int UnlockAttempts {
        get { return _unlockAttemtps; }
        set {
            _unlockAttemtps = value;
            OnPropertyChanged(nameof(UnlockAttempts));
        }
    }
    public int TimeoutMinutes {
        get { return _timeoutMinutes; }
        set {
            _timeoutMinutes = value;
            OnPropertyChanged(nameof(TimeoutMinutes));
        }
    }

    public Utils.DelegateCommand IncrementAutoBackupCountCommand { get; set; }
    public Utils.DelegateCommand DecrementAutoBackupCountCommand { get; set; }
    public Utils.DelegateCommand IncrementUnlockAttemptsCommand { get; set; }
    public Utils.DelegateCommand DecrementUnlockAttemptsCommand { get; set; }
    public Utils.DelegateCommand IncrementTimeoutMinutesCommand { get; set; }
    public Utils.DelegateCommand DecrementTimeoutMinutesCommand { get; set; }

    internal AppSettings_ViewModel(ViewModelBase parentVM) : base(parentVM) {
        IncrementAutoBackupCountCommand = new(OnIncrementAutoBackupCountCommand);
        DecrementAutoBackupCountCommand = new(OnDecrementAutoBackupCountCommand);
        IncrementUnlockAttemptsCommand = new(OnIncrementUnlockAttemtpsCommand);
        DecrementUnlockAttemptsCommand = new(OnDecrementUnlockAttemtpsCommand);
        IncrementTimeoutMinutesCommand = new(OnIncrementTimeoutMinutesCommand);
        DecrementTimeoutMinutesCommand = new(OnDecrementTimeoutMinutesCommand);
    }

    private void OnIncrementAutoBackupCountCommand(object obj) {
        if (_autoBackupCount < MAX_AUTO_BACKUP_COUNT)
            AutoBackupCount++;
    }
    private void OnDecrementAutoBackupCountCommand(object obj) {
        if (_autoBackupCount > MIN_AUTO_BACKUP_COUNT)
            AutoBackupCount--;
    }
    private void OnIncrementUnlockAttemtpsCommand(object obj) {
        if (_unlockAttemtps < MAX_UNLOCK_ATTEMPTS)
            UnlockAttempts++;
    }
    private void OnDecrementUnlockAttemtpsCommand(object obj) {
        if (_unlockAttemtps > MIN_UNLOCK_ATTEMPTS)
            UnlockAttempts--;
    }
    private void OnIncrementTimeoutMinutesCommand(object obj) {
        if (_timeoutMinutes < MAX_TIMEOUT_MINUTES)
            TimeoutMinutes++;
    }
    private void OnDecrementTimeoutMinutesCommand(object obj) {
        if (_timeoutMinutes > MIN_TIMEOUT_MINUTES)
            TimeoutMinutes--;
    }
}