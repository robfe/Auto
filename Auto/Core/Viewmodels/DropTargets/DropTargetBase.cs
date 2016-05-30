using System.Threading.Tasks;
using System.Windows.Media;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Auto.Core.Viewmodels.DropTargets
{
	public abstract class DropTargetBase : ReactiveObject
	{
		[Reactive]
		public Color Color { get; set; }

		[Reactive]
		public string Title { get; set; }

		[Reactive]
		public bool IsHovered { get; set; }

		public abstract bool CanProcess(string data);
		public abstract Task Process(string data);

		protected void SetColor(string s)
		{
			// ReSharper disable once PossibleNullReferenceException
			Color = (Color)ColorConverter.ConvertFromString(s);
		}
	}
}
