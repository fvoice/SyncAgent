using System;
using System.Threading;
using AppDomainsInteraction.Contracts;
using NLog;

namespace AppDomainsInteraction.Scenarios.GettingPlannedTasksScenario.Stages.Client
{
	public class ExportRequestingStage : ISyncAgentScenarioStage
	{
		readonly Logger _logger = LogManager.GetCurrentClassLogger();

		public void Execute()
		{
			_logger.Info($"{nameof(ExportRequestingStage)} is in progress - put your code here");
			_logger.Info($"{nameof(ExportRequestingStage)} {AppDomain.CurrentDomain.FriendlyName}");
			//Thread.Sleep(5000);
			_logger.Info($"{nameof(ExportRequestingStage)} completed");
		}
	}
}
