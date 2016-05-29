using System;
using System.Collections.Generic;
using System.Linq;
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
using DragEventArgs = System.Windows.DragEventArgs;
using Rectangle = System.Drawing.Rectangle;

namespace Auto
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			In.GoToState(LayoutRoot, false);
			Left = Screen.AllScreens.Select(screen => screen.Bounds)
				.Aggregate(Rectangle.Union)
				.Left;
		}

		void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				this.DragMove();
			}
			
		}

		void MainWindow_OnDragEnter(object sender, DragEventArgs e)
		{
			Out.GoToState(LayoutRoot);
		}

		void MainWindow_OnDragLeave(object sender, DragEventArgs e)
		{
			In.GoToState(LayoutRoot);
		}
	}
}
