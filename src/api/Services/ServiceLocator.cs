using MeetingRoomScheduler.API.Employees;
using MeetingRoomScheduler.API.Infrastructure.Appointments;
using MeetingRoomScheduler.API.Infrastructure.Employees;
using MeetingRoomScheduler.API.Infrastructure.Stances;
using MeetingRoomScheduler.API.Services.MeetingRooms;
using MeetingRoomScheduler.API.Validators;
using MeetingRoomScheduler.DAL;

namespace MeetingRoomScheduler.API.Services
{
    public static class ServiceLocator
    {
        private static SpaceTable SpaceTableConnection { get; } = new SpaceTable();
        public static RSVPsTable RSVPTAbleConnection { get; } = new RSVPsTable();
        public static RSVPEmployeesTable RSVPEmployeeTableConnection { get; } = new RSVPEmployeesTable();
        public static EmployeesTable EmployeesTableConnection { get; } = new EmployeesTable();

        private static MeetingRepository MeetingRepository { get; } = new MeetingRepository(SpaceTableConnection, RSVPTAbleConnection, RSVPEmployeeTableConnection, EmployeesTableConnection);
        private static EmployeeRepository EmployeeRepository { get; } = new EmployeeRepository(EmployeesTableConnection);
        private static MeetingRoomRepository MeetingRoomRepository { get; } = new MeetingRoomRepository(SpaceTableConnection);
        
        public static EmployeeService EmployeeService { get; } = new EmployeeService(EmployeeRepository);
        public static MeetingRoomService MeetingRoomService { get; } = new MeetingRoomService(MeetingRoomRepository);
        public static MeetingService MeetingService { get; } = new MeetingService(MeetingRepository, EmployeeService, MeetingRoomService);
        

        public static EmailValidator EmailValidator { get; } = new EmailValidator();
    }
}
