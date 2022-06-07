using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Forms;


namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    /// 
    static class Constants
    {
        public const int TASK = 0;
        public const int DELEGATE = 1;
        public const int ASYNC = 2;

    }
    public partial class MainWindow : Window
    {
        private NewtonCounter nc;
        private Compresser compresser;
        public MainWindow()
        {
            InitializeComponent();
            this.nc = new NewtonCounter();
            this.compresser = new Compresser();
        }
        public void Task(object sender, RoutedEventArgs e)
        {
            int result = Newton(Constants.TASK);
            if (result > 1)
            {
                taskText.Text = result.ToString();
            }


        }

        private int Newton(int mode)
        {
            int k, n;
            if (!Int32.TryParse(N.Text, out n) || !Int32.TryParse(K.Text, out k))
            {
                System.Windows.MessageBox.Show("Plese set k and n value", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                return -1;
            }
            nc.n = n;
            nc.k = k;
            int result = nc.Calculate(mode);
            switch (result)
            {
                case -1:
                    System.Windows.MessageBox.Show("K must be greater than -1 and N must be greater than 0", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                    return -1;
                case -2:
                    System.Windows.MessageBox.Show("K cannot be greater than N", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                    return -1;
                default:
                    return result;

            }
        }
        public void Delegate(object sender, RoutedEventArgs e)
        {
            int result = Newton(Constants.DELEGATE);
            if (result > 1)
            {
                delText.Text = result.ToString();
            }

        }
        public async void Async(object sender, RoutedEventArgs e)
        {
            int k, n;
            if (!Int32.TryParse(N.Text, out n) || !Int32.TryParse(K.Text, out k))
            {
                System.Windows.MessageBox.Show("Plese set k and n value", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            nc.n = n;
            nc.k = k;
            int result = await nc.CalAsync();
            asyncText.Text = result.ToString();

        }

        public void countFib(object sender, RoutedEventArgs e)
        {
            int i;
            if (!Int32.TryParse(fib.Text, out i))
            {
                System.Windows.MessageBox.Show("Plese set i value", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (i < 1)
            {
                System.Windows.MessageBox.Show("I must be greater than 1", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            BackgroundWorker fibo = new BackgroundWorker();
            fibo.DoWork += fiboDoWork;
            fibo.RunWorkerCompleted += fiboCompleted;
            fibo.ProgressChanged += fiboProgress;
            ProgressBar.Value = 0;
            fibo.RunWorkerAsync(i);
        }

        private void fiboCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            fibResult.Text = e.Result.ToString();
            ProgressBar.Value = 100;
        }

        private void fiboDoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bg = sender as BackgroundWorker;
            bg.WorkerReportsProgress = true;
            e.Result = CountFib((int)e.Argument, bg, e);
        }

        private void fiboProgress(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;
        }
        private long CountFib(int i, BackgroundWorker bg, DoWorkEventArgs e)
        {
            long result = 0;
            if (bg.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                long prev1 = 0;
                long prev2 = 0;
                for (int j = 0; j < i; j++)
                {
                    if (j < 2)
                    {
                        result = 1;
                    }
                    else
                    {
                        result = prev1 + prev2;

                    }
                    int percentComplete = (int)((float)j / i * 100);
                    bg.ReportProgress(percentComplete);
                    Thread.Sleep(20);
                    prev1 = prev2;
                    prev2 = result;
                }
            }
            return result;
        }

        public void Compress(object sender, RoutedEventArgs e)
        {
            var dlg = new FolderBrowserDialog()
            {
                Description = "Select directory to open"
            };
            DialogResult dialog = dlg.ShowDialog();
            if (dialog == System.Windows.Forms.DialogResult.OK)
            {
                DirectoryInfo dir = new DirectoryInfo(dlg.SelectedPath);
                compresser.Compress(dir);
               
            }
        }

        private void Decompress(object sender, RoutedEventArgs e)
        {
            var dlg = new FolderBrowserDialog()
            {
                Description = "Select directory to open"
            };
            DialogResult dialog = dlg.ShowDialog();
            if (dialog == System.Windows.Forms.DialogResult.OK)
            {
                DirectoryInfo dir = new DirectoryInfo(dlg.SelectedPath);
                compresser.Decompress(dir);

            }
        }

        private void DNS(object sender, RoutedEventArgs e)
        {
            var domains = DomainsChanger.ChangeDomains();
            TextBoxOutput.Text = "";
            foreach (var domain in domains)
            {
                TextBoxOutput.Text += $"{domain.Item1} => {domain.Item2}\n";
            }

        }

    }


}

