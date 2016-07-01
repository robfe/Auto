using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using Auto.Infrastructure;
using Octokit;

namespace Auto.Core.Viewmodels.DropTargets
{
	public class QuestionDropTarget : PullRequestDropTargetBase
	{
		public QuestionDropTarget():base("Question")
		{
			SetColor("#FFcc317c");
		}

		public override Task Process(string data)
		{
			var p = new PullRequestUrl(data);
			var c = GitClerk.CreateClient();
			return UpdateLabels(c, p, new[] {"ready for review", "ready to land", "comments addressed"}, new[] { "question" });
		}
	}
}
