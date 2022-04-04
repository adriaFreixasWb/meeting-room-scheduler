using MeetingRoomScheduler.API.Employees;
using MeetingRoomScheduler.API.Infrastructure.Meetings;
using MeetingRoomScheduler.API.Infrastructure.MeetingRooms;
using MeetingRoomScheduler.API.Model;
using MeetingRoomScheduler.API.Services.MeetingRooms;
using MeetingRoomScheduler.DAL.Models;
using MeetingRoomScheduler.API.Infrastructure.Employees;

namespace MeetingRoomScheduler.API.Services
{
    public class MeetingService
    {
        private readonly MeetingRepository _repository;
        private readonly EmployeeService _employeeService;
        private readonly MeetingRoomService _meetingRoomService;

        public MeetingService(MeetingRepository repository, EmployeeService employeeService, MeetingRoomService meetingRoomService)
        {
            _repository = repository;
            _employeeService = employeeService;
            _meetingRoomService = meetingRoomService;
        }

        public List<Meeting> Get()
        {
            return _repository.Get();
        }

        public Meeting Create(MeetingRequest request)
        {
            
            var meetingRoom = _meetingRoomService.GetBy(request.MeetingRoom);
            if(meetingRoom == null)
            {
                return null;
            }
            var meeting = new Meeting(request.Date, request.Title);
            meeting.AddMeetingRoom(meetingRoom);
            var attendees = _employeeService.GetByEmail(request.Emails)
                .Select(x => x.ToAttendee());
            meeting.AddAttendees(attendees.ToArray());
            if(!meeting.HasMinimumAttendies)
            {
                throw new ArgumentException("Not enough attendes");
            }
            var id = _repository.Create(meeting);
            return _repository.GetBy(id);
        }

        public bool CheckAllAssitantsExist(IEnumerable<string> emails)
        {
            var employees = _employeeService.GetByEmail(emails)
                .ToList();
            
            if(!SameCount(emails, employees))
            {
                return false;
            }
            return !IsAnyEmailNotFound(emails.ToList(), employees.Select(x => x.Email));
            
        }

        private bool IsAnyEmailNotFound(List<string> emails, IEnumerable<string> employeeEmails)
        {
            var isMissing = false;
            var count = 0;
            while(!isMissing && count < emails.Count())
            {
                if(!employeeEmails.Any(x=>x.Contains(emails[count], StringComparison.OrdinalIgnoreCase)))
                {
                    isMissing = true;
                }
                count++;
            }
            return isMissing;
        }

        public bool CheckMeetingRoomExists(string stanceName)
        {
            return _meetingRoomService.CheckIfExists(stanceName);
        }

        private bool SameCount(IEnumerable<string> emails, IEnumerable<Employee> employees)
        {
            return emails.Count() == employees.Count();
        }
    }
}
