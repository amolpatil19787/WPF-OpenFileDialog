using FileAndFolderDialogs.Models;
using FileAndFolderDialogs.ViewModels;
using MahApps.Metro.Controls;
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
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace FileAndFolderDialogs
{
    /// <summary>
    /// Interaction logic for ATFileDialog.xaml
    /// </summary>
    public partial class ATFileDialog : MetroWindow
    {
        public ATFileDialog()
        {
            InitializeComponent();
            this.DataContext = VMATFileDialogModel.Instance;
        }

        public ATFileDialog(string Title, string Filters, bool IsMultiSelect, string AvoidedPaths)
        {
            InitializeComponent();
            VMATFileDialogModel.Instance.FilterList = Filters.Split('!').ToList();
            VMATFileDialogModel.Instance.SelectedFilter = VMATFileDialogModel.Instance.FilterList.FirstOrDefault();
            VMATFileDialogModel.Instance.AvoidedFilePaths = AvoidedPaths;
            VMATFileDialogModel.Instance.loadTreeData();
            this.DataContext = VMATFileDialogModel.Instance;
        }

        private void dg_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (VMATFileDialogModel.Instance.SelectedFileItem.IsDirectory)
            {
                getSelectedTreeViewItem(treeview.Items);
            }
        }

        public void getSelectedTreeViewItem(ItemCollection items)
        {
            foreach(FileFolderInfo item in items)
            {
                    TreeViewItem maintreeitem = treeview.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                if (maintreeitem != null)
                {
                    if (item == VMATFileDialogModel.Instance.SelectedFileItem)
                    {
                        maintreeitem.IsSelected = true;
                        break;
                    }
                    else
                    {
                        maintreeitem.IsExpanded = true;
                        maintreeitem.IsSelected = true;
                        if (maintreeitem.HasItems)
                            getSelectedTreeViewItem(maintreeitem.Items);
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Content.ToString().ToUpper() == "OPEN")
            {
                VMATFileDialogModel.Instance.IsOk = true;
                this.Close();
            }
            else
            {
                VMATFileDialogModel.Instance.IsOk = false;
                this.Close();
            }
        }

        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)) 
            {
                if (dg.SelectedItems.Count > 0)
                {
                    VMATFileDialogModel.Instance.TempSelectedFileItemList = new ObservableCollection<Models.FileFolderInfo>();
                    foreach (FileFolderInfo file in dg.SelectedItems)
                        VMATFileDialogModel.Instance.TempSelectedFileItemList.Add(file);
                }
                
            }
            else
            {
                VMATFileDialogModel.Instance.TempSelectedFileItemList = new ObservableCollection<Models.FileFolderInfo>();
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = treeview.ItemContainerGenerator.ContainerFromItem(VMATFileDialogModel.Instance.Drives.FirstOrDefault()) as TreeViewItem;
            if (item != null)
                item.IsSelected = true;
        }
    }
}

public static class VisualTreeExt
{
    public static IEnumerable<T> GetDescendants<T>(DependencyObject parent) where T : DependencyObject
    {
        var count = VisualTreeHelper.GetChildrenCount(parent);
        for (var i = 0; i < count; ++i)
        {
            // Obtain the child
            var child = VisualTreeHelper.GetChild(parent, i);
            if (child is T)
                yield return (T)child;

            // Return all the descendant children
            foreach (var subItem in GetDescendants<T>(child))
                yield return subItem;
        }
    }
}
