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

            rowRightColBottomRow.MinHeight = selectedRecordInfoPane.MinHeight;
            rowRightColBottomRow.Height = new GridLength(1, GridUnitType.Star);
            //selectedRecordInfoPaneGridRow.MinHeight = 220;
            //selectedRecordInfoPaneGridRow.Height = new GridLength(0.75, GridUnitType.Star);
        } else {
            selectedRecordInfoPane.Visibility = Visibility.Collapsed;
            buttonSelectedRecordInfoPaneExpander.Visibility = Visibility.Visible;
            //selectedRecordInfoPaneGridRow.MinHeight = 24;
            //selectedRecordInfoPaneGridRow.Height = GridLength.Auto;
            rowRightColBottomRow.MinHeight = 0;
            rowRightColBottomRow.Height = GridLength.Auto;
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

    private void buttonTreeViewDisplayContextMenu_Click(object sender, RoutedEventArgs e) {
        System.Diagnostics.Debug.WriteLine("sender type: " + sender.GetType());
        System.Diagnostics.Debug.WriteLine("sender parent: " + ((Button)sender).Parent);
        System.Diagnostics.Debug.WriteLine("sender templated parent: " + ((Button)sender).TemplatedParent);

        if (sender == null) { System.Diagnostics.Debug.WriteLine("parameter \'object\' is null"); return; }

        if (sender.GetType() == typeof(Button)) {
            Button btnSender = (Button)sender;

            if (btnSender.TemplatedParent is TreeViewItem) {
                TreeViewItem tempParent = (TreeViewItem)btnSender.TemplatedParent;

                if (tempParent.IsSelected == false) {
                    tempParent.IsSelected = true;
                }

                System.Diagnostics.Debug.WriteLine("tempParent header group name: " + ((Models.Group)tempParent.Header).Title);

                var cm = this.FindResource("TreeViewItemContextMenu");

                if (cm != null) {
                    ((ContextMenu)cm).PlacementTarget = btnSender;
                    ((ContextMenu)cm).Placement = System.Windows.Controls.Primitives.PlacementMode.Right;
                    ((ContextMenu)cm).HorizontalOffset = 8;

                    if (((Models.Group)tempParent.Header).ParentGroup == null) {
                        ((MenuItem)((ContextMenu)cm).Items[2]).IsEnabled = false;
                    } else {
                        ((MenuItem)((ContextMenu)cm).Items[2]).IsEnabled = true;
                    }

                    ((ContextMenu)cm).IsOpen = true;
                }

                e.Handled = true;
            }
        }
    }
    private void TreeViewItem_MouseRightButtonUp(object sender, MouseButtonEventArgs e) {
        System.Diagnostics.Debug.WriteLine("TreeViewItem_MouseRightButtonUp event handler:");
        System.Diagnostics.Debug.WriteLine("    - sender type: " + sender.GetType());
        System.Diagnostics.Debug.WriteLine("    - source: " + e.Source);
        System.Diagnostics.Debug.WriteLine("    - original source: " + e.OriginalSource);
        ((TreeViewItem)sender).IsSelected = true;
        //TreeViewItemContextMenu.PlacementTarget = (TreeViewItem)sender;
        //TreeViewItemContextMenu.Placement = PlacementMode.Mouse;
        //TreeViewItemContextMenu.IsOpen = true;

        var cm = this.FindResource("TreeViewItemContextMenu");
        if (cm != null) {
            //Debug.WriteLine("cm is not null");
            ((ContextMenu)cm).PlacementTarget = (TreeViewItem)sender;
            ((ContextMenu)cm).Placement = System.Windows.Controls.Primitives.PlacementMode.Mouse;

            if (((Models.Group)treeViewGroups.SelectedItem).ParentGroup == null) {
                ((MenuItem)((ContextMenu)cm).Items[2]).IsEnabled = false;
            } else if (((Models.Group)treeViewGroups.SelectedItem).ParentGroup != null) {
                ((MenuItem)((ContextMenu)cm).Items[2]).IsEnabled = true;
            }

            ((ContextMenu)cm).IsOpen = true;
        }

        e.Handled = true;
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
