using CommunityToolkit.Mvvm.ComponentModel;
using System.Text;
using Discord.Webhook;
using System.Resources;
using CommunityToolkit.Mvvm.Input;

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
			TextToSend = GetLongTestText();
		}

		private async void PostToDiscord()
		{
			if (TextToSend.Equals("<empty>"))
			{
				return;
			}
			
			string webhooklink = "Extract from PrivateData.config";
			DiscordWebhookClient webhook = new DiscordWebhookClient(webhooklink);
			await webhook.SendMessageAsync(TextToSend);
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
