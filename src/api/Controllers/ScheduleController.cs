using MeetingRoomScheduler.API.Model;
using MeetingRoomScheduler.API.Services;
using MeetingRoomScheduler.API.Validators;
using Microsoft.AspNetCore.Mvc;

namespace MeetingRoomScheduler.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ScheduleController : ControllerBase
    {
        private static readonly MeetingService _meetingService = ServiceLocator.MeetingService;
        private static readonly EmailValidator _emailValidator = ServiceLocator.EmailValidator;

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _meetingService.Get().Select(x => x.MeetingRoom +" "+ string.Join(", ", x.Attendiees.Select(x => x.Email)) + " at " + x.Date.ToString());
        }

        [HttpPost]
        public ActionResult Create(MeetingRequest request)
        {
            if (!_emailValidator.AllMatch(request.Emails))
            {
                return BadRequest("Email inconsistency");
            }
            if (!_meetingService.CheckAllAssitantsExist(request.Emails))
            {
                return BadRequest("Employee not found");
            }
            if (DateTime.Now > request.Date)
            {
                return BadRequest("Cannot set meeting at past time");
            }
            if (!_meetingService.CheckMeetingRoomExists(request.MeetingRoom))
            {
                return BadRequest("Meeting room does not exist");
            }

            var meeting = _meetingService.Create(request);
            var result = new MeetingResponse
            {
                Date = meeting.Date,
                MeetingRoom = meeting.MeetingRoom.Name,
                Title = meeting.Title,
                Emails = meeting.Attendiees.Select(x => x.Email)
            };
            return Ok(result);
        }
    }
}
