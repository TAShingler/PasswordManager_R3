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
        if (textBoxPassword.Visibility == Visibility.Visible &&
            pWrdBox.Visibility == Visibility.Collapsed) {

            pWrdBox.Password = textBoxPassword.Text;
        }
    }

    private void pWrdBox_PasswordChanged(object sender, RoutedEventArgs e) {
        if (pWrdBox.Visibility == Visibility.Visible &&
            textBoxPassword.Visibility == Visibility.Collapsed) {

            textBoxPassword.Text = pWrdBox.Password;
        }

        pWrdBox.Focus();
        pWrdBox.GetType().GetMethod("Select", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(pWrdBox, new object[] { pWrdBox.Password.Length, 0 });
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
}
