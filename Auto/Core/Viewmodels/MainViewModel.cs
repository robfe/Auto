using System;
using System.Reactive;
using System.Threading.Tasks;
using Auto.Infrastructure;
using Auto.Properties;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Auto.Core.Viewmodels
{
	public class MainViewModel : ReactiveObject
	{
		public MainViewModel()
		{
			GithubToken = Settings.Default.GithubToken;

			this.WhenAnyValue(x => x.HoveredUrl).Subscribe(x =>
			{
				var m = new PullRequestUrl(x);
				if (m.Success)
				{
					State = MainState.Expanded;
					TicketNumber = m.Ticket.ToString();
				}
				else
				{
					State = MainState.Collapsed;
					TicketNumber = "";
				}
			});

			SaveSettings = ReactiveCommand.CreateAsyncTask(x =>
			{
				Settings.Default.GithubToken = GithubToken;
				Settings.Default.Save();
				State = MainState.Collapsed;
				return Task.FromResult(Unit.Default);
			});
		}

		[Reactive]
		public string HoveredUrl { get; set; }

		[Reactive]
		public MainState State { get; set; }

		[Reactive]
		public string TicketNumber { get; set; }

		[Reactive]
		public string GithubToken { get; set; }

		public ReactiveCommand<Unit> SaveSettings { get; }

		public void Drop()
		{
			if (State == MainState.Expanded)
			{
				var p = new PullRequestUrl(HoveredUrl);
				GitClerk.Approve(p.Org, p.Repo, p.Ticket).ContinueWith(x => Console.WriteLine(x.Status));
			}
			HoveredUrl = null;
		}
	}

	public enum MainState
	{
		Collapsed, Expanded, Settings
	}
}
