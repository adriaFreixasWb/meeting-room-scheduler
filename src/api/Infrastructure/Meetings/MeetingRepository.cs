using MeetingRoomScheduler.API.Model;
using MeetingRoomScheduler.DAL;
using MeetingRoomScheduler.DAL.Models;

namespace MeetingRoomScheduler.API.Infrastructure.Appointments
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

            var rsvpSpace = _rsvpTable.Items
                .Join(_spaceTable.Items,
                rsvp => rsvp.SpaceId,
                spa => spa.Id,
                (rsvp, spa) => new { Meeting = new Meeting((uint)rsvp.Id, rsvp.Time, rsvp.Title), MeetingRoom = new MeetingRoom(spa.Id, spa.Name) });
            var result = new Dictionary<uint, Meeting>();
            
            foreach (var item in rsvpSpace)
            {
                var meeting = item.Meeting;
                meeting.AddMeetingRoom(item.MeetingRoom);
                result.Add(meeting.Id, meeting);
            }

            var employees = _rsvpEmployeeTable.Items.Join(_employeesTable.Items,
                rsvpEmp => rsvpEmp.EmployeeId,
                emp => emp.Id,
                (rsvpEmp, emp) => new { Id = (uint)rsvpEmp.RSVP_Id, Attendiee = new Attendiee((uint)emp.Id, emp.Name + " " + emp.Surname, emp.Email) });
            
            foreach (var employee in employees)
            {
                result[employee.Id].AddAttendies(employee.Attendiee);
            }
            
            return result.Values.ToList();

        }

        public void Create(Meeting meeting)
        {
            
            var space = _spaceTable.Items.First(x=>x.Name == meeting.MeetingRoom.Name);
            var rsvpEmp = _rsvpTable.Items.Last();
            _rsvpTable.Items.Add(new RSVP
            {
                Id = rsvpEmp.Id + 1,
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
                    RSVP_Id = rsvpEmp.Id + 1
                });
            }
            
        }
    }
}
