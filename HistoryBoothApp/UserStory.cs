using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace HistoryBoothApp
{
    public enum PersonType
    {
        student, alumni, staff, faculty, other
    }

    internal class UserStory
    {
        public int id { get; set; } 
        public string title { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string storyYear { get; set; }
        public DateTime date { get; set; }
        public PersonType personType { get; set; }
        public string description { get; set; }
        public List<string> tags { get; set; }
        public StorageFile audioRecording { get; set; }
    }
}
