using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Utils;
internal class DelegateCommand : System.Windows.Input.ICommand {
    private readonly Predicate<object> _canExecute;
    private readonly Action<object> _execute;

    public event EventHandler? CanExecuteChanged;

    public DelegateCommand(Action<object> execute) : this(execute, null) { }

    public DelegateCommand(Action<object> execute, Predicate<object> canExecute) {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter) {
        //throw new NotImplementedException();
        if (_canExecute == null) {
            return true;
        }

        return _canExecute(parameter);
    }

    public void Execute(object? parameter) {
        //throw new NotImplementedException();
        _execute(parameter);
    }

    public void RaiseCanExecuteChanged() {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
