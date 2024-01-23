using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SquadForger.Repository;

namespace SquadForger.ViewModel
{
    public class SquadVM : ObservableObject
    {
        public RelayCommand SelectFileCommand { get; private set; }
        private ITeamNamesRepository TeamNamesRepository { get; set; } = new CSVTeamsParser();

        public SquadVM()
        {
            SelectFileCommand = new RelayCommand(ReadTeamsFromCsv);
        }

        private void ReadTeamsFromCsv()
        {
            var teams = TeamNamesRepository.GetTeams("Participant Username");
        }
    }
}