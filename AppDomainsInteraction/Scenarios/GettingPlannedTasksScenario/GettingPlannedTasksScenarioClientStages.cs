using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDomainsInteraction.Scenarios.Base;

namespace AppDomainsInteraction.Scenarios.GettingPlannedTasksScenario
{
	public class GettingPlannedTasksScenarioClientStages : StagesBase
	{
		protected override List<string> Stages { get; } = new List<string>()
		{
			"ExportRequesting", //todo to const
			"ExportWaiting",
			"DataFileReceiving",
			"TasksImporting",
			"LogFileReceiving",
			"UserNotifying",
			"Completed",
		};
	}
}
