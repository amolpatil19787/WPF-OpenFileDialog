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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileAndFolderDialogs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //use ! symbol to seperate different extensions
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new ATFileDialog
                        (
                            "File Dialog Title",
                            string.Format("Rio files (*.rio;)|*.rio!{0}|*.*", Application.Current.Resources["AllFiles"]),
                            true,
                            "Z:\\"
                        );
            ofd.ShowDialog();
        }
    }
}
