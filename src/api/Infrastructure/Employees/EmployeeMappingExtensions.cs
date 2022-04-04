using MeetingRoomScheduler.API.Model;
using MeetingRoomScheduler.DAL.Models;

namespace MeetingRoomScheduler.API.Infrastructure.Employees
{
    public static class EmployeeMappingExtensions
    {
        public static Attendee ToAttendee(this Employee emp) =>
            new Attendee((uint)emp.Id, emp.Name + " " + emp.Surname, emp.Email);
    }
}
