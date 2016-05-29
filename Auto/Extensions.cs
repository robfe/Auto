using System.Windows;

namespace Auto
{
	public static class Extensions
	{
		public static void GoToState(this VisualState s, FrameworkElement root, bool useTransitions = true)
		{
			VisualStateManager.GoToElementState(root, s.Name, useTransitions);
		} 
	}
}