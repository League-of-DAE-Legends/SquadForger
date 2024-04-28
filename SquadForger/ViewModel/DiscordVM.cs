using CommunityToolkit.Mvvm.ComponentModel;
using System.Text;
using Discord.Webhook;
using CommunityToolkit.Mvvm.Input;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using SquadForger.Model;
using SquadForger.Services;

namespace SquadForger.ViewModel
{
	public class DiscordVM : ObservableObject
	{
		private string _textToSend = "<empty>";
		public string TextToSend
		{
			get => _textToSend;
			private set { _textToSend = value; OnPropertyChanged(nameof(TextToSend)); }
		}
		public RelayCommand PostToDiscordCommand { get; private set; }
		public RelayCommand UpdatePreviewCommand { get; private set; }
		public List<Team> Teams { get; private set; } = new List<Team>();
		public DiscordVM()
		{
			PostToDiscordCommand = new RelayCommand(PostToDiscord);
			UpdatePreviewCommand = new RelayCommand(UpdatePreview);
			
			ServiceLocator.Instance.EventAggregator.Subscribe<TeamsUpdatedEvent>(OnTeamsUpdated);
		}
		private void OnTeamsUpdated(TeamsUpdatedEvent eventArgs)
		{
			Teams = eventArgs.Teams;
		}

		private void UpdatePreview()
		{
			var stringBuilder = new StringBuilder();

			foreach (var team in Teams)
			{
				stringBuilder.Append(team.FormatTeamInfo());
				stringBuilder.AppendLine(); // Add an extra line between teams
			}

			TextToSend = stringBuilder.ToString();
		}
		private async void PostToDiscord()
		{
			if (TextToSend.Equals("<empty>"))
			{
				return;
			}
			
			try
            {
				string webhooklink = ConfigurationManager.AppSettings.Get("dev_webhook");
				DiscordWebhookClient webhook = new DiscordWebhookClient(webhooklink);
				await webhook.SendMessageAsync(TextToSend);
				MessageBox.Show("Teams posted to discord successfully!");
            }
			catch (Exception e)
            {
                MessageBox.Show($"Exception: {e.Message}\nCouldn't post to discord");
            }
		}

		private static string GetLongTestText(int lines = 30)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < lines; ++i)
			{
				sb.AppendLine("This is a very long sentence just to fill some space. Lorem ispum or something I don't know.");
			}
			return sb.ToString();
		}
	}
}
