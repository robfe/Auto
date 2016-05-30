using System.Threading.Tasks;
using System.Windows.Media;
using Auto.Infrastructure;

namespace Auto.Core.Viewmodels.DropTargets
{
	class PollinateDropTarget : PullRequestDropTargetBase
	{
		public PollinateDropTarget():base("Pollinate")
		{
			SetColor("#FFFFFF66");
		}

		public override Task Process(string data)
		{
			var p = new PullRequestUrl(data);
			var c = GitClerk.CreateClient();
			return Task.WhenAll(
				c.Issue.Labels.RemoveFromIssue(p.Owner, p.Repo, p.Ticket, "needs pollinating"),
				c.Issue.Comment.Create(p.Owner, p.Repo, p.Ticket, ":hibiscus:"));
		}
	}
}
