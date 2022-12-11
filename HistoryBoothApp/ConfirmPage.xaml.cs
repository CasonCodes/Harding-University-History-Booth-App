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
    public sealed partial class ConfirmPage : Page
    {
        public ConfirmPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(800, 500);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            // fetches current year, adds combo box items for
            // each decade from the 1920's to the current decade
            int currentYear = Convert.ToInt32(DateTime.Now.Year.ToString());
            for (int y = 1920; y < currentYear; y += 10)
            {
                decadeComboBox.Items.Add(y + "'s");
            }

            // TODO: load mp3 file meta data to show all user information together
            

        }

        private async void displayConfirmDialog()
        {
            ContentDialog acknowledgement = new ContentDialog();
            acknowledgement.Title = "Confirm Recording";
            acknowledgement.Content =
                "Are you sure you want to submit your recording?\n" +
                "Once your press 'Complete and Submit', your story will be saved!";

            acknowledgement.IsPrimaryButtonEnabled = true;
            acknowledgement.PrimaryButtonText = "Complete and Submit";
            acknowledgement.PrimaryButtonClick += OnPrimaryButtonClick;

            acknowledgement.IsSecondaryButtonEnabled = true;
            acknowledgement.SecondaryButtonText = "Cancel";
            // if user presses cancel, remain on confirm page

            await acknowledgement.ShowAsync();
        }

        private void OnPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // TODO: save mp3 file and metadata

            // TODO: initialize the mp3 object for next user

            // TODO: return to the main page (clear all the metadata)
            // maybe in onNavigatedTo() in MainPage, clear all data 
            Frame.GoBack();
            Frame.GoBack();
            Frame.GoBack();
        }

        private async void displayMissingInfoDialog()
        {
            ContentDialog acknowledgement = new ContentDialog();
            acknowledgement.Title = "Missing Information";
            acknowledgement.Content = "Look's like we're missing some details.\n" +
                "Please make sure all information has been entered.";

            acknowledgement.IsPrimaryButtonEnabled = true;
            acknowledgement.PrimaryButtonText = "OK";

            await acknowledgement.ShowAsync();
        }

        private void finishButton_Click(object sender, RoutedEventArgs e)
        {
            // make sure user didnt clear out the information           
            if (nameTextBox.Text == "")
            {
                nameTextBox.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
                displayMissingInfoDialog();
            }
            else
            {
                // show confirm dialog
                displayConfirmDialog();                
            }            
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
