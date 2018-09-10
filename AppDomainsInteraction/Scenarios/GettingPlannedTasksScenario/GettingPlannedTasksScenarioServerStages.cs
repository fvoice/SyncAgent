using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDomainsInteraction.Scenarios.Base;

namespace AppDomainsInteraction.Scenarios.GettingPlannedTasksScenario
{
	public class GettingPlannedTasksScenarioServerStages : StagesBase
	{
		protected override List<string> Stages { get; } = new List<string>()
		{
			"TasksExporting",
			"Completed"
		};
	}
}
