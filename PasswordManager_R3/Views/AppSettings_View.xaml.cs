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
/// Interaction logic for AppSettings_View.xaml
/// </summary>
public partial class AppSettings_View : UserControl {
    public AppSettings_View() {
        InitializeComponent();
    }

    private void listViewTabDecrementButton_Click(object sender, RoutedEventArgs e) {
        if (listViewTabSelector.SelectedIndex > 0)
            listViewTabSelector.SelectedIndex--;
    }

    private void listViewTabIncrementButton_Click(object sender, RoutedEventArgs e) {
        if (listViewTabSelector.SelectedIndex < listViewTabSelector.Items.Count)
            listViewTabSelector.SelectedIndex++;
    }

    private void NumericUpDownTextBox_PreviewKeyDown(object sender, KeyEventArgs e) {
        if (e.Key == Key.Tab || e.Key == Key.Left || e.Key == Key.Right) {
            e.Handled = false;
            return;
        }

        System.Text.RegularExpressions.Regex regex = new("^D(\\d)$");
        e.Handled = !regex.IsMatch(e.Key.ToString());
    }

    private void RadioButton_Click(object sender, RoutedEventArgs e) {
        System.Diagnostics.Debug.WriteLine("sender is checked = " + ((RadioButton)sender).IsChecked);
    }
}
