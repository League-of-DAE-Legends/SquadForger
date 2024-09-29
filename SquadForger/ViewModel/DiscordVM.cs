using CommunityToolkit.Mvvm.ComponentModel;
using System.Text;
using Discord.Webhook;
using CommunityToolkit.Mvvm.Input;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Xml;
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
		public RelayCommand UpdateWebhookIDCommand { get; private set; }
		public List<Team> Teams { get; private set; } = new List<Team>();
		public string WebhookID { get; set; } = "";
		public DiscordVM()
		{
			PostToDiscordCommand = new RelayCommand(PostToDiscord);
			UpdatePreviewCommand = new RelayCommand(UpdatePreview);
			UpdateWebhookIDCommand = new RelayCommand(UpdateWebhookID);
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

		private void UpdateWebhookID()
		{
			if (string.IsNullOrEmpty(WebhookID))
			{
				MessageBox.Show("Webhook ID is empty!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			
			string discordWebhookPattern = @"^https:\/\/discord\.com\/api\/webhooks\/\d+\/[A-Za-z0-9_-]+$";
			if (!Regex.IsMatch(WebhookID, discordWebhookPattern))
			{
				MessageBox.Show("Invalid Webhook ID format. Please provide a valid Discord Webhook URL.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			UpdateWebhookInConfig(WebhookID);
		}

		private void UpdateWebhookInConfig( string newWebhookID)
		{
			string configFilePath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

			// Step 1: Load the app.config file
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(configFilePath);

			// Find the 'dev_webhook' node
			XmlNode webhookNode = xmlDoc.SelectSingleNode("//appSettings/add[@key='dev_webhook']");

			if (webhookNode != null)
			{
				// Update the webhook ID value
				webhookNode.Attributes["value"].Value = newWebhookID;

				// Save the updated config file
				xmlDoc.Save(configFilePath);
				ConfigurationManager.RefreshSection("appSettings");
				MessageBox.Show("Webhook ID updated successfully in app.config!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			else
			{
				MessageBox.Show("Webhook ID key not found in app.config.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
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
