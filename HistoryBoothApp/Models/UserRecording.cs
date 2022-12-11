namespace HistoryBoothApp.Models
{
    public class UserRecording
    {
        public string fullName { get; set; }

        public string storyDecade { get; set; }

        public bool wasStudent { get; set; }

        //property for the recording mp3 itself
        public Taglib.File recording { get; set; }

        public Taglib.File ReadRecording()
        {
            return recording;
        }

        public bool WriteRecording(Taglib.File value)
        {
            return true;
        }
    }
}
