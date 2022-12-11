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
    public sealed partial class InfoPage : Page
    {
        public InfoPage()
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

        private void continueButton_Click(object sender, RoutedEventArgs e)
        {
            // make sure user enters all information before continuing
            if (nameTextBox.Text != "" 
                && decadeComboBox.SelectedIndex != -1 
                && ((bool)yesRadioButton.IsChecked || (bool)noRadioButton.IsChecked))
            {
                // TODO: if all info has been entered, assign info
                // to mp3 file object and navigate to the record page
                RecordPage recordPage = new RecordPage();
                Frame.Navigate(typeof(RecordPage), recordPage);
            }
            else
            {
                if (nameTextBox.Text == "")
                {
                    nameTextBox.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
                }
                if (decadeComboBox.SelectedIndex == -1)
                {
                    decadeComboBox.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
                }
                if ((bool)yesRadioButton.IsChecked == false && (bool)noRadioButton.IsChecked == false) {
                    // wanted to make radio buttons red, but unsuccessful
                }
                displayMissingInfoDialog();
            }
           
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
