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

        private UserStory userStory;

        public InfoPage()
        {            
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(800, 500);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            // fetches current year, adds combo box items for every year up until current year
            int currentYear = Convert.ToInt32(DateTime.Now.Year.ToString());
            for (int y = 1920; y <= currentYear; y++)
            {
                yearComboBox.Items.Add(y);                
            }

            originalColor = firstNameTextBox.BorderBrush;
            userStory = new UserStory();
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
                || (bool)staffRadioButton.IsChecked
                || (bool)otherRadioButton.IsChecked;
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
                // to UserStory object and navigate to the recording page 

                userStory.date = DateTime.Now;
                userStory.firstName = firstNameTextBox.Text;
                userStory.lastName = lastNameTextBox.Text;
                userStory.title = storyTitleTextBox.Text;
                userStory.storyYear = yearComboBox.SelectedItem.ToString(); // TODO: ?

                if (studentRadioButton.IsChecked == true)
                {
                    userStory.personType = PersonType.student;
                }
                else if (alumniRadioButton.IsChecked == true)
                {
                    userStory.personType = PersonType.alumni;
                }
                else if (facultyRadioButton.IsChecked == true)
                {
                    userStory.personType = PersonType.faculty;
                }
                else if (staffRadioButton.IsChecked == true)
                {
                    userStory.personType = PersonType.staff;
                }
                else if (otherRadioButton.IsChecked == true)
                {
                    userStory.personType = PersonType.other;
                }

                //RecordPage recordPage = new RecordPage();
                Frame.Navigate(typeof(RecordPage), userStory);
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
                    otherRadioButton.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                }
                else 
                {
                    studentRadioButton.Foreground = originalColor;
                    alumniRadioButton.Foreground = originalColor;
                    facultyRadioButton.Foreground = originalColor;
                    staffRadioButton.Foreground = originalColor;
                    otherRadioButton.Foreground = originalColor;
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
