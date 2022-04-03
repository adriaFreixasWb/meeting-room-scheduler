using MeetingRoomScheduler.API.Infrastructure.Employees;
using MeetingRoomScheduler.DAL.Models;

namespace MeetingRoomScheduler.API.Employees
{
    public class EmployeeService
    {
        public readonly EmployeeRepository _repository;

        public EmployeeService(EmployeeRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Employee> GetByEmail(IEnumerable<string> emails)
        {
            return _repository.GetByEmail(emails);
        }
    }
}
