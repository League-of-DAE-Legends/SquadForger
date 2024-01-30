using System;
using System.Text.RegularExpressions;

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
        public LeagueVersion(LeagueVersion other) : this(other.Season,other.PatchNumber,other.SubpatchNumber)
        {
        }

		public int Season;
		public int PatchNumber;
		public int SubpatchNumber;
	}
	public static class LeagueVersionExtensions
    {
        public static LeagueVersion ParseIntoLeagueVersion(this string versionText)
        {
            // Source: ChatGPT

            // Define a regular expression pattern for x.y.z
            string pattern = @"^(\d+)\.(\d+)\.(\d+)$";

            // Use Regex to match the pattern
            Match match = Regex.Match(versionText, pattern);

            if (match.Success)
            {
                // Extract the matched integers
                int season = int.Parse(match.Groups[1].Value);
                int patch = int.Parse(match.Groups[2].Value);
                int subpatch = int.Parse(match.Groups[3].Value);

                return new LeagueVersion(season, patch, subpatch);
            }
            else
            {
                throw new FormatException("Invalid version format");
            }
        }
    }
}
