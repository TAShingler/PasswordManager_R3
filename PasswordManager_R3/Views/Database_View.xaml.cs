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

            bottomRow.MinHeight = 220.0;
            bottomRow.Height = new GridLength(0.01, GridUnitType.Star);
            bottomRow.MaxHeight = rightPane.ActualHeight * 0.4;

            //selectedRecordInfoPaneGridRow.MinHeight = 220;
            //selectedRecordInfoPaneGridRow.Height = new GridLength(0.75, GridUnitType.Star);
        } else {
            selectedRecordInfoPane.Visibility = Visibility.Collapsed;
            buttonSelectedRecordInfoPaneExpander.Visibility = Visibility.Visible;
            //selectedRecordInfoPaneGridRow.MinHeight = 24;
            //selectedRecordInfoPaneGridRow.Height = GridLength.Auto;
            bottomRow.MinHeight = buttonSelectedRecordInfoPaneExpander.MinHeight;
            bottomRow.Height = new GridLength(24, GridUnitType.Pixel);
            bottomRow.MaxHeight = buttonSelectedRecordInfoPaneExpander.MaxHeight;

        }
    }

    private void TreeViewItem_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e) {
        //e.Handled = true;
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
        /*
        System.Diagnostics.Debug.WriteLine("sender type: " + sender.GetType());
        System.Diagnostics.Debug.WriteLine("sender parent: " + ((Button)sender).Parent);
        System.Diagnostics.Debug.WriteLine("sender templated parent: " + ((Button)sender).TemplatedParent);

        if (sender == null) { System.Diagnostics.Debug.WriteLine("parameter \'object\' is null"); return; }

        if (sender is not Button)
            return;

        //if (sender.GetType() == typeof(Button)) {
        Button btnSender = (Button)sender;

        if (btnSender.TemplatedParent is not TreeViewItem)
            return;

        //if (btnSender.TemplatedParent is TreeViewItem) {
        TreeViewItem tempParent = (TreeViewItem)btnSender.TemplatedParent;

        //if (tempParent.IsSelected == false) {
        //tempParent.IsSelected = true;
        //}

        System.Diagnostics.Debug.WriteLine("tempParent header group name: " + ((Models.Group)tempParent.Header).Title);

        var cm = this.FindResource("TreeViewItemContextMenu");

        if (cm is null)
            return;
        //if (cm != null) {
        ((ContextMenu)cm).PlacementTarget = btnSender;
        ((ContextMenu)cm).Placement = System.Windows.Controls.Primitives.PlacementMode.Right;
        ((ContextMenu)cm).HorizontalOffset = 8;

        //if (((Models.Group)tempParent.Header).ParentGroup == null) {
        //    ((MenuItem)((ContextMenu)cm).Items[2]).IsEnabled = false;
        //} else {
        //    ((MenuItem)((ContextMenu)cm).Items[2]).IsEnabled = true;
        //}

        ((ContextMenu)cm).IsOpen = true;
        //}

        e.Handled = true;
        //}
        //}
        */
    }
    private void TreeViewItem_MouseRightButtonUp(object sender, MouseButtonEventArgs e) {
        /* OLD
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
        */

        //right click event for expand/collapse actions?
    }

    private void UserControl_ContextMenuClosing(object sender, ContextMenuEventArgs e) {
        System.Diagnostics.Debug.WriteLine("UserControl_ContextMenuClosing() called...");
    }

    private void UserControl_ContextMenuOpening(object sender, ContextMenuEventArgs e) {
        System.Diagnostics.Debug.WriteLine("UserControl_ContextMenuOpening() called...");
    }

    private void UserControl_Unloaded(object sender, RoutedEventArgs e) {
        System.Diagnostics.Debug.WriteLine("UserControl_Unloaded() called...");

        //var cm = this.FindResource("TreeViewItemContextMenu");
        //((ContextMenu)cm).IsOpen = false;

        //e.Handled = true;
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e) {
        System.Diagnostics.Debug.WriteLine("UserControl_Loaded() called...");
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

    private void TreeViewItemContextMenuItem_Click(object sender, RoutedEventArgs e) {
        System.Diagnostics.Debug.WriteLine("TreeViewItemContextMenuItem_Click() called...");
        //var cm = this.FindResource("TreeViewItemContextMenu");

        //((ContextMenu)cm).IsOpen = false;
        //e.Handled = true;
    }

    private void ContextMenu_Unloaded(object sender, RoutedEventArgs e) {
        System.Diagnostics.Debug.WriteLine("ContextMenu_Unloaded() called...");
    }

    private void MenuItem_Click(object sender, RoutedEventArgs e) {
        System.Diagnostics.Debug.WriteLine("MenuItem_Click() called...");
        System.Diagnostics.Debug.WriteLine("sender type: " + sender.GetType());
        //var cm = this.FindResource("TreeViewItemContextMenu");
        //((ContextMenu)cm).Height = 0.0;
        //((ContextMenu)cm).Width = 0.0;
    }

    private void TreeViewItemContextMenu_Loaded(object sender, RoutedEventArgs e) {
        System.Diagnostics.Debug.WriteLine("TreeViewItemContextMenu_Loaded() called...");
        System.Diagnostics.Debug.WriteLine("sender = " + sender);
        ContextMenu cm = (ContextMenu)sender;

        //cm.DataContext = this.DataContext;
        ((MenuItem)cm.Items[0]).IsEnabled = ((ViewModels.Database_ViewModel)this.DataContext).CanCreateNewGroup;
        ((MenuItem)cm.Items[1]).IsEnabled = ((ViewModels.Database_ViewModel)this.DataContext).CanEditSelectedGroup;
        ((MenuItem)cm.Items[2]).IsEnabled = ((ViewModels.Database_ViewModel)this.DataContext).CanDeleteSelectedGroup;
    }

    private void treeViewGroups_ContextMenuClosing(object sender, ContextMenuEventArgs e) {
        System.Diagnostics.Debug.WriteLine("treeViewGroups_ContextMenuClosing() called...");
    }

    private void treeViewGroups_ContextMenuOpening(object sender, ContextMenuEventArgs e) {
        System.Diagnostics.Debug.WriteLine("treeViewGroups_ContextMenuOpening() called...");
    }

    private void CtMenuItemDeleteGroup_Click(object sender, RoutedEventArgs e) {
        System.Diagnostics.Debug.WriteLine("CtMenuDeleteSelectedGroup_Click() called...");
        TreeView? tView = FindName("treeViewGroups") as TreeView;

        System.Diagnostics.Debug.WriteLine($"tView.GetType() = {tView.GetType()}");

        System.Diagnostics.Debug.WriteLine("\n\nItems in tView.Items");
        foreach (var item in tView.Items) {
            System.Diagnostics.Debug.WriteLine($"item: {((Models.Group)item).Title}");
            foreach (var child in ((Models.Group)item).ChildrenGroups) {
                System.Diagnostics.Debug.WriteLine($"\tchild: {child.Title}");
            }
        }
        System.Diagnostics.Debug.WriteLine("\n\n");

        //tView.ItemsSource = null;

        //tView?.InvalidateArrange();
        //tView?.InvalidateMeasure();
        //tView?.InvalidateVisual();
        //tView?.InvalidateProperty(TreeView.ItemsSourceProperty);
        //tView.UpdateLayout();

        ((ViewModels.Database_ViewModel)DataContext).OnDeleteGroup(tView.SelectedItem);
    }

    private void CtMenuItemAddGroup_Click(object sender, RoutedEventArgs e) {
        System.Diagnostics.Debug.WriteLine($"CtMenuItemCreateNewGroup_Click() called...\n\tsender.GetType() = {sender.GetType()}");
        TreeView? tView = FindName("treeViewGroups") as TreeView;
        ((ViewModels.Database_ViewModel)DataContext).OnCreateGroup(tView.SelectedItem);
    }

    private void CtMenuItemEditGroup_Click(object sender, RoutedEventArgs e) {
        System.Diagnostics.Debug.WriteLine($"CtMenuItemEditGroup_Click() called...\n\tsender.GetType() = {sender.GetType()}");
        TreeView? tView = FindName("treeViewGroups") as TreeView;
        ((ViewModels.Database_ViewModel)DataContext).OnUpdateGroup(tView.SelectedItem);
    }
}
