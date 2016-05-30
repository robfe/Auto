using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Auto.Core.Viewmodels;
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

		void MainWindow_OnDrop(object sender, DragEventArgs e)
		{
			_vm.Drop();
		}

		void MainWindow_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			_vm.State = MainState.Settings;
		}
	}
}
