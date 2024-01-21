using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SquadForger.Model;
using SquadForger.Repository;

namespace SquadForger.ViewModel
{
    public class SquadVM : ObservableObject
    {
        public List<Team> Teams { get; set; } = new List<Team>();
        
        public RelayCommand SelectFileCommand { get; set; }
        public RelayCommand AddTeamsCommand { get; set; }
        private ITeamNamesRepository TeamNamesRepository { get; set; } = new CsvChallongeParser();
        
        public string TeamsInput { get; set; }

        public SquadVM()
        {
            SelectFileCommand = new RelayCommand(ReadTeamsFromCsv);
            AddTeamsCommand = new RelayCommand(AddTeams);
            TeamsInput = "Enter team names, separated by commas";
        }

        private void ReadTeamsFromCsv()
        {
            Teams.AddRange(TeamNamesRepository.GetTeams());
        }

        private void AddTeams()
        {
            // Split the input by commas and remove empty entries
            List<string> teamNames = TeamsInput.Split(',').Select(s => s.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            // Create Team objects from team names
            foreach (string teamName in teamNames)
            {
                Teams.Add(new Team { TeamName = teamName });
            }
            OnPropertyChanged(nameof(Teams));
        }
    }
}