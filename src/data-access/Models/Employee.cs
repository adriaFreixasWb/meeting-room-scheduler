namespace MeetingRoomScheduler.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BadgeId { get; set; }
        public string JobTitle { get; set; }
        public string Email { get; internal set; }
    }
}
