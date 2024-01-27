using CommunityToolkit.Mvvm.ComponentModel;
using SquadForger.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SquadForger.ViewModel
{
    public class SquadVM : ObservableObject
    {
        private readonly List<string> _championNames = new List<string>();
        private LeagueVersion _lastVersionUsed = new LeagueVersion(14,1);

        public SquadVM()
        {
            GetFallBackChampionNames();
        }

        private async void GetFallBackChampionNames()
        {
            if (_lastVersionUsed.Season == -1 &&
                _lastVersionUsed.PatchNumber == -1 &&
                _lastVersionUsed.SubpatchNumber == -1)
            {
                return;
            }
            _lastVersionUsed.Season = -1;
            _lastVersionUsed.PatchNumber = -1;
            _lastVersionUsed.SubpatchNumber = -1;

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

        private async void GetChampionNames(int season, int patchNumber, int subPatchNumber = 1)
        {
            if (_lastVersionUsed.Season == season && 
                _lastVersionUsed.PatchNumber == patchNumber && 
                _lastVersionUsed.SubpatchNumber == subPatchNumber) 
            {
                return;
            }
			_lastVersionUsed.Season = season;
            _lastVersionUsed.PatchNumber = patchNumber;
            _lastVersionUsed.SubpatchNumber = subPatchNumber;
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