namespace MeetingRoomScheduler.Model
{
    public class MeetingRoomSchedule
    {
        public DateTime Date { get; set; }
        public IEnumerable<string> People { get; set; }
        public string StanceName { get; set; }
    }
}
