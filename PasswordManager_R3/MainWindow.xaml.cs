using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordManager_R3;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    System.Windows.Threading.DispatcherTimer dispatcherTimer = new();
    System.Timers.Timer timer = new System.Timers.Timer() {
        Interval = 1000
    };
    private int SecondsSinceLastAction { get; set; } = 0;
    //public int SecondsSinceLastAction {
    //    get => secondsSinceLastAction;
    //    set {
    //        secondsSinceLastAction = value;
    //        OnPropertyChanged(nameof(SecondsSinceLastAction));
    //    }
    //}
    public MainWindow() {
        dispatcherTimer.Tick += dispatcherTimer_Tick;
        dispatcherTimer.Interval = new(0, 0, 1);
        InitializeComponent();

        timer.Elapsed += Timer_Elapsed;
        //if (Application.Current is App app) {
        //    System.Diagnostics.Debug.WriteLine("Application.Current is App");
        //    app.PropertyChanged += (s, e) => {
        //        if (e.PropertyName == nameof(App.MillisecondsWithoutFocus)) {
        //            Dispatcher.BeginInvoke(new Action(() => {
        //                secondsSinceLastAction = app.MillisecondsWithoutFocus;   //in milliseconds
        //                System.Diagnostics.Debug.WriteLine("secondsSinceLastAction = " + secondsSinceLastAction);

        //                if (secondsSinceLastAction >= (AppVariables.TimeoutMinutes * 60000)) {
        //                    ((ViewModels.MainWindow_ViewModel)this.DataContext).OnLockDatabaseCommand(new());
        //                    System.Diagnostics.Debug.WriteLine("Database locked...");
        //                }
        //            }));
        //        }
        //    };
        //}
    }

    private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e) {
        SecondsSinceLastAction++;
    }

    private void toggleButtonAppMenu_Click(object sender, RoutedEventArgs e) {
        System.Diagnostics.Debug.WriteLine("toggleButtonAppMenu_Click called");
        //System.Diagnostics.Debug.WriteLine("toggleButtonAppMenu isChecked = " + toggleButtonAppMenu.IsChecked);
    }

    private void toggleButtonAppMenu_MouseEnter(object sender, MouseEventArgs e) {
        //toggleButtonAppMenu.IsChecked = true;
    }

    private void toggleButtonAppMenu_MouseLeave(object sender, MouseEventArgs e) {
        //toggleButtonAppMenu.IsChecked = false;
    }

    private void WinMain_Loaded(object sender, RoutedEventArgs e) {
        dispatcherTimer.Start();
    }

    private void dispatcherTimer_Tick(object sender, EventArgs e) {
        //if (((ViewModels.MainWindow_ViewModel)this.DataContext).SelectedViewModel is ViewModels.LockScreen_ViewModel)
        //    return;

        //secondsSinceLastAction += dispatcherTimer.Interval.Seconds;
        //elapsedSecondsTimer.Text = secondsSinceLastAction.ToString();

        ////System.Diagnostics.Debug.WriteLine(secondsSinceLastAction);

        //if (secondsSinceLastAction >= (AppVariables.TimeoutMinutes * 60)) {
        //    ((ViewModels.MainWindow_ViewModel)this.DataContext).OnLockDatabaseCommand(new());
        //    secondsSinceLastAction = 0;
        //    System.Diagnostics.Debug.WriteLine("Database locked...");
        //}
    }

    private void WinMain_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
        //dispatcherTimer.Stop();
        //secondsSinceLastAction = 0;
        //dispatcherTimer.Start();

        //timer.Stop();
        SecondsSinceLastAction = 0;
        timer.Start();
    }

    private void WinMain_Deactivated(object sender, EventArgs e) {
        //dispatcherTimer.Stop();
        //secondsSinceLastAction = 0;
        //timer.Start();
    }

    private void WinMain_Activated(object sender, EventArgs e) {
        //dispatcherTimer.Start();
        SecondsSinceLastAction = 0;
        timer.Start();
    }

    //private void MenuItem_MouseEnter(object sender, MouseEventArgs e) {
    //    menuItemMain.IsSubmenuOpen = true;
    //}

    //private void MenuItem_MouseLeave(object sender, MouseEventArgs e) {
    //    menuItemMain.IsSubmenuOpen = false;
    //    menuItemMain.
    //}
}
