using System;
using System.Linq;
using System.Threading.Tasks;
using Auto.Infrastructure;
using Octokit;

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

		protected async Task UpdateLabels(GitHubClient c, PullRequestUrl p, string[] removes = null, string[] adds = null)
		{
			var v = await c.Issue.Labels.GetAllForIssue(p.Owner, p.Repo, p.Ticket);
			var comparer = StringComparer.InvariantCultureIgnoreCase;
			var labelNames = v.Select(x => x.Name).ToList();

			var actualRemoves = removes.EmptyIfNull().Intersect(labelNames, comparer);
			foreach (var l in actualRemoves)
			{
				await c.Issue.Labels.RemoveFromIssue(p.Owner, p.Repo, p.Ticket, l);
			}

			var newLabels = adds.EmptyIfNull().Except(labelNames, comparer).ToArray();
			await c.Issue.Labels.AddToIssue(p.Owner, p.Repo, p.Ticket, newLabels);

		}
	}
}
