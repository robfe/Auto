using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows;
using Auto.Core.Viewmodels.DropTargets;
using Auto.Infrastructure;
using Auto.Properties;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Auto.Core.Viewmodels
{
	public class MainViewModel : ReactiveObject
	{
		readonly DropTargetBase[] _targets =
		{
			new TestDropTarget(),
			new PollinateDropTarget(),
			new QuestionDropTarget(),
			new ApproveDropTarget(),
		};
		readonly ReactiveList<DropTargetBase> _dropTargets = new ReactiveList<DropTargetBase>();

		public MainViewModel()
		{
			GithubToken = Settings.Default.GithubToken;

			this.WhenAnyValue(x => x.HoveredUrl).Subscribe(x =>
			{
				using (_dropTargets.SuppressChangeNotifications())
				{
					_dropTargets.Clear();
					_dropTargets.AddRange(_targets.Where(t => t.CanProcess(x)));
				}
				State = DropTargets.Any() ? MainState.Expanded : MainState.Collapsed;

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
		public string GithubToken { get; set; }

		public ReactiveCommand<Unit> SaveSettings { get; }

		public ICollection<DropTargetBase> DropTargets
		{
			get { return _dropTargets; }
		}

		public async void Process(DropTargetBase dc, string s)
		{
			_dropTargets.Clear();
			try
			{
				if (dc != null)
				{
					await dc.Process(s);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error " + ex.Message);
			}
			State = MainState.Collapsed;
		}
	}

	public enum MainState
	{
		Collapsed, Expanded, Settings
	}
}
