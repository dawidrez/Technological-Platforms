using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy Create.xaml
    /// </summary>
    public partial class Create : Window
    {
        private string path;
        private bool created;

        public Create(string path)
        {
            InitializeComponent();
            this.path = path;
            created = false;
        }

        private void OKButton(object send,RoutedEventArgs e)
        {
            bool isFile = (bool)radioFile.IsChecked;
            bool isDir = (bool)radioDir.IsChecked;
            if (isFile && !Regex.IsMatch(Name.Text, "^[a-zA-Z0-9_~-]{1,8}\\.(txt|php|html)$"))
            {
                System.Windows.MessageBox.Show("This name is not allowed!", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (!isFile && !isDir)
            {
                System.Windows.MessageBox.Show("Choose file or directory", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                FileAttributes atr = FileAttributes.Normal;
                if ((bool)checkRO.IsChecked)
                {
                    atr =atr| FileAttributes.ReadOnly;
                }
                if ((bool)checkA.IsChecked)
                {
                    atr = atr | FileAttributes.Archive;
                }
                if ((bool)checkH.IsChecked)
                {
                    atr = atr | FileAttributes.Hidden;
                }
                if ((bool)checkS.IsChecked)
                {
                    atr = atr | FileAttributes.System;
                }
                string name = Name.Text;
                path += "//" + name;
                if (isFile)
                {
                    File.Create(path);
                    File.SetAttributes(path, atr);
                }
                else
                {
                    Directory.CreateDirectory(path);
                    File.SetAttributes(path, atr);
                }
                created = true;
            }
            Close();
        }

        private void CancelButton(object send, RoutedEventArgs e)
        {
            Close();
        }
       
        public bool IfCreated()
        {
            return created;
        }

        public string GetPath()
        {
            return path;
        }

    }
}
