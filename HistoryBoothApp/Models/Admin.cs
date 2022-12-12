using System.Collections.Generic;

namespace HistoryBoothApp.Models
{
    public class Admin
    {
        public string password { get; set; }
        public List<UserRecording> userRecordings { get; set; }

        public Admin()
        {
            password = "password";
            userRecordings = new List<UserRecording>
            {
                //TODO:
                //CODE FOR RETRIEVING RECORDINGS (localFolder?)
            };
        }
    }
}
