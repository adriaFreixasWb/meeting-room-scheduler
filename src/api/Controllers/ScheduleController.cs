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
        public ActionResult Create(MeetingRequest meeting)
        {
            if(!_emailValidator.AllMatch(meeting.Emails))
            {
                return BadRequest("Email inconsistency");
            }
            if(!_meetingService.CheckAllAssitantsExist(meeting.Emails))
            {
                return BadRequest("Employee not found");
            }
            if(DateTime.Now > meeting.Date)
            {
                return BadRequest("Cannot set meeting at past time");
            }
            if(!_meetingService.CheckMeetingRoomExists(meeting.MeetingRoom))
            {
                return BadRequest("Meeting room does not exist");
            }

            _meetingService.Create(meeting);
            var result = _meetingService.Get().Last();
            return Ok(result);
        }
    }
}
