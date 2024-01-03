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

    private Models.AppVariables _appVariables;
    private readonly Utils.DatabaseOperations? _databaseOps = new();

    #region PROPERTIES
    internal Models.AppVariables AppVariables {
        get => _appVariables;
    }
    internal Utils.DatabaseOperations? DatabaseOps {
        get => _databaseOps;
    }
    #endregion PROPERTIES

    protected override void OnStartup(StartupEventArgs e) {
        //Utils.FileOperations.DatabaseBackup();
        const string appName = "PasswordManager_R3";
        bool createdNew;

        _mutex = new System.Threading.Mutex(true, appName, out createdNew);

        if (!createdNew) {
            //app is already running! Exiting the application
            Application.Current.Shutdown();
        }


        /*
        //verify that app data directory exist...
        if (Utils.FileOperations.DoesDirectoryExist(Utils.FileOperations.AppSettingsDirectory) == false) {
            //create directory
            System.IO.Directory.CreateDirectory(Utils.FileOperations.AppSettingsDirectory);
        }

        //verify that app variables file exists
        if (Utils.FileOperations.DoesFileExist(Utils.FileOperations.AppSettingsDirectory + @"\" + Utils.FileOperations.AppSettingsFileName) == false) {
            //deserialize file and pass data to Models.AppVariables class
            _appVariables = new Models.AppVariables();
        } else {
            var fromFile = Utils.FileOperations.ReadAppVariablesFromFile();// Utils.FileOperations.AppSettingsDirectory + @"\" + Utils.FileOperations.AppSettingsFileName);
            System.Diagnostics.Debug.WriteLine("fromFile length: " + fromFile.Length);
        //}
        */

        //set AppVariables - retrieve from file; create new if file does not exist
        try {
            var appSettingsString = Utils.FileOperations.ReadAppVariablesFromFile();

            //deserialize
            var deserializedAppSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.AppVariables>(appSettingsString);

            _appVariables = deserializedAppSettings;
        } catch (Exception ex) {
            MessageBox.Show("There was an error retrieving saved application settings.\nNew file has been created.","Load App Settings Error",MessageBoxButton.OK,MessageBoxImage.Error);

            //set new AppVariables instance for _appVariables
            _appVariables = new();

            //create new AppSettings file
            Utils.FileOperations.WriteAppVariablesToFile();
        }

        //check for backup database
        if (Utils.FileOperations.DoesDirectoryExist(AppVariables.BackupLocation) == false) {
            Utils.FileOperations.CreateDirectory(AppVariables.BackupLocation);
        }

        //enter app
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

    //internal void ClearValues() {
    //    _databaseOperations = null;
    //}
}