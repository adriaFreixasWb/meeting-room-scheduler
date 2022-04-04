namespace MeetingRoomScheduler.API.Model
{
    public class Meeting
    {
        const int MINIMUM_ATTENDIEES = 2;
        public uint Id { get; private set; }
        public DateTime Date { get; }
        public List<Attendee> Attendiees { get; private set; } = new List<Attendee>();
        public MeetingRoom MeetingRoom { get; private set; }
        public string Title { get; }
        public bool HasMinimumAttendies { get => Attendiees.Count >= MINIMUM_ATTENDIEES; }

        public Meeting(DateTime date, string title)
        {
            Assure(date, title);
            (Date, Title) = (date, title);
        }

        public void AddAttendees(params Attendee[] attendiees)
        {
            Attendiees.AddRange(attendiees.Distinct());
        }

        private void Assure(DateTime date, string title)
        {
            if(date < DateTime.Today)
            {
                throw new ArgumentException("Meeting cannot be set for a past date");
            }
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Meeting must have a title");
            }
        }
        public void UpdateId(uint id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Incorrect Id");
            }
            Id = id;
        }
        public void AddMeetingRoom(MeetingRoom meetingRoom)
        {
            MeetingRoom = meetingRoom;
        }
    }
}
