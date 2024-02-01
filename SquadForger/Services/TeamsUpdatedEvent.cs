using System.Collections.Generic;
using System.Collections.ObjectModel;
using SquadForger.Model;

namespace SquadForger.Services
{
    public class TeamsUpdatedEvent
    {
        public List<Team> Teams { get; }

        public TeamsUpdatedEvent(List<Team> teams)
        {
            Teams = teams;
        }
    }
}