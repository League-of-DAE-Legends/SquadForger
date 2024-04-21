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
        public RelayCommand SquadViewCommand { get; set; }
        public RelayCommand DiscordViewCommand { get; set; }
        public SquadView SquadView { get; private set; } = new SquadView();
        public DiscordView DiscordView { get; private set; } = new DiscordView();

        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set 
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainVM()
        {
            OpenGithubRepoCommand = new RelayCommand(OpenGithubRepo);
            SquadViewCommand = new RelayCommand(GoToSquadView);
            DiscordViewCommand = new RelayCommand(GoToDiscordView);
            CurrentView = SquadView;
        }

        private void OpenGithubRepo()
        {
            Process.Start(new ProcessStartInfo("https://github.com/League-of-DAE-Legends/SquadForger"));
        }

        private void GoToSquadView()
        {
            CurrentView = SquadView;
        }

        private void GoToDiscordView()
        {
            CurrentView = DiscordView;
        }
    }
}