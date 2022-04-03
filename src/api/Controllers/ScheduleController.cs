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
            return _meetingService.Get().Select(x => x.StanceName +" "+ string.Join(", ", x.Emails) + " at " + x.Date.ToString());
        }

        [HttpPost]
        public ActionResult Create(StanceAppointmentRequest appointment)
        {
            if(!_emailValidator.AllMatch(appointment.Emails))
            {
                return BadRequest("Email inconsistency");
            }
            if(!_meetingService.CheckAllAssitantsExist(appointment.Emails))
            {
                return BadRequest("Employee not found");
            }
            if(DateTime.Now > appointment.Date)
            {
                return BadRequest("Cannot set meeting at past time");
            }
            if(!_meetingService.CheckMeetingRoomExists(appointment.StanceName))
            {
                return BadRequest("Meeting room does not exist");
            }

            _meetingService.Create(appointment);
            var result = _meetingService.Get().Last();
            return Ok(result);
        }
    }
}
