using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using TagLib.Mpeg;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.System;
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
    
    public sealed partial class RecordPage : Page
    {
        UserStory userStory;
        bool brandNewRecording = true;
        private MediaCapture mediaCapture;
        public RecordPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(800, 500);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Retrieve the userStory object from the NavigationEventArgs parameter
            userStory = (UserStory)e.Parameter;
        }

        private void doneButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: stop and save recording to userStory









            //ConfirmPage confirmPage = new ConfirmPage();
            Frame.Navigate(typeof(ConfirmPage), userStory);




            




        }

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
                StorageFile file = await KnownFolders.MusicLibrary.CreateFileAsync("recording.wav", CreationCollisionOption.ReplaceExisting);
                MediaEncodingProfile profile = MediaEncodingProfile.CreateWav(AudioEncodingQuality.High);
                await mediaCapture.StartRecordToStorageFileAsync(profile, file);



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

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }        
        

    }
}
