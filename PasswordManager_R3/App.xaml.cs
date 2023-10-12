using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordManager_R3;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application, System.ComponentModel.INotifyPropertyChanged {
    public event PropertyChangedEventHandler PropertyChanged;
    //private long millisecondsWithoutFocus;
    //public System.Timers.Timer timer { get; set; } = new System.Timers.Timer(100);
    private static System.Threading.Mutex _mutex = null;

    private System.Threading.Thread FileIOThread = new(new System.Threading.ThreadStart(void () => { }));

    protected override void OnStartup(StartupEventArgs e) {
        //Utils.FileOperations.DatabaseBackup();
        const string appName = "PasswordManager_R3";
        bool createdNew;

        _mutex = new System.Threading.Mutex(true, appName, out createdNew);

        if (!createdNew) {
            //app is already running! Exiting the application
            Application.Current.Shutdown();
        }

        //var files = System.IO.Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

        //foreach (var file in files) {
        //    System.Diagnostics.Debug.WriteLine($"file = {file}");
        //}

        //System.Diagnostics.Debug.WriteLine("DatabaseBackupsPath = " + AppVariables.DatabaseBackupsPath);
        base.OnStartup(e);
    }

    //private void Application_Activated(object sender, EventArgs e) => timer.Elapsed += (s, e) => MillisecondsWithoutFocus++;
    //private void Application_Deactivated(object sender, EventArgs e) => timer.Stop();
    //private void Application_Startup(object sender, StartupEventArgs e) => timer.Start();

    //public long MillisecondsWithoutFocus {
    //    get => millisecondsWithoutFocus;
    //    set {
    //        millisecondsWithoutFocus = value;
    //        PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(MillisecondsWithoutFocus)));
    //    }
    //}
}


