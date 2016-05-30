using System.Text.RegularExpressions;

namespace Auto.Infrastructure
{
	public class PullRequestUrl
	{
		static Regex Pattern = new Regex(@"https://github.com/(\w+)/(\w+)/pull/(\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public PullRequestUrl(string s)
		{
			var match = Pattern.Match(s??"");
			if (match.Success)
			{
				Success = true;
				Owner = match.Groups[1].Value;
				Repo = match.Groups[2].Value;
				Ticket = int.Parse(match.Groups[3].Value);
			}
		}

		public string Owner { get; }
		public string Repo { get; }
		public int Ticket { get; }
		public bool Success { get; }
	}
}
