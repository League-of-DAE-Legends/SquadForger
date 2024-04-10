using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SquadForger.View;

namespace SquadForger.ViewModel
{
    public class MainVM : ObservableObject
    {
        public string WindowTitle { get; private set; } = $"Squad Forger v1.0.1";
        public RelayCommand OpenGithubRepoCommand { get; private set; }
        public SquadView SquadPage { get; private set; } = new SquadView();
        public DiscordView DiscordPage { get; private set; } = new DiscordView();

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