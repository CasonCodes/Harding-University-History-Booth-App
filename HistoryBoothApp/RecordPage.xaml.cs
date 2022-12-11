using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HistoryBoothApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RecordPage : Page
    {
        public RecordPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(800, 500);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            
        }

        private void doneButton_Click(object sender, RoutedEventArgs e)
        {
            ConfirmPage confirmPage = new ConfirmPage();
            Frame.Navigate(typeof(ConfirmPage), confirmPage);
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO:
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO:
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO:
        }

        private void recordButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO:
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
