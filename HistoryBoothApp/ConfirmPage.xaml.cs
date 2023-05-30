using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
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
    public sealed partial class ConfirmPage : Page
    {
        private Brush originalColor;
        private UserStory userStory;
        int currentYear;
        public ConfirmPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(800, 500);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            userStory = new UserStory();

            // fetches current year, adds combo box items for
            // each decade from the 1920's to the current decade
            currentYear = Convert.ToInt32(DateTime.Now.Year.ToString());
            for (int y = 1920; y <= currentYear; y++)
            {
                yearComboBox.Items.Add(y);
            }
           
            originalColor = firstNameTextBox.BorderBrush;           
        }

        #region STORY_INFO_CODE
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);         
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

                // collect and save all checked tags 
                for (int i = 1; i <= 20; i++)
                {
                    string checkBoxName = "tag" + i.ToString();
                    if (FindName(checkBoxName) is CheckBox checkBox)
                    {
                        if (checkBox.IsChecked == true)
                        {
                            // save tag to userStory object
                            userStory.tags.Add(checkBox.Content.ToString()); 
                        }                                                                      
                    }
                }
            }
        }

        private void OnPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // save userStory object
            SaveAllFields();

            // submit userStory object
            if (userStory.submitStory())
            {
                // return to the main page 
                Frame.GoBack();
            }
            else
            {
                // TODO: display "could not submit please try again" dialog
            }          
            
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
                // assign all info to UserStory object
                userStory.date = DateTime.Now;
                userStory.firstName = firstNameTextBox.Text;
                userStory.lastName = lastNameTextBox.Text;
                userStory.title = storyTitleTextBox.Text;
                userStory.storyYear = yearComboBox.SelectedItem.ToString();
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
                switch (firstNameTextBox.Text.Length)
                {
                    case 0: firstNameTextBox.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red); break;
                    default: firstNameTextBox.BorderBrush = originalColor; break;
                }

                switch (lastNameTextBox.Text.Length)
                {
                    case 0: lastNameTextBox.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red); break;
                    default: lastNameTextBox.BorderBrush = originalColor; break;
                }

                switch (yearComboBox.SelectedIndex)
                {
                    case -1: yearComboBox.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red); break;
                    default: yearComboBox.BorderBrush = originalColor; break;
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

        #endregion

        #region RECORDING_CODE

        bool brandNewRecording = true;
        private MediaCapture mediaCapture;

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            if (playButton.Content.ToString() == "▶ Play")
            {
                recordButton.Content = "⬤ Record";
                playButton.Content = "❚❚ Pause";
                // TODO: play recorded audio back to user

            }
            else if (playButton.Content.ToString() == "❚❚ Pause")
            {
                playButton.Content = "▶ Play";
                // TODO: pause current audio

            }
        }

        private void eraseButton_Click(object sender, RoutedEventArgs e)
        {











            // TODO: erase the current recording so user can start over   
            brandNewRecording = true;












            if (playButton.Content.ToString() == "❚❚ Pause")
            {
                playButton.Content = "▶ Play";
            }
            if (recordButton.Content.ToString() == "❚❚ Pause")
            {
                recordButton.Content = "⬤ Record";
            }
            minuteTextBlock.Text = "00";
            secondTextBlock.Text = "00";
        }

        private async void recordButton_Click(object sender, RoutedEventArgs e)
        {
            if (recordButton.Content.ToString() == "⬤ Record")
            {
                recordButton.Content = "❚❚ Pause";
                playButton.Content = "▶ Play";

                if (brandNewRecording)
                {
                    // TODO: make a new recording
                    mediaCapture = new MediaCapture();
                    await mediaCapture.InitializeAsync();
                    brandNewRecording = false;
                }

                // TODO: start recording         
                //StorageFile file = await KnownFolders.MusicLibrary.CreateFileAsync("recording.wav", CreationCollisionOption.ReplaceExisting);
                //MediaEncodingProfile profile = MediaEncodingProfile.CreateWav(AudioEncodingQuality.High);
                //await mediaCapture.StartRecordToStorageFileAsync(profile, file);



            }
            else if (recordButton.Content.ToString() == "❚❚ Pause")
            {
                recordButton.Content = "⬤ Record";

                // TODO: pause the recording



                if (!brandNewRecording)
                {
                    // TODO: append recording NAME FORMAT: userStory(" + userStory.date.ToString() + ").wav 
                }

            }
        }

        #endregion
    }
}
