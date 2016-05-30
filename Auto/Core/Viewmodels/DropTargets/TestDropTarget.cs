using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Auto.Core.Viewmodels.DropTargets
{
	class TestDropTarget : PullRequestDropTargetBase
	{
		public TestDropTarget():base("Test")
		{
			SetColor("#FFFF8800");
		}

		public override Task Process(string data)
		{
			MessageBox.Show(data, "Test drop:");
			return Task.Delay(500);
		}
	}
}
