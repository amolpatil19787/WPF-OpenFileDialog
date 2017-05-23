using FileAndFolderDialogs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FileAndFolderDialogs.Helpers
{
    public class DoubleClickBehavior
    {
        public static DependencyProperty OnDoubleClickProperty = DependencyProperty.RegisterAttached(
            "OnDoubleClick",
            typeof(ICommand),
            typeof(DoubleClickBehavior),
            new UIPropertyMetadata(DoubleClickBehavior.OnDoubleClick));

        public static void SetOnDoubleClick(DependencyObject target, ICommand value)
        {
            target.SetValue(OnDoubleClickProperty, value);
        }

        private static void OnDoubleClick(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            var element = target as Control;
            if (element == null)
            {
                throw new InvalidOperationException("This behavior can be attached to a Control item only.");
            }

            if ((e.NewValue != null) && (e.OldValue == null))
            {
                element.MouseDoubleClick += MouseDoubleClick;
            }
            else if ((e.NewValue == null) && (e.OldValue != null))
            {
                element.MouseDoubleClick -= MouseDoubleClick;
            }
        }

        private static void MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UIElement element = (UIElement)sender;
            ICommand command = (ICommand)element.GetValue(OnDoubleClickProperty);
            command.Execute(null);
        }

    }
}
