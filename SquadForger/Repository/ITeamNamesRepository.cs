using System.Collections.Generic;
using SquadForger.Model;

namespace SquadForger.Repository
{
	public interface ITeamNamesRepository
	{
		List<Team> GetTeams();
	}
}
