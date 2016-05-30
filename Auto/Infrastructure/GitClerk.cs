using System.Threading.Tasks;
using Auto.Properties;
using Octokit;
using Octokit.Internal;

namespace Auto.Infrastructure
{
	public class GitClerk
	{

		public static Task Approve(string owner, string repo, int ticket)
		{
			return Task.Delay(0);
			var c = new GitHubClient(new ProductHeaderValue("Auto"), new InMemoryCredentialStore(new Credentials(Settings.Default.GithubToken)));
			return Task.WhenAll(
				c.Issue.Labels.RemoveFromIssue(owner, repo, ticket, "ready for review"),
				c.Issue.Labels.AddToIssue(owner, repo, ticket, new[] { "ready to land" }),
				c.Issue.Comment.Create(owner, repo, ticket, "LGTM :rainbow:"));
		}
	}
}
