using CommunityToolkit.Mvvm.ComponentModel;
using System.Text;
using Discord.Webhook;
using CommunityToolkit.Mvvm.Input;
using System.Configuration;
using System;
using System.Windows;

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

		public DiscordVM()
		{
			PostToDiscordCommand = new RelayCommand(PostToDiscord);

			// Temporary!
			TextToSend = "This is a test message";
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
            }
			catch (Exception e)
            {
				MessageBox.Show($"Exception: {e.Message}");
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
