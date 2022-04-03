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

            var rsvpEmp = _rsvpTable.Items
            .Join(_rsvpEmployeeTable.Items,
                rsvp => rsvp.Id,
                rsem => rsem.RSVP_Id,
                (rsvp, rsem) => new { Id = rsvp.Id, Title = rsvp.Title, SpaceId = rsvp.SpaceId, Time = rsvp.Time, EmployeeId = rsem.EmployeeId })
            .ToList();
            var rsvpSp = rsvpEmp.Join(_spaceTable.Items,
                rsvp => rsvp.SpaceId,
                spa => spa.Id,
                (rsvp, spa) => new { Id = rsvp.Id, Title = rsvp.Title, SpaceName = spa.Name, Time = rsvp.Time, EmployeeId = rsvp.EmployeeId })
            .ToList();
            var rsvpName = rsvpSp.Join(_employeesTable.Items,
                rsvp => rsvp.EmployeeId,
                emp => emp.Id,
                (rsvp, emp) => new { Id = rsvp.Id, Title = rsvp.Title, SpaceName = rsvp.SpaceName, Time = rsvp.Time, Employee = emp.Name + " " +emp.Surname })
            .ToList();
            var meeting = rsvpName.GroupBy(x => x.Id)
            .Select(x =>
                new Meeting((uint)x.First().Id, x.First().Time, x.First().Title)).ToList();
            return meeting;
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
