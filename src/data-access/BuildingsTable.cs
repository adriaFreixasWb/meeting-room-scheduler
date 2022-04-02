using MeetingRoomScheduler.DAL.Models;

namespace MeetingRoomScheduler.DAL
{
    
    public class BuildingsTable {

        public static BuildingsTable Current { get; } = new BuildingsTable();
        public List<Building> Items { get; } = new List<Building>
        {
            new Building
            {
                Id = 11,
                Name = "Supply office",
            },
            new Building
            {
                Id = 15,
                Name = "Roundup",
            },
            new Building
            {
                Id = 101,
                Name = "Trainning building",
            },
        };

    }
}
