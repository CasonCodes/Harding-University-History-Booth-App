using HistoryBoothApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace HistoryBoothApp.ViewModels
{
    public class UserRecordingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private UserRecording userRecording;

        public UserRecordingViewModel()
        {
            userRecording = new UserRecording();
        }

        public string fullName
        {
            get
            {
                return userRecording.fullName;
            }
            set
            {
                userRecording.fullName = value;
                OnPropertyChanged("fullName");
            }
        }

        public string storyDecade
        {
            get 
            {
                return userRecording.storyDecade;
            }
            set
            {
                userRecording.storyDecade = value;
                OnPropertyChanged("storyDecade");
            }
        }

        public bool wasStudent
        {
            get
            {
                return userRecording.wasStudent;
            }
            set
            {
                userRecording.wasStudent = value;
                OnPropertyChanged("wasStudent");
            }
        }

        public List<string> customTag
        {
            get
            {
                return userRecording.customTag;
            }
            set
            {
                userRecording.customTag = value;
                OnPropertyChanged("customTag");
            }
        }

        public string description
        {
            get
            {
                return userRecording.description;
            }
            set
            {
                userRecording.description = value;
                OnPropertyChanged("description");
            }
        }

        public TagLib.File recording
        {
            get
            {
                return userRecording.recording;
            }
            set
            {
                userRecording.recording = value;
                OnPropertyChanged("recording");
            }
        }

        private void OnPropertyChanged(string property)
        {
            // Notify any controls bound to the ViewModel that the property changed
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
