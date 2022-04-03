namespace MeetingRoomScheduler.API.Model
{
    public class StanceAppointment
    {
        public DateTime Date { get; set; }
        public List<string> Emails { get; set; }
        public string StanceName { get; set; }
        public string Title { get; set; }
    }
}
