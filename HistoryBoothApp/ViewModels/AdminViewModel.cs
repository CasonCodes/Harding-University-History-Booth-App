using HistoryBoothApp.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace HistoryBoothApp.ViewModels
{
    public class AdminViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Admin admin;

        public ObservableCollection<UserRecordingViewModel> userRecordings { get; set; }

        public string password
        {
            get
            {
                return admin.password;
            }
            set
            {
                admin.password = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("password"));
            }
        }

        public AdminViewModel()
        {
            this.admin = new Admin();

            userRecordings = new ObservableCollection<UserRecordingViewModel>();

            foreach (var recording in admin.userRecordings) 
            {
                var newRecording = new UserRecordingViewModel 
                {
                    fullName = recording.fullName,
                    storyDecade = recording.storyDecade,
                    wasStudent = recording.wasStudent
                };
                newRecording.PropertyChanged += OnPropertyChanged;
                userRecordings.Add(newRecording);
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }
    }
}
