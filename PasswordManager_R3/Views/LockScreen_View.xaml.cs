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

namespace PasswordManager_R3.Views;
/// <summary>
/// Interaction logic for LockScreen_View.xaml
/// </summary>
public partial class LockScreen_View : UserControl {
    public LockScreen_View() {
        InitializeComponent();
    }

    //handles click event for the toggle button that toggles the visibility states of the password box and collapsed text box
    private void tBtnPasswordVisibility_Click(object sender, RoutedEventArgs e) {
        if (tBtnPasswordVisibility.IsChecked == true) {
            tBox.Visibility = Visibility.Collapsed;
            pBox.Visibility = Visibility.Visible;
        } else {
            tBox.Visibility = Visibility.Visible;
            pBox.Visibility = Visibility.Collapsed;
        }
    }

    private void tBox_TextChanged(object sender, TextChangedEventArgs e) {
        if (tBox.Visibility == Visibility.Visible &&
            pBox.Visibility == Visibility.Collapsed) {

            pBox.Password = tBox.Text;
        }
    }

    private void pBox_PasswordChanged(object sender, RoutedEventArgs e) {
        if (pBox.Visibility == Visibility.Visible &&
            tBox.Visibility == Visibility.Collapsed) {

            tBox.Text = pBox.Password;
        }
    }

    private void UserControl_Unloaded(object sender, RoutedEventArgs e) {
        System.Diagnostics.Debug.WriteLine("LockScreen_View unloaded");
    }
}
