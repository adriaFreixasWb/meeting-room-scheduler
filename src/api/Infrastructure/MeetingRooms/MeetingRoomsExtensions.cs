using MeetingRoomScheduler.API.Model;
using MeetingRoomScheduler.DAL.Models;

namespace MeetingRoomScheduler.API.Infrastructure.MeetingRooms
{
    public static class MeetingRoomsExtensions
    {
        public static MeetingRoom ToMeetingRoom(this Space space) =>
            new MeetingRoom(space.Id, space.Name);
    }
}
