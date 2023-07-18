using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordManager_R3;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application {
    private static System.Threading.Mutex _mutex = null;

    protected override void OnStartup(StartupEventArgs e) {
        const string appName = "PasswordManager_R3";
        bool createdNew;

        _mutex = new System.Threading.Mutex(true, appName, out createdNew);

        if (!createdNew) {
            //app is already running! Exiting the application
            Application.Current.Shutdown();
        }

        var files = System.IO.Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

        foreach (var file in files) {
            System.Diagnostics.Debug.WriteLine($"file = {file}");
        }

        //System.Diagnostics.Debug.WriteLine("DatabaseBackupsPath = " + AppVariables.DatabaseBackupsPath);
        base.OnStartup(e);
    }
}


