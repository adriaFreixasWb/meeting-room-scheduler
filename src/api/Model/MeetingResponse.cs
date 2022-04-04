namespace MeetingRoomScheduler.API.Model
{
    public class MeetingResponse
    {
        public DateTime Date { get; set; }
        public IEnumerable<string> Emails { get; set; }
        public string MeetingRoom { get; set; }
        public string Title { get; set; }
    }
}
