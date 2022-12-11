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

            MediaTransportControls mtc = new MediaTransportControls();
            mtc.IsFullWindowButtonVisible = false;
            mtc.IsNextTrackButtonVisible = false;
            mtc.IsPreviousTrackButtonVisible = false;
            mediaElement.TransportControls = mtc;
        }

        private void doneButton_Click(object sender, RoutedEventArgs e)
        {
            ConfirmPage confirmPage = new ConfirmPage();
            Frame.Navigate(typeof(ConfirmPage), confirmPage);
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO:
            // start playing current mp3 file at slider value of media player
            // may not need a dedicated button if a media player control is used
            if (playButton.Content.ToString() == "▶ Play")
            {
                recordButton.Content = "⬤ Record";
                playButton.Content = "❚❚ Pause";
            }
            else if (playButton.Content.ToString() == "❚❚ Pause")
            {
                playButton.Content = "▶ Play";
            }
        }

        private void eraseButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO:
            // erase the current data for the mp3 file
            if (playButton.Content.ToString() == "❚❚ Pause")
            {
                playButton.Content = "▶ Play";
            }
            if (recordButton.Content.ToString() == "❚❚ Pause")
            {
                recordButton.Content = "⬤ Record";
            }
            minuteTextBlock.Text = "0";
            secondTextBlock.Text = "00";
        }

        private void recordButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO:
            // enable recording mode
            // this button can flip back and forth between 'record' <--> 'pause'
            if (recordButton.Content.ToString() == "⬤ Record")
            {
                recordButton.Content = "❚❚ Pause";
                playButton.Content = "▶ Play";
                // TODO: start recording and append to mp3 file
            }
            else if (recordButton.Content.ToString() == "❚❚ Pause")
            {
                recordButton.Content = "⬤ Record";
                // TODO: pause the recording
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }                
    }
}
