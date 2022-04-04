﻿namespace MeetingRoomScheduler.Model
{
    public class MeetingRoomSchedule
    {
        public DateTime Date { get; set; }
        public IEnumerable<string> Emails { get; set; }
        public string MeetingRoom { get; set; }
        public string Title { get; internal set; }
    }
}
