using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SquadForger.Repository;

namespace SquadForger.ViewModel
{
    public class SquadVM : ObservableObject
    {
        public RelayCommand SelectFileCommand { get; private set; }

        public SquadVM()
        {
            SelectFileCommand = new RelayCommand(ReadTeamsFromCsv);
        }

        private void ReadTeamsFromCsv()
        {
            var teams = CsvChallongeParser.ReadTeamNames();
        }
    }
}