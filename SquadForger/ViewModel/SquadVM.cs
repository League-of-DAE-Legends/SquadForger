using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SquadForger.Model;
using SquadForger.Repository;
using SquadForger.Services;


namespace SquadForger.ViewModel
{
    public class SquadVM : ObservableObject
    {
        public ObservableCollection<Team> Teams { get; private set; } = new ObservableCollection<Team>();
        
        public RelayCommand SelectFileCommand { get; private set; }

        public RelayCommand AddTeamsCommand { get; private set; }
        public RelayCommand ClearTeamsCommand { get; private set; }
        private ITeamNamesRepository TeamNamesRepository { get; set; } = new CSVTeamsParser();
        public string TeamsInput { get; set; }
        public string AmountChampsInput { get; set; } = "15";

        private readonly List<string> _championNames = new List<string>();
        private LeagueVersion _lastVersionUsed;

        public RelayCommand CustomGenerateCommand { get; private set; }
        public string LeagueVersionText { get; set; }
        public RelayCommand SafeGenerateCommand { get; private set; }
        


        private readonly IRandomPicker _randomPicker = new DefaultChampionPicker();

        public SquadVM()
        {
            SelectFileCommand = new RelayCommand(ReadTeamsFromCsv);
            AddTeamsCommand = new RelayCommand(AddTeams);
            ClearTeamsCommand = new RelayCommand(ClearTeams);
            CustomGenerateCommand = new RelayCommand(CustomGenerate);
            SafeGenerateCommand = new RelayCommand(SafeGenerate);

            Teams.CollectionChanged += (s, e) => ServiceLocator.Instance.EventAggregator.Publish(new TeamsUpdatedEvent(Teams.ToList()));
           // GetFallBackChampionNames();
        }

        private void GenerateChampionsPerTeam()
        {
            if (Teams.Count == 0)
            {
                MessageBox.Show("Team count is 0");
                return;
            }
            try
            {
                int amount = int.Parse(AmountChampsInput);
                //Generate champions for each team
                foreach (var team in Teams)
                {
                    team.ChampionNames = _randomPicker.GetRandom(_championNames, amount);
                    team.ChampionNames.Sort();
                }
                MessageBox.Show("Teams generated successfully. Proceed to Discord Tab!");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private async void SafeGenerate()
        {
            await GetFallBackChampionNames();
            GenerateChampionsPerTeam();
            
        }
        private async void CustomGenerate()
        {
            LeagueVersion newVersion;
            try
            {
                newVersion = LeagueVersionText.ParseIntoLeagueVersion();
            }
            catch (Exception)
            {
                MessageBox.Show($"Invalid league version!");
                return;
            }
            await GetChampionNames(newVersion);
            GenerateChampionsPerTeam();
        }

        private async Task GetFallBackChampionNames()
        {
            if (_lastVersionUsed.Season == -1 &&
                _lastVersionUsed.PatchNumber == -1 &&
                _lastVersionUsed.SubpatchNumber == -1)
            {
                return;
            }
            _lastVersionUsed = new LeagueVersion(-1,-1,-1);

            List<string> temp = new List<string>();
            try
            {
                Champions champions = await Champions.GetFallBackAsync();
                if (champions == null)
                {
                    throw new ArgumentNullException("Failed to get champions");
                }
                foreach (KeyValuePair<string,Champion> kvp in champions.data) 
                {
				    temp.Add(kvp.Key);
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show($"Exception: {ex.Message}");
                return;
            }
            _championNames.Clear();
            _championNames.AddRange(temp);
        }

        private async Task GetChampionNames(LeagueVersion leagueVersion)
        {
            if (_lastVersionUsed.Season == leagueVersion.Season && 
                _lastVersionUsed.PatchNumber == leagueVersion.PatchNumber && 
                _lastVersionUsed.SubpatchNumber == leagueVersion.SubpatchNumber) 
            {
                return;
            }
            _lastVersionUsed = leagueVersion;

			List<string> temp = new List<string>();
			try
			{
				Champions champions = await Champions.GetChampionsAsync(_lastVersionUsed);
                if (champions == null)
                {
                    throw new ArgumentNullException("Failed to get champions");
                }
				foreach (KeyValuePair<string, Champion> kvp in champions.data)
				{
					temp.Add(kvp.Key);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Exception: {ex.Message}");
                return;
			}
			_championNames.Clear();
			_championNames.AddRange(temp);
		}

        private void ReadTeamsFromCsv()
        {
            if (!(TeamNamesRepository is CSVTeamsParser csvParser)) return;
            csvParser.ColumnName = "Participant Username";
            
            foreach (Team team in TeamNamesRepository.GetTeams())
            {
                Teams.Add(team);
            }
        }

        private void AddTeams()
        {
            if (TeamsInput == null) return;
            if (!TeamsInput.Any()) return;
            // Split the input by commas and remove empty entries
            List<string> teamNames = TeamsInput.Split(',').Select(s => s.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            // Create Team objects from team names
            foreach (string teamName in teamNames)
            {
                Teams.Add(new Team { TeamName = teamName });
            }
        }

        private void ClearTeams()
        {
            Teams.Clear();
        }

    }
}