﻿using MeetingRoomScheduler.DAL;
using MeetingRoomScheduler.DAL.Models;

namespace MeetingRoomScheduler.API.Infrastructure.Stances
{
    public class MeetingRoomRepository
    {
        private readonly SpaceTable _spaceTable;

        public MeetingRoomRepository(SpaceTable spaceTable)
        {
            _spaceTable = spaceTable;
        }

        public IEnumerable<Space> Get()
        {
            return _spaceTable.Items;
        }
        public IEnumerable<Space> Search(string name)
        {
            return _spaceTable.Items.Where(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}