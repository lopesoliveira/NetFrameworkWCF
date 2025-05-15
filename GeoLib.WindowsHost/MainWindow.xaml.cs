using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
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
using GeoLib.Services;
using GeoLib.WindowsHost.Services;

namespace GeoLib.WindowsHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow MainUI { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;

            MainUI = this;

            this.Title = "UI Running on thread: " + Thread.CurrentThread.ManagedThreadId +
                         " | Process: " + Process.GetCurrentProcess().Id.ToString();
        }

        private ServiceHost _hostGeoManager = null;
        private ServiceHost _hostMessageManager = null;

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            _hostGeoManager = new ServiceHost(typeof(GeoManager));
            _hostMessageManager = new ServiceHost(typeof(MessageManager));
            try
            {
                _hostGeoManager.Open();
                _hostMessageManager.Open();
                MessageBox.Show("The service host was started.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was a problem starting the host. \n {ex.Message}");
            }

            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _hostGeoManager.Close();
                _hostMessageManager.Close();
                MessageBox.Show("The service host has been closed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was a problem closing the host.\n {ex.Message}");
            }

            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;
        }

        public void ShowMessage(string message)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;

            lblMessage.Content = message + Environment.NewLine + $"( shown on thread {threadId}" +
                                 " | Process: " + Process.GetCurrentProcess().Id + " )";
        }
    }
}
