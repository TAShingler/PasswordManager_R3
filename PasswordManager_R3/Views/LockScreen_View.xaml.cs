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
    #region ToggleButton Old and New Password tBox and pBox Visibility Event Handlers
    private void tBtnPasswordVisibility_Click(object sender, RoutedEventArgs e) {
        if (tBtnPasswordVisibility.IsChecked == true) {
            tBox.Visibility = Visibility.Collapsed;
            pBox.Visibility = Visibility.Visible;

            pBox.Focus();
            pBox.GetType().GetMethod("Select", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(pBox, new object[] { pBox.Password.Length, 0 });
        } else {
            tBox.Visibility = Visibility.Visible;
            pBox.Visibility = Visibility.Collapsed;

            tBox.Focus();
            tBox.Select(tBox.Text.Length, 0);
        }
    }
    private void tBtnOldPasswordVisibility_Click(object sender, RoutedEventArgs e) {
        if (tBtnOldPasswordVisibility.IsChecked == true) {
            tBoxOldPassword.Visibility = Visibility.Collapsed;
            pBoxOldPassword.Visibility = Visibility.Visible;

            pBoxOldPassword.Focus();
            pBoxOldPassword.GetType().GetMethod("Select", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(pBoxOldPassword, new object[] { pBoxOldPassword.Password.Length, 0 });
        } else {
            tBoxOldPassword.Visibility = Visibility.Visible;
            pBoxOldPassword.Visibility = Visibility.Collapsed;

            tBoxOldPassword.Focus();
            tBoxOldPassword.Select(tBoxOldPassword.Text.Length, 0); //maybe change to place caret at some position it was before the button was pressed -- maybe do both ways (e.g., tBox -> pBox and pBox -> tBox visibility)
        }
    }
    private void tBtnNewPasswordVisibility_Click(object sender, RoutedEventArgs e) {
        if (tBtnNewPasswordVisibility.IsChecked == true) {
            tBoxNewPassword.Visibility = Visibility.Collapsed;
            pBoxNewPassword.Visibility = Visibility.Visible;

            pBoxNewPassword.Focus();
            pBoxNewPassword.GetType().GetMethod("Select", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(pBoxNewPassword, new object[] { pBoxNewPassword.Password.Length, 0 });
        } else {
            tBoxNewPassword.Visibility = Visibility.Visible;
            pBoxNewPassword.Visibility = Visibility.Collapsed;

            tBoxNewPassword.Focus();
            tBoxNewPassword.Select(tBoxNewPassword.Text.Length, 0);
        }
    }
    #endregion ToggleButton Old and New Password tBox and pBox Visibility Event Handlers

    #region TextBox and PasswordBox TextChanged Event Handlers
    private void tBox_TextChanged(object sender, TextChangedEventArgs e) {
        System.Diagnostics.Debug.WriteLine("tBox_TextChanged executed...");
        if (tBox.Visibility == Visibility.Visible &&
            pBox.Visibility == Visibility.Collapsed) {

            pBox.Password = tBox.Text;
        }
    }
    private void pBox_PasswordChanged(object sender, RoutedEventArgs e) {
        System.Diagnostics.Debug.WriteLine("pBox_PasswordChanged executed...");
        if (pBox.Visibility == Visibility.Visible &&
            tBox.Visibility == Visibility.Collapsed) {

            tBox.Text = pBox.Password;
        }
    }
    private void tBoxOldPassword_TextChanged(object sender, RoutedEventArgs e) {
        if (tBoxOldPassword.Visibility == Visibility.Visible &&
            pBoxOldPassword.Visibility == Visibility.Collapsed) {

            pBoxOldPassword.Password = tBoxOldPassword.Text;
        }
    }
    private void pBoxOldPassword_PasswordChanged(object sender, RoutedEventArgs e) {
        if (pBoxOldPassword.Visibility == Visibility.Visible &&
            tBoxOldPassword.Visibility == Visibility.Collapsed) {

            tBoxOldPassword.Text = pBoxOldPassword.Password;
        }
    }
    private void tBoxNewPassword_TextChanged(object sender, RoutedEventArgs e) {
        if (tBoxNewPassword.Visibility == Visibility.Visible &&
            pBoxNewPassword.Visibility == Visibility.Collapsed) {

            pBoxNewPassword.Password = tBoxNewPassword.Text;
        }
    }
    private void pBoxNewPassword_PasswordChanged(object sender, RoutedEventArgs e) {
        if (pBoxNewPassword.Visibility == Visibility.Visible &&
            tBoxNewPassword.Visibility == Visibility.Collapsed) {

            tBoxNewPassword.Text = pBoxNewPassword.Password;
        }
    }
    #endregion TextBox and PasswordBox TextChanged Event Handlers

    private void UserControl_Unloaded(object sender, RoutedEventArgs e) {
        System.Diagnostics.Debug.WriteLine("LockScreen_View unloaded");
    }

    private void btnConfirmSetNewPassword_Click(object sender, RoutedEventArgs e) {
        //pass old & new password to ViewModel
        ((ViewModels.LockScreen_ViewModel)this.DataContext).ConfirmSetNewPassword(tBoxOldPassword.Text, tBoxNewPassword.Text);
    }
}
