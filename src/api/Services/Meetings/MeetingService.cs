using MeetingRoomScheduler.API.Employees;
using MeetingRoomScheduler.API.Infrastructure.Appointments;
using MeetingRoomScheduler.API.Infrastructure.Stances;
using MeetingRoomScheduler.API.Model;
using MeetingRoomScheduler.API.Services.MeetingRooms;
using MeetingRoomScheduler.DAL.Models;

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

        public List<StanceAppointment> Get()
        {
            return _repository.Get();
        }

        public StanceAppointment Create(StanceAppointmentRequest appointment)
        {
            _repository.Create(appointment);
            return _repository.Get().Last();
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
