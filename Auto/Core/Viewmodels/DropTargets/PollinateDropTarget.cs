using System.Threading.Tasks;
using System.Windows.Media;
using Auto.Infrastructure;

namespace Auto.Core.Viewmodels.DropTargets
{
	public class PollinateDropTarget : PullRequestDropTargetBase
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
				UpdateLabels(c, p, new [] { "needs pollinating" }),
				c.Issue.Comment.Create(p.Owner, p.Repo, p.Ticket, ":hibiscus:"));
		}
	}
}
