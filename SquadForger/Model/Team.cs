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

            int count = 0; // Initialize a counter for the champions

            foreach (var champion in ChampionNames)
            {
                stringBuilder.Append($"{champion}, ");
                count++; // Increment the counter for each champion

                // Check if the count is a multiple of 5
                if (count % 5 == 0)
                {
                    stringBuilder.AppendLine(); // Append an additional new line after every 5 champions
                }
            }
            
            

            return stringBuilder.ToString();
        }
    }
}