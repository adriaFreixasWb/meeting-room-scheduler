using MeetingRoomScheduler.DAL.Models;

namespace MeetingRoomScheduler.DAL
{
    public class RSVPEmployeesTable
    {
        public List<RSVPEmployee> Items { get; } = new List<RSVPEmployee>
        {
            new RSVPEmployee
            {
                RSVP_Id = 1,
                EmployeeId = 21
            },
            new RSVPEmployee
            {
                RSVP_Id = 1,
                EmployeeId = 45
            }
        };

    }
}
