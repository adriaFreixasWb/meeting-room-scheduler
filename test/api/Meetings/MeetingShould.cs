using MeetingRoomScheduler.API.Model;
using System;
using Xunit;

namespace MeetingRoomScheduler.API.Test.Meetings
{
    public class MeetingShould
    {
        public const int ID = 1;
        public const string MEETING_TITLE = "Meeting";
        public readonly Attendee FIRST_ATTENDEE = new Attendee(1, "John Doe", "jdoe@people.tk");
        public readonly Attendee SECOND_ATTENDEE = new Attendee(1, "Priscila Moor", "pmoor@people.tk");

        [Fact]
        public void HaveTitle()
        {
            var emptyTitle = string.Empty;
            Assert.Throws<ArgumentException>(() => new Meeting(DateTime.Now, emptyTitle));
        }

        [Fact]
        public void NotBeInThePast()
        {
            var yesterday = DateTime.Today.AddDays(-1);
            Assert.Throws<ArgumentException>(() => new Meeting(yesterday, MEETING_TITLE));
        }

        [Fact]
        public void HavePositiveIde()
        {
            uint wrongId = 0;
            var sut = new Meeting(DateTime.Now, MEETING_TITLE);
            Assert.Throws<ArgumentException>(() => sut.UpdateId(wrongId));
        }

        [Fact]
        public void NotHaveNoAttendies()
        {
            var sut = new Meeting(DateTime.Now, MEETING_TITLE);
            sut.UpdateId(ID);
            Assert.False(sut.HasMinimumAttendies);
        }

        [Fact]
        public void NotHaveOnlyOneAttendies()
        {
            var sut = new Meeting(DateTime.Now, MEETING_TITLE);
            sut.UpdateId(ID);
            sut.AddAttendees(FIRST_ATTENDEE);
            Assert.False(sut.HasMinimumAttendies);
        }

        [Fact]
        public void HaveTwoAttendiesMinimum()
        {
            var sut = new Meeting(DateTime.Now, MEETING_TITLE);
            sut.UpdateId(ID);
            sut.AddAttendees(FIRST_ATTENDEE, SECOND_ATTENDEE);
            Assert.True(sut.HasMinimumAttendies);
        }

        [Fact]
        public void NotHaveRepetedAttendies()
        {
            var expected = 2;
            var sut = new Meeting(DateTime.Now, MEETING_TITLE);
            sut.UpdateId(ID);
            sut.AddAttendees(FIRST_ATTENDEE, FIRST_ATTENDEE, SECOND_ATTENDEE);
            Assert.Equal(expected, sut.Attendiees.Count);
        }
    }
}

