namespace MeetingRoomScheduler.API.Model
{
    public class MeetingRequest
    {
        public DateTime Date { get; set; }
        public List<string> Emails { get; set; }
        public string MeetingRoom { get; set; }
        public string Title { get; set; }
     }
}
