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
/// Interaction logic for Database_View.xaml
/// </summary>
public partial class Database_View : UserControl {
    public Database_View() {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e) {
        if (selectedRecordInfoPane.Visibility == Visibility.Collapsed) {
            selectedRecordInfoPane.Visibility = Visibility.Visible;
            buttonSelectedRecordInfoPaneExpander.Visibility = Visibility.Collapsed;
            selectedRecordInfoPaneGridRow.MinHeight = 220;
            selectedRecordInfoPaneGridRow.Height = new GridLength(0.75, GridUnitType.Star);
        } else {
            selectedRecordInfoPane.Visibility = Visibility.Collapsed;
            buttonSelectedRecordInfoPaneExpander.Visibility = Visibility.Visible;
            selectedRecordInfoPaneGridRow.MinHeight = 24;
            selectedRecordInfoPaneGridRow.Height = GridLength.Auto;
        }
    }
}
