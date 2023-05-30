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

namespace HistoryBoothApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(800, 500);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;      
        }

        private async void displayAcknowledgement()
        {            
            ContentDialog acknowledgement = new ContentDialog();
            acknowledgement.Title = "Recording Acknowledgement";
            acknowledgement.Content =
                "This program uses the microphone to record your voice.\n" +
                "All voice recordings submitted are for the research\npurposes of Harding University only.\n\n" +
                "What do we mean by research?\n" +
                "-----------------------------------------\n" +
                "Harding students and faculty may use these recordings in classes to practice interview skills or in papers as evidence." +
                " All identifying information will be removed prior to any presentations or publication.\n\n" +
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
            Frame.Navigate(typeof(ConfirmPage));
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
