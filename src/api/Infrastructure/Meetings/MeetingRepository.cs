using MeetingRoomScheduler.API.Infrastructure.Employees;
using MeetingRoomScheduler.API.Model;
using MeetingRoomScheduler.DAL;
using MeetingRoomScheduler.DAL.Models;

namespace MeetingRoomScheduler.API.Infrastructure.Meetings
{
    public class MeetingRepository
    {
        private readonly SpaceTable _spaceTable;
        private readonly RSVPsTable _rsvpTable;
        private readonly RSVPEmployeesTable _rsvpEmployeeTable;
        private readonly EmployeesTable _employeesTable;

        public MeetingRepository(SpaceTable spaceTable, RSVPsTable rsvpTable, RSVPEmployeesTable rsvpEmployeeTable, EmployeesTable employeesTable)
        {
            _spaceTable = spaceTable;
            _rsvpTable = rsvpTable;
            _rsvpEmployeeTable = rsvpEmployeeTable;
            _employeesTable = employeesTable;
        }

        public List<Meeting> Get()
        {
            var meetings = _rsvpTable.Items
                .Join(_spaceTable.Items,
                rsvp => rsvp.SpaceId,
                spa => spa.Id,
                (rsvp, spa) => rsvp.ToMeetingWithRoom(spa))
                .ToList();

            var attendieeGroups = _rsvpEmployeeTable.Items.Join(_employeesTable.Items,
                rsvpEmp => rsvpEmp.EmployeeId,
                emp => emp.Id,
                (rsvpEmp, emp) => new { Id = (uint)rsvpEmp.RSVP_Id, Attendiee = emp.ToAttendee() })
                .GroupBy(x=>x.Id)
                .ToDictionary(x=>x.Key, v=>v.Select(x=>x.Attendiee));

            return meetings.AssociateAttendeesToMeeting(attendieeGroups);

        }        
        public Meeting GetBy(uint id)
        {
            var rsvp = _rsvpTable.Items.First(x => x.Id == id);
            var space = _spaceTable.Items.First(x => x.Id == rsvp.SpaceId);
            var attendees = _rsvpEmployeeTable.Items.Where(x => x.RSVP_Id == rsvp.Id)
                .Join(_employeesTable.Items,
                rsvp => rsvp.EmployeeId,
                emp => emp.Id,
                (rsvp, emp) => emp.ToAttendee());
            var meeting = rsvp.ToMeetingWithRoom(space);
            meeting.AddAttendees(attendees.ToArray());
            return meeting;
        }

        public uint Create(Meeting meeting)
        {
            var space = _spaceTable.Items.First(x=>x.Name == meeting.MeetingRoom.Name);
            var id = GetNextRSVPIndex();
            _rsvpTable.Items.Add(new RSVP
            {
                Id = id,
                SpaceId = space.Id,
                Time = meeting.Date,
                Title = meeting.Title,
            });
            foreach (var attendiee in meeting.Attendiees)
            {
                var employee = _employeesTable.Items.First(x => x.Email == attendiee.Email);
                _rsvpEmployeeTable.Items.Add(new RSVPEmployee
                {
                    EmployeeId = employee.Id,
                    RSVP_Id = id
                });
            }
            return (uint)id;
        }

        public int GetNextRSVPIndex()
        {
            var rsvp = _rsvpTable.Items.Last();
            return rsvp.Id + 1;
        }
    }
}
