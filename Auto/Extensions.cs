using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Auto
{
	public static class Extensions
	{
		public static void GoToState(this VisualState s, FrameworkElement root, bool useTransitions = true)
		{
			VisualStateManager.GoToElementState(root, s.Name, useTransitions);
		}

		public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> t)
		{
			return t ?? Enumerable.Empty<T>();
		}
	}
}
