using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Auto.Core.Viewmodels;
using Auto.Core.Viewmodels.DropTargets;
using ReactiveUI;
using DragEventArgs = System.Windows.DragEventArgs;
using Rectangle = System.Drawing.Rectangle;

namespace Auto
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		MainViewModel _vm;

		public MainWindow()
		{
			InitializeComponent();

			Left = Screen.AllScreens.Select(screen => screen.Bounds)
				.Aggregate(Rectangle.Union)
				.Left;

			DataContext = _vm = new MainViewModel();

			_vm.WhenAnyValue(x => x.State).Subscribe(x => VisualStateManager.GoToElementState(LayoutRoot, x.ToString(), true));
		}

		void MainWindow_OnDragEnter(object sender, DragEventArgs e)
		{
			_vm.HoveredUrl = e.Data.GetData(typeof (string)).ToString();
		}

		void MainWindow_OnDragLeave(object sender, DragEventArgs e)
		{
			_vm.HoveredUrl = null;
		}

		void MainWindow_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			_vm.State = MainState.Settings;
		}

		void UIElement_OnDrop(object sender, DragEventArgs e)
		{
			var s = e.Data.GetData(typeof (string)).ToString();
			var dc = ((FrameworkElement) sender).DataContext as  DropTargetBase;
			_vm.Process(dc, s);
		}

		void MainWindow_OnDrop(object sender, DragEventArgs e)
		{
			_vm.State = MainState.Collapsed;
		}
	}
}
