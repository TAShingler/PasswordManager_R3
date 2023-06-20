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
/// Interaction logic for AddEditRecord_View.xaml
/// </summary>
public partial class AddEditRecord_View : UserControl {
    public AddEditRecord_View() {
        InitializeComponent();
    }

    private void textBoxPassword_TextChanged(object sender, TextChangedEventArgs e) {
        //if (textBoxPassword.Visibility == Visibility.Visible &&
        //    pWrdBox.Visibility == Visibility.Collapsed) {

            //System.Diagnostics.Debug.WriteLine("textBoxPassword_TextChanged executed...");
            
            //System.Diagnostics.Debug.WriteLine($"textBoxPassword_TextChanged e.Handled before setting pWrdBox.Password = {e.Handled}");
            //if (pWrdBox.Password.Equals(textBoxPassword.Text) == false)
        pWrdBox.Password = textBoxPassword.Text;
        e.Handled = true;
            //System.Diagnostics.Debug.WriteLine($"textBoxPassword_TextChanged e.Handled after setting pWrdBox.Password = {e.Handled}");
        //}
    }

    private void pWrdBox_PasswordChanged(object sender, RoutedEventArgs e) {
        System.Diagnostics.Debug.WriteLine("pWrdBoxPassword_TextChanged executed...");

        if (pWrdBox.Visibility == Visibility.Visible &&
            textBoxPassword.Visibility == Visibility.Collapsed) {

            //System.Diagnostics.Debug.WriteLine($"textBoxPassword_TextChanged e.Handled before setting pWrdBox.Password = {e.Handled}");
            textBoxPassword.Text = pWrdBox.Password;
            e.Handled = true;
            //System.Diagnostics.Debug.WriteLine($"textBoxPassword_TextChanged e.Handled after setting pWrdBox.Password = {e.Handled}");
        }

        pWrdBox.Focus();
        pWrdBox.GetType().GetMethod("Select", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(pWrdBox, new object[] { pWrdBox.Password.Length, 0 });
        e.Handled = true;
    }

    private void toggleButtonMaskPassword_Click(object sender, RoutedEventArgs e) {
        //if (pWrdBox.Visibility == Visibility.Visible) {
        //    pWrdBox.Visibility = Visibility.Collapsed;
        //    textBoxPassword.Visibility = Visibility.Visible;
        //} else if (pWrdBox.Visibility == Visibility.Collapsed) {
        //    pWrdBox.Visibility = Visibility.Visible;
        //    textBoxPassword.Visibility = Visibility.Collapsed;
        //}

        if (toggleButtonMaskPassword.IsChecked == true) {
            textBoxPassword.Visibility = Visibility.Collapsed;
            pWrdBox.Visibility = Visibility.Visible;

            pWrdBox.Focus();
            pWrdBox.GetType().GetMethod("Select", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(pWrdBox, new object[] { pWrdBox.Password.Length, 0 });
        } else {
            textBoxPassword.Visibility = Visibility.Visible;
            pWrdBox.Visibility = Visibility.Collapsed;

            textBoxPassword.Focus();
            textBoxPassword.Select(textBoxPassword.Text.Length, 0);
        }
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e) {
        pWrdBox.Password = textBoxPassword.Text;
    }

    private void listViewTabDecrementButton_Click(object sender, RoutedEventArgs e) {
        if (listViewTabSelector.SelectedIndex > 0)
            listViewTabSelector.SelectedIndex--;
    }

    private void listViewTabIncrementButton_Click(object sender, RoutedEventArgs e) {
        if (listViewTabSelector.SelectedIndex < listViewTabSelector.Items.Count)
            listViewTabSelector.SelectedIndex++;
    }

    private void expirationDateCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e) {
        //System.Diagnostics.Debug.WriteLine("Expiration Date changed to " + expirationDateCalendar.SelectedDate);
        System.Diagnostics.Debug.WriteLine("expirationDateCalendar_SelectedDatesChanged called...");
    }

    private void expirationDateCalendar_SelectionModeChanged(object sender, EventArgs e) {
        System.Diagnostics.Debug.WriteLine("expirationDateCalendar_SelectionModeChanged called...");
    }

    private void expirationDateCalendar_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e) {
        System.Diagnostics.Debug.WriteLine("expirationDateCalendar_DisplayDateChanged called...");
    }

    private void expirationDateCalendar_SourceUpdated(object sender, DataTransferEventArgs e) {
        System.Diagnostics.Debug.WriteLine("expirationDateCalendar_SourceUpdated called...");
    }

    private void expirationDateCalendar_TargetUpdated(object sender, DataTransferEventArgs e) {
        System.Diagnostics.Debug.WriteLine("expirationDateCalendar_TargetUpdated called...");
    }

    private void expirationDateCalendar_TextInput(object sender, TextCompositionEventArgs e) {
        System.Diagnostics.Debug.WriteLine("expirationDateCalendar_TextInput called...");
    }

    private void comboBoxExpirationDatePresets_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        comboBoxExpirationDatePresets.SelectedIndex = -1;
    }
}
