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
        private Brush originalColor;

        public InfoPage()
        {            
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(800, 500);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            // fetches current year, adds combo box items for
            // each decade from the 1920's to the current decade
            int currentYear = Convert.ToInt32(DateTime.Now.Year.ToString());
            for (int y = 1920; y < currentYear; y++)
            {
                yearComboBox.Items.Add(y);                
            }

            originalColor = firstNameTextBox.BorderBrush;
        }
        private async void displayMissingInfoDialog()
        {
            ContentDialog acknowledgement = new ContentDialog();
            acknowledgement.Title = "Missing Information";
            acknowledgement.Content = "Looks like we're missing some details.\n" + 
                "Please make sure all information has been entered.";

            acknowledgement.IsPrimaryButtonEnabled = true;
            acknowledgement.PrimaryButtonText = "OK";

            await acknowledgement.ShowAsync();
        }

        private bool radioButtonSelected()
        {
            return (bool)studentRadioButton.IsChecked
                || (bool)alumniRadioButton.IsChecked
                || (bool)facultyRadioButton.IsChecked
                || (bool)staffRadioButton.IsChecked;
                //|| (bool)otherRadioButton.IsChecked;
        }

        private bool allInformationEntered()
        {
            return firstNameTextBox.Text != ""
                && lastNameTextBox.Text != ""
                && yearComboBox.SelectedIndex != -1
                && radioButtonSelected();
        }

        private void continueButton_Click(object sender, RoutedEventArgs e)
        {
            // make sure user enters all information before continuing
            if (allInformationEntered())
            {
                // TODO: if all info has been entered, assign info
                // to mp3 file object and navigate to the record page
                RecordPage recordPage = new RecordPage();
                Frame.Navigate(typeof(RecordPage), recordPage);
            }
            else
            {
                // TODO: refactor

                if (firstNameTextBox.Text == "")
                {
                    firstNameTextBox.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
                }
                else
                {
                    firstNameTextBox.BorderBrush = originalColor;
                }

                if (lastNameTextBox.Text == "")
                {
                    lastNameTextBox.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
                }
                else
                {
                    lastNameTextBox.BorderBrush = originalColor;
                }

                if (yearComboBox.SelectedIndex == -1)
                {
                    yearComboBox.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
                }
                else
                {
                    yearComboBox.BorderBrush = originalColor;
                }

                if (radioButtonSelected() == false) {
                    studentRadioButton.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                    alumniRadioButton.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                    facultyRadioButton.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                    staffRadioButton.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                    //otherRadioButton.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                }
                else 
                {
                    studentRadioButton.Foreground = originalColor;
                    alumniRadioButton.Foreground = originalColor;
                    facultyRadioButton.Foreground = originalColor;
                    staffRadioButton.Foreground = originalColor;
                    //otherRadioButton.Foreground = originalColor;
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
