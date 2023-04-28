using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
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
        private List<string> selectedTags = new List<string>();
        private Brush originalColor;
        UserStory userStory;
        int currentYear;
        public ConfirmPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(800, 500);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            // fetches current year, adds combo box items for
            // each decade from the 1920's to the current decade
            currentYear = Convert.ToInt32(DateTime.Now.Year.ToString());
            for (int y = 1920; y <= currentYear; y++)
            {
                yearComboBox.Items.Add(y);
            }

            originalColor = firstNameTextBox.BorderBrush;




            //for (int i = 0; i < 1; i++)
            //{
            //    selectedTags.Add(userStory.tags[i]);
            //}



            //< ComboBox x: Name = "storyTagsComboBox" HorizontalAlignment = "Center" Width = "400" PlaceholderForeground = "LightGray" PlaceholderText = "optional..." SelectionChanged = "ComboBox_SelectionChanged" >
            //        < CheckBox Content = "Social Clubs" />
            //        < CheckBox Content = "Dorm Life" />
            //        < CheckBox Content = "Chapel" />
            //        < CheckBox Content = "Study Abroad" />
            //        < CheckBox Content = "Yearbook" />
            //        < CheckBox Content = "Sports" />
            //    </ ComboBox >




        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Retrieve the userStory object from the NavigationEventArgs parameter
            userStory = (UserStory)e.Parameter;

            // TODO: display user story info
            firstNameTextBox.Text = userStory.firstName;
            lastNameTextBox.Text = userStory.lastName;
            storyTitleTextBox.Text = userStory.title;
            yearComboBox.SelectedIndex = currentYear - Int32.Parse(userStory.storyYear);
            switch (userStory.personType)
            {
                case PersonType.student: studentRadioButton.IsChecked = true; break;
                case PersonType.alumni: alumniRadioButton.IsChecked = true; break;
                case PersonType.staff: staffRadioButton.IsChecked = true; break;
                case PersonType.faculty: facultyRadioButton.IsChecked = true; break;
                case PersonType.other: otherRadioButton.IsChecked = true; break;
            }
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

        

        private void SaveAllFields()
        {
            // make sure user enters all information before continuing
            if (allInformationEntered())
            {
                // assign info to UserStory object 
                userStory.date = DateTime.Now;
                userStory.firstName = firstNameTextBox.Text;
                userStory.lastName = lastNameTextBox.Text;
                userStory.title = storyTitleTextBox.Text;
                userStory.storyYear = yearComboBox.SelectedItem.ToString();
                userStory.description = descriptionTextBox.Text;
                for (int i = 0; i < tagsAutoSuggestBox.Items.Count; i++)
                {
                    // TODO: collect and save all tags clicked on by user in combo bos
                }
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
            }
        }

        private void OnPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // TODO: save mp3 file and metadata

            // TODO: initialize the mp3 object for next user

            // TODO: return to the main page (clear all the metadata)
            // maybe in onNavigatedTo() in MainPage, clear all data 




            SaveAllFields();

            Frame.GoBack();
            Frame.GoBack();
            Frame.GoBack();
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

        private void finishButton_Click(object sender, RoutedEventArgs e)
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

                displayConfirmDialog();

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

                if (radioButtonSelected() == false)
                {
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
