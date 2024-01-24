using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadForger.Model
{
	public struct LeagueVersion
	{
		public LeagueVersion(int season, int patchNumber, int subpatchNumber = 1)
		{
			Season = season;
			PatchNumber = patchNumber;
			SubpatchNumber = subpatchNumber;
		}

		public int Season;
		public int PatchNumber;
		public int SubpatchNumber;
	}
}
