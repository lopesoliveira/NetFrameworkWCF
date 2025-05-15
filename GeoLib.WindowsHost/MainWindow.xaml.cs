using System;
using System.Collections.Generic;
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

namespace GeoLib.WindowsHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;

            this.Title = "UI Running on thread: " + Thread.CurrentThread.ManagedThreadId;
        }

        private ServiceHost _hostGeoManager = null;

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            _hostGeoManager = new ServiceHost(typeof(GeoManager));
            try
            {
                _hostGeoManager.Open();
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
                MessageBox.Show("The service host has been closed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was a problem closing the host.\n {ex.Message}");
            }

            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;
        }
    }
}
