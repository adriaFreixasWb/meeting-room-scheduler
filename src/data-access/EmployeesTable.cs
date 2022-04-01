using MeetingRoomScheduler.DAL.Models;

namespace MeetingRoomScheduler.DAL
{
    public class EmployeesTable
    {
        public List<Employee> Items { get; } = new List<Employee>
        {
            new Employee
            {
                BadgeId = "1",
                Id = 21,
                Name = "Erika",
                Surname = "Berger",
                JobTitle = "Magazine owner",
                Email = "erika.berger@millenium-mag.com"
            },
            new Employee
            {
                Id = 88,
                Name = "Lisbeth",
                Surname = "Salander",
                JobTitle = "Investigator",
                Email = "lsalander@miltonsecurity.com"
            },
            new Employee
            {
                BadgeId = "23",
                Id = 45,
                Name = "Mikael",
                Surname = "Blomkvist",
                JobTitle = "Reporter",
                Email = "mikael.blomkvist@millenium-mag.com"
            }            ,
            new Employee
            {
                Id = 251,
                Name = "Hans-Erik",
                Surname = "Wennerström",
                JobTitle = "Company owner",
                Email = "hewennerstrom@oil-spoil.com"
            }
        };
    }
}
