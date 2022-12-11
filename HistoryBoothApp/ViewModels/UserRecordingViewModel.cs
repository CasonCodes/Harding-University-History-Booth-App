using HistoryBoothApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        

        //ADD MORE WHEN TAGLIB WORKS


        private void OnPropertyChanged(string property)
        {
            // Notify any controls bound to the ViewModel that the property changed
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
