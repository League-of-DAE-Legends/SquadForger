using System.Collections.Generic;

namespace SquadForger.Repository
{
	public interface ITeamNamesRepository
	{
		List<string> GetTeamNames();
	}
}
