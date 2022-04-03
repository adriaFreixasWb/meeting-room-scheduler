namespace MeetingRoomScheduler.API.Model
{
    public class Meeting
    {
        const int MINIMUM_ATTENDIEES = 2;
        public uint Id { get; }
        public DateTime Date { get; }
        public List<Attendiee> Attendiees { get; private set; } = new List<Attendiee>();
        public MeetingRoom MeetingRoom { get; private set; }
        public string Title { get; }
        public bool HasMinimumAttendies { get => Attendiees.Count >= MINIMUM_ATTENDIEES; }

        public Meeting(uint id, DateTime date, string title)
        {
            Assure(id, date, title);
            (Id, Date, Title) = (id, date, title);
        }

        public void AddAttendies(params Attendiee[] attendiees)
        {
            Attendiees.AddRange(attendiees.Distinct());
        }

        private void Assure(uint id, DateTime date, string title)
        {
            if(id == 0)
            {
                throw new ArgumentException("Incorrect Id");
            }
            if(date < DateTime.Today)
            {
                throw new ArgumentException("Meeting cannot be set for a past date");
            }
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Meeting must have a title");
            }
        }

        internal void AddMeetingRoom(MeetingRoom meetingRoom)
        {
            throw new NotImplementedException();
        }
    }
}
