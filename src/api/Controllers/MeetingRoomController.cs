using MeetingRoomScheduler.API.Infrastructure.MeetingRooms;
using MeetingRoomScheduler.API.Services;
using MeetingRoomScheduler.API.Services.MeetingRooms;
using Microsoft.AspNetCore.Mvc;

namespace MeetingRoomScheduler.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeetingRoomController : ControllerBase
    {
        private readonly MeetingRoomService _meetingRoomService = ServiceLocator.MeetingRoomService;
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _meetingRoomService.Get().Select(x=>x.Name);
        }
    }
}
