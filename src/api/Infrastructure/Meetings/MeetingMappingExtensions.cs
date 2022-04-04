using MeetingRoomScheduler.API.Infrastructure.MeetingRooms;
using MeetingRoomScheduler.API.Model;
using MeetingRoomScheduler.DAL.Models;

namespace MeetingRoomScheduler.API.Infrastructure.Meetings
{
    public static class MeetingMappingExtensions
    {
        public static Meeting ToMeeting(this RSVP rsvp)
        {
            var meeting = new Meeting(rsvp.Time, rsvp.Title);
            meeting.UpdateId((uint)rsvp.Id);
            return meeting;
        }
        public static Meeting ToMeetingWithRoom(this RSVP rsvp, Space space)
        {
            var meeting = rsvp.ToMeeting();
            meeting.AddMeetingRoom(space.ToMeetingRoom());
            return meeting;
        }

        public static List<Meeting> AssociateAttendeesToMeeting(this List<Meeting> meetings, Dictionary<uint, IEnumerable<Attendee>> attendieeGroups)
        {
            foreach (var group in attendieeGroups)
            {
                var meeting = meetings.FirstOrDefault(x => x.Id == group.Key);
                if(meeting != null)
                {
                    meeting.AddAttendees(group.Value.ToArray());
                }
            }
            return meetings;
        }

    }
}
