using System.Windows;

namespace PasswordManager_R3.ViewModels;
internal class LockScreen_ViewModel : ViewModelBase {
    //Private Fields
    //public delegate void UnlockDatabaseDelegate(object obj);
    //public event UnlockDatabaseDelegate? DatabaseUnlocked;

    //Public Properties
    public Classes.DelegateCommand UnlockDatabaseCommand { get; set; }
    public Classes.DelegateCommand CloseWindowCommand { get; set; }

    //Constructors
    public LockScreen_ViewModel() {
        UnlockDatabaseCommand = new Classes.DelegateCommand(onUnlockDatabaseCommand);
        CloseWindowCommand = new Classes.DelegateCommand(onCloseWindowCommand);

        //if (ParentVM == null) {
        //    System.Diagnostics.Debug.WriteLine("LockScreen_ViewModel ParentVM is NULL");
        //} else {
        //    System.Diagnostics.Debug.WriteLine("LockScreen_ViewModel ParentVM is " + ParentVM.ToString());
        //}

        //if (ParentDependency == null) {
        //    System.Diagnostics.Debug.WriteLine("LockScreen_ViewModel ParentDependency is NULL");
        //} else {
        //    System.Diagnostics.Debug.WriteLine("LockScreen_ViewModel ParentDependency is " + ParentDependency.ToString());
        //}
    }

    //Event Handlers
    private void onUnlockDatabaseCommand(object obj) {
        System.Diagnostics.Debug.WriteLine("onUnlockDatabaseCommand() called; functionality currently not implemented...");
        //compare entered password with stored password
        //unlock DB if true
        //decrement attempts by 1 if false

        //bubble change view to MainWindow -- don't know how I am going to do that yet...
        //if (ParentVM != null) {
        //    if (ParentVM.GetType() == typeof(MainWindow_ViewModel)) {
        //        System.Diagnostics.Debug.WriteLine("parentVM is MainWindow_ViewModel");
        //    } else {
        //        System.Diagnostics.Debug.WriteLine("parentVM is not MainWindow_ViewModel, it is " + ParentVM == null ? "null" : ParentVM.ToString());
        //    }
        //}
        //if (DatabaseUnlocked != null) {
        //    DatabaseUnlocked(this);
        //}
    }
    private void onCloseWindowCommand(object obj) {
        Window win = (Window)obj;
        win.Close();
    }

    //Other methods


    
}
