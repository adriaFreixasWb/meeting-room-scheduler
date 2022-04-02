using MeetingRoomScheduler.DAL.Models;
using System.Globalization;

namespace MeetingRoomScheduler.DAL
{
    public class RSVPsTable
    {
        public static RSVPsTable Current { get; } = new RSVPsTable();
        public List<RSVP> Items { get; } = new List<RSVP> 
        { 
            new RSVP
            { 
                Id = 1,
                Time = DateTime.ParseExact("2022-12-02","yyyy-MM-dd", CultureInfo.InvariantCulture),
                Title = "Christmas party planning",
                SpaceId = 12

            }
        };
    }
}
