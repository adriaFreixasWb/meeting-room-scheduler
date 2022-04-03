using MeetingRoomScheduler.DAL;
using MeetingRoomScheduler.DAL.Models;

namespace MeetingRoomScheduler.API.Infrastructure.Employees
{
    public class EmployeeRepository
    {
        private readonly EmployeesTable _employeesTable;

        public EmployeeRepository(EmployeesTable employeesTable)
        {
            _employeesTable = employeesTable;
        }

        public IEnumerable<Employee> Get()
        {
            return _employeesTable.Items;
        }
        public IEnumerable<Employee> Search(string email)
        {
            return _employeesTable.Items.Where(x => x.Email.Contains(email));
        }

        public IEnumerable<Employee> GetByEmail(IEnumerable<string> emails)
        {
            var result = new List<Employee>();
            foreach (var email in emails)
            {
                result.AddRange(Search(email));
            }
            return result;
        }
    }
}
