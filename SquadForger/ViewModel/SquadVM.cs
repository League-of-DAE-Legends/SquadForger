using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SquadForger.Model;
using SquadForger.Repository;

namespace SquadForger.ViewModel
{
    public class SquadVM : ObservableObject
    {
        public ObservableCollection<Team> Teams { get; set; } = new ObservableCollection<Team>();
        
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
            foreach (var team in TeamNamesRepository.GetTeams())
            {
                Teams.Add(team);
            }
        }

        private void AddTeams()
        {
            // Split the input by commas and remove empty entries
            List<string> teamNames = TeamsInput.Split(',').Select(s => s.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            // Create Team objects from team names
            foreach (string teamName in teamNames)
            {
                var temp = new Team{ TeamName = teamName };
                var t = temp;
                Teams.Add(temp);
            }
            
        }
    }
}