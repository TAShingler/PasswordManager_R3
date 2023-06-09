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
            //selectedRecordInfoPaneGridRow.MinHeight = 220;
            //selectedRecordInfoPaneGridRow.Height = new GridLength(0.75, GridUnitType.Star);
        } else {
            selectedRecordInfoPane.Visibility = Visibility.Collapsed;
            buttonSelectedRecordInfoPaneExpander.Visibility = Visibility.Visible;
            //selectedRecordInfoPaneGridRow.MinHeight = 24;
            //selectedRecordInfoPaneGridRow.Height = GridLength.Auto;
        }
    }

    private void TreeViewItem_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e) {
        e.Handled = true;
        //var item = (TreeViewItem)sender;
        //if (item != null) {
        //    // move horizontal scrollbar only when event reached last parent item
        //    if (item.Parent == null) {
        //        var scrollViewer = treeViewGroups.Template.FindName("_tv_scrollviewer_", treeViewGroups) as ScrollViewer;
        //        if (scrollViewer != null)
        //            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, (Action)(() => scrollViewer.ScrollToLeftEnd()));
        //    }
        //}
    }

    //private void TreeViewItem_MouseDoubleClick(object sender, MouseEventArgs e) {
    //    if (sender is TreeViewItem) {
    //        ((TreeViewItem)sender).IsExpanded = !((TreeViewItem)sender).IsExpanded;
    //    }
    //}

    //private void TreeViewItem_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e) {
    //    //System.Diagnostics.Debug.WriteLine($"TreeViewItem_PreviewMouseDoubleClick sender: {sender.GetType()}");
    //    if (sender is TreeViewItem) {
    //        ((TreeViewItem)sender).IsExpanded = !((TreeViewItem)sender).IsExpanded;
    //    }
    //}

    //private void TreeView_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e) {
    //    System.Diagnostics.Debug.WriteLine($"TreeView_PreviewMouseDoubleClick sender: {sender.GetType()}");
    //}
}
