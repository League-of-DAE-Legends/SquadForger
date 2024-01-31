using System.Collections.Generic;
using System.Text;

namespace SquadForger.Model
{
    public class Team
    {
        public string TeamName { get; set; }
        public List<string> ChampionNames = new List<string>();
        
        public string FormatTeamInfo()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"# **{TeamName}:**");

            foreach (var champion in ChampionNames)
            {
                stringBuilder.AppendLine($"- {champion}");
            }

            return stringBuilder.ToString();
        }
    }
}