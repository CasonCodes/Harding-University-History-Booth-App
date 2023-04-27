using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HistoryBoothApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(800, 500);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;

            // TODO: load any/all mp3 recordings (probably should be on admin page)

            // TODO: initialize mp3 object, pass to info page (necessary?)
            

        }

        private async void displayAcknowledgement()
        {            
            ContentDialog acknowledgement = new ContentDialog();
            acknowledgement.Title = "Recording Acknowledgement";
            acknowledgement.Content =
                "This program uses the microphone to record your voice.\n" +
                "Any voice recording submitted is for the use of Harding University only.\n\n" +
                "Do you agree to these terms?";

            acknowledgement.IsPrimaryButtonEnabled = true;
            acknowledgement.PrimaryButtonText = "Agree";
            acknowledgement.PrimaryButtonClick += OnPrimaryButtonClick;

            acknowledgement.IsSecondaryButtonEnabled = true;
            acknowledgement.SecondaryButtonText = "Disagree";
            // if user presses disagree, remain on main page

            await acknowledgement.ShowAsync();
        }

        private void OnPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // Navigate to the next page if the user clicks the primary button
            InfoPage infoPage = new InfoPage();
            Frame.Navigate(typeof(InfoPage), infoPage);
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            displayAcknowledgement();
        }

        private void adminButton_Click(object sender, RoutedEventArgs e)
        {
           LoginPage loginPage = new LoginPage();
           Frame.Navigate(typeof(LoginPage), loginPage);
        }

        private void quitButton_Click(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }
    }
}
