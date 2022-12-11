using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryBoothApp.Models
{
    public class Admin
    {
        public string password { get; set; }

        public List<UserRecording> userRecordings { get; set; }

        public Admin()
        {
            password = "password";
            userRecordings = new List<UserRecording>();
        }

        //Method to retrieve list of available user recordings and return
        public List<UserRecording> FindUserRecordings()
        {
            List<UserRecording> availableRecordings = null;

            return availableRecordings;
        }
    }
}
