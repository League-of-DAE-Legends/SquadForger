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
            List<string> temp = new List<string>();
            try
            {
                Champions champions = await Champions.GetFallBackAsync();
                foreach (KeyValuePair<string,Champion> kvp in champions.data) 
                {
				    temp.Add(kvp.Key);
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show($"Exception: {ex.Message}");
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
				foreach (KeyValuePair<string, Champion> kvp in champions.data)
				{
					temp.Add(kvp.Key);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Exception: {ex.Message}");
			}
			_championNames.Clear();
			_championNames.AddRange(temp);
		}
    }
}