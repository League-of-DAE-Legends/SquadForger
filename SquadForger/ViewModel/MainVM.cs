using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SquadForger.ViewModel
{
    public class MainVM : ObservableObject
    {
        public string WindowTitle { get; private set; } = $"Squad Forger v0.0.1";
        public RelayCommand OpenGithubRepoCommand { get; private set; }

        public MainVM()
        {
            OpenGithubRepoCommand = new RelayCommand(OpenGithubRepo);
        }

        private void OpenGithubRepo()
        {
            Process.Start(new ProcessStartInfo("https://github.com/League-of-DAE-Legends/SquadForger"));
        }
    }
}