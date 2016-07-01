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
		readonly List<DropTargetBase> _allTargets;

		readonly ReactiveList<DropTargetBase> _dropTargets = new ReactiveList<DropTargetBase>();

		public MainViewModel()
		{
			GithubToken = Settings.Default.GithubToken;

			_allTargets = GetType().Assembly.GetExportedTypes()
				.Where(t => !t.IsAbstract)
				.Where(t => typeof (DropTargetBase).IsAssignableFrom(t))
				.OrderBy(x => x.Name)
				.Select(t => (DropTargetBase) Activator.CreateInstance(t))
				.ToList();


			this.WhenAnyValue(x => x.HoveredUrl).Subscribe(x =>
			{
				using (_dropTargets.SuppressChangeNotifications())
				{
					_dropTargets.Clear();
					_dropTargets.AddRange(_allTargets.Where(t => t.CanProcess(x)));
				}
				State = DropTargets.Any() ? MainState.Expanded : MainState.Collapsed;

			});

			Quit = ReactiveCommand.CreateAsyncTask(x =>
			{
				Application.Current.Shutdown();
				return Task.FromResult(Unit.Default);
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
		public ReactiveCommand<Unit> Quit { get; }

		public ICollection<DropTargetBase> DropTargets => _dropTargets;

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
