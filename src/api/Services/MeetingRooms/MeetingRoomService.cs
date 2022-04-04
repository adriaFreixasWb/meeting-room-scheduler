using MeetingRoomScheduler.API.Infrastructure.MeetingRooms;
using MeetingRoomScheduler.API.Model;
using MeetingRoomScheduler.DAL.Models;

namespace MeetingRoomScheduler.API.Services.MeetingRooms
{
    public class MeetingRoomService
    {
        public readonly MeetingRoomRepository _meetingRoomRepository;

        public MeetingRoomService(MeetingRoomRepository meetingRoomRepository)
        {
            _meetingRoomRepository = meetingRoomRepository;
        }

        public IEnumerable<Space> Get()
        {
            return _meetingRoomRepository.Get();
        }

        public bool CheckIfExists(string meetingRoom)
        {
            return _meetingRoomRepository.Search(meetingRoom)
                .Any(x => x.Name.Equals(meetingRoom, StringComparison.InvariantCultureIgnoreCase));
        }

        public MeetingRoom GetBy(string name)
        {
            return _meetingRoomRepository.GetBy(name);
        }
    }
}
