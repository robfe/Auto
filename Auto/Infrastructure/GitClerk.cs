using System.Threading.Tasks;
using Auto.Properties;
using Octokit;
using Octokit.Internal;

namespace Auto.Infrastructure
{
	public class GitClerk
	{
		public static GitHubClient CreateClient()
		{
			return new GitHubClient(new ProductHeaderValue("Auto"), new InMemoryCredentialStore(new Credentials(Settings.Default.GithubToken)));
		}
	}
}
