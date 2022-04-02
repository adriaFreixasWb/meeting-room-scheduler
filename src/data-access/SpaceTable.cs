using MeetingRoomScheduler.DAL.Models;

namespace MeetingRoomScheduler.DAL
{
    public class SpaceTable
    {
        public List<Space> Items { get; } = new List<Space>
        {
            new Space
            { 
                Id = 2,
                Name = "SO Conference room",
                BuildingId = 11
            },
            new Space
            {
                Id = 3,
                Name = "RO Conference room",
                BuildingId = 15
            },
            new Space
            {
                Id = 7,
                Name = "RO Meeting Room",
                BuildingId = 15
            },
            new Space
            {
                Id = 8,
                Name = "RO Meeting Room 2",
                BuildingId = 15
            },
            new Space
            {
                Id = 11,
                Name = "TB Conference room",
                BuildingId = 101
            },
            new Space
            {
                Id = 12,
                Name = "RO Meeting Room 1",
                BuildingId = 101
            },
            new Space
            {
                Id = 15,
                Name = "Glass infinity view",
                BuildingId = 101
            },

        };

    }
}
