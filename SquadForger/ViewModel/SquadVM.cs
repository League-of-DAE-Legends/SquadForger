using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SquadForger.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SquadForger.ViewModel
{
    public class SquadVM : ObservableObject
    {
        private readonly List<string> _championNames = new List<string>();
        private LeagueVersion _lastVersionUsed;

        public RelayCommand CustomGenerateCommand { get; private set; }
        public string LeagueVersionText { get; set; } = "Enter valid season and patch (ie 14.1.1)";

        public SquadVM()
        {
            CustomGenerateCommand = new RelayCommand(CustomGenerate);

            GetFallBackChampionNames();
        }

        private void CustomGenerate()
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
            GetChampionNames(newVersion);
        }

        private async void GetFallBackChampionNames()
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

        private async void GetChampionNames(LeagueVersion leagueVersion)
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
    }
}