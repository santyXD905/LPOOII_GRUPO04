using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private double duration = -1;
        public About()
        {
            InitializeComponent();
            bgvideo.MediaOpened += bgvideo_MediaOpened;
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += new EventHandler(ticktock);
            timer.Start();

            btnStop.IsEnabled = false;
            btnMoveBack.IsEnabled = false;
            btnMoveForw.IsEnabled = false;
        }
        void ticktock(object sender, EventArgs args)
        {
            progre.Value = bgvideo.Position.TotalSeconds;
            if (progre.Value == duration)
            {
                btnPlay.IsEnabled = true;
                btnPlay.Content = "Replay";
            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            btnPlay.IsEnabled = false;
            btnStop.IsEnabled = true;
            btnMoveBack.IsEnabled = true;
            btnMoveForw.IsEnabled = true;
            btnPlay.Visibility = Visibility.Hidden;
            btnStop.Visibility = Visibility.Visible;
            if (duration == progre.Value)
            {
                progre.Value = 0;
                bgvideo.Stop();
            }
            bgvideo.Play();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            bgvideo.Pause();
            btnPlay.IsEnabled = true;
            btnPlay.Content = "Continue";
            btnPlay.Visibility = Visibility.Visible;
            btnStop.Visibility = Visibility.Hidden;
        }

        private void btnMoveBack_Click(object sender, RoutedEventArgs e)
        {
            bgvideo.Position = bgvideo.Position - TimeSpan.FromSeconds(10);
        }

        private void btnMoveForw_Click(object sender, RoutedEventArgs e)
        {
            bgvideo.Position = bgvideo.Position + TimeSpan.FromSeconds(10);
        }

        private void bgvideo_MediaOpened(object sender, RoutedEventArgs e)
        {
            duration = bgvideo.NaturalDuration.TimeSpan.TotalSeconds;
            progre.Minimum = 0;
            progre.Maximum = duration;

        }

        private void btnVoumeDown_Click(object sender, RoutedEventArgs e)
        {
            if (bgvideo.Volume == 0) return;
            bgvideo.Volume -= 0.1;
        }

        private void btnVolumeUp_Click(object sender, RoutedEventArgs e)
        {
            if (bgvideo.Volume == 1) return;
            bgvideo.Volume += 0.1;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
