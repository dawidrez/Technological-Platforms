using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void Open(object app, RoutedEventArgs e)
        {
            var dlg = new FolderBrowserDialog()
            {
                Description = "Select directory to open"
            };
            DialogResult directory = dlg.ShowDialog();
            if (directory == System.Windows.Forms.DialogResult.OK)
            {
                treeView.Items.Clear();
                DirectoryInfo dir = new DirectoryInfo(dlg.SelectedPath);
                var root = MakeTreeDir(dir);
                treeView.Items.Add(root);
            }
        }

        private TreeViewItem MakeTreeDir(DirectoryInfo path)
        {
            var root = new TreeViewItem
            {
                Header = path.Name,
                Tag = path.FullName
            };
            root.ContextMenu = new System.Windows.Controls.ContextMenu();
            var menuCreate = new System.Windows.Controls.MenuItem { Header = "Create" };
            menuCreate.Click +=new  RoutedEventHandler(CreateClick);
            var menuDelete = new System.Windows.Controls.MenuItem { Header = "Delete" };
            menuDelete.Click += new RoutedEventHandler(DeleteClick);
            root.ContextMenu.Items.Add(menuCreate);
            root.ContextMenu.Items.Add(menuDelete);
            DirectoryInfo[] folders = path.GetDirectories();
            FileInfo[] files = path.GetFiles();
            for(int i = 0; i < folders.Length; i++)
            {
                root.Items.Add(MakeTreeDir(folders[i]));
            }
            for (int i = 0; i < files.Length; i++)
            {
                root.Items.Add(MakeTreeFile(files[i]));
            }
            root.Selected += new RoutedEventHandler(StatusUpdate);
            return root;
        }

        private void CreateClick(object dir, RoutedEventArgs e)
        {
            TreeViewItem selectedDir = (TreeViewItem)treeView.SelectedItem;
            Create createNewFile = new Create((string)selectedDir.Tag);
            createNewFile.ShowDialog();
            if (createNewFile.IfCreated())
            {
                string path = createNewFile.GetPath();
                if (File.Exists(path))
                {
                    FileInfo fi = new FileInfo(path);
                    selectedDir.Items.Add(MakeTreeFile(fi));
                }
                else
                {
                    DirectoryInfo di = new DirectoryInfo(path);
                    selectedDir.Items.Add(MakeTreeDir(di));
                }
            }

        }

        private void OpenClick(object dir, RoutedEventArgs e)
        {
            TreeViewItem selectedFile = (TreeViewItem)treeView.SelectedItem;
            string text = File.ReadAllText((string)selectedFile.Tag);
            scrollViewer.Content = new TextBlock() { Text = text };
        }

        private void DelDir(string path)
        {

            string[] folders = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);
            for (int i = 0; i < folders.Length; i++)
            {
                FileAttributes attributes = File.GetAttributes(folders[i]);
                File.SetAttributes(path, attributes & ~FileAttributes.ReadOnly);
                DelDir(folders[i]);
            }
            for (int i = 0; i < files.Length; i++)
            {
                FileAttributes attributes = File.GetAttributes(files[i]);
                File.SetAttributes(path, attributes & ~FileAttributes.ReadOnly);
                File.Delete(files[i]);
            }
            Directory.Delete(path);
        }

        private void DeleteClick(object toDelete, RoutedEventArgs e)
        {
            TreeViewItem selected=(TreeViewItem)treeView.SelectedItem;
            string path = (string)selected.Tag;
            FileAttributes attributes = File.GetAttributes(path);
            File.SetAttributes(path, attributes & ~FileAttributes.ReadOnly);
            if ((attributes & FileAttributes.Directory) == FileAttributes.Directory)
            {
                DelDir(path);
            }
            else
            {
                File.Delete(path);
            }
            if ((TreeViewItem)treeView.Items[0] == selected)
            {
                treeView.Items.Clear();
               
            }
            else
            {
                TreeViewItem parent = (TreeViewItem)selected.Parent;
                parent.Items.Remove(selected);

            }

        }
        private TreeViewItem MakeTreeFile(FileInfo path)
        {
            var root = new TreeViewItem
            {
                Header = path.Name,
                Tag = path.FullName
            };
            root.ContextMenu = new System.Windows.Controls.ContextMenu();
            var menuOpen = new System.Windows.Controls.MenuItem { Header = "Open" };
            menuOpen.Click += new RoutedEventHandler(OpenClick);
            var menuDelete = new System.Windows.Controls.MenuItem { Header = "Delete" };
            menuDelete.Click += new RoutedEventHandler(DeleteClick);
            root.ContextMenu.Items.Add(menuOpen);
            root.ContextMenu.Items.Add(menuDelete);
            root.Selected += new RoutedEventHandler(StatusUpdate);
            return root;
        }



        private void Exit(object app, RoutedEventArgs e)
        {
            Close();
        }

        private void About(object app, RoutedEventArgs e)
        {
            About about = new About();
            about.Show();
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StatusUpdate(object sender, RoutedEventArgs e)
        {
            TreeViewItem selected = (TreeViewItem)treeView.SelectedItem;
            FileAttributes attributes = File.GetAttributes((string)selected.Tag);
            status.Text = "";
            if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                status.Text += 'r';
            }
            else
            {
                status.Text += '-';
            }
            if ((attributes & FileAttributes.Archive) == FileAttributes.Archive)
            {
                status.Text += 'a';
            }
            else
            {
                status.Text += '-';
            }
            if ((attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
            {
                status.Text += 'h';
            }
            else
            {
                status.Text += '-';
            }
            if ((attributes & FileAttributes.System) == FileAttributes.System)
            {
                status.Text += 's';
            }
            else
            {
                status.Text += '-';
            }
        }
    }
}

