namespace MeetingRoomScheduler.DAL.Models
{
    public class RSVP
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SpaceId { get; set; }
        public DateTime Time { get; set; }
    }
}
