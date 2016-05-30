using Auto.Infrastructure;

namespace Auto.Core.Viewmodels.DropTargets
{
	public abstract class PullRequestDropTargetBase : DropTargetBase
	{
		protected PullRequestDropTargetBase(string name)
		{
			Name = name;
		}

		public string Name { get; }

		public override bool CanProcess(string data)
		{
			var p = new PullRequestUrl(data);
			if (p.Success)
			{
				Title = $"{Name} {p.Ticket}";
				return true;
			}
			return false;
		}
	}
}