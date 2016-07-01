using System.Threading.Tasks;
using System.Windows.Media;
using Auto.Infrastructure;

namespace Auto.Core.Viewmodels.DropTargets
{
	public class ApproveDropTarget : PullRequestDropTargetBase
	{
		public ApproveDropTarget():base("Approve")
		{
			SetColor("#FFbfe5bf");
		}

		public override Task Process(string data)
		{
			var p = new PullRequestUrl(data);
			var c = GitClerk.CreateClient();
			return Task.WhenAll(
				UpdateLabels(c, p, new[] { "ready for review", "question" }, new[] { "ready to land" }),
				c.Issue.Comment.Create(p.Owner, p.Repo, p.Ticket, "LGTM :rainbow:"));
		}
	}
}
