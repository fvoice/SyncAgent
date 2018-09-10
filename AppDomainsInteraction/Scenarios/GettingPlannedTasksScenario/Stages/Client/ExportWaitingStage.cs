using System;
using AppDomainsInteraction.Contracts;
using NLog;

namespace AppDomainsInteraction.Scenarios.GettingPlannedTasksScenario.Stages.Client
{
	public class ExportWaitingStage : ISyncAgentScenarioStage
	{
		readonly Logger _logger = LogManager.GetCurrentClassLogger();

		public void Execute()
		{
			_logger.Info($"{nameof(ExportRequestingStage)} is in progress - we are waiting for specific answer from server");
			_logger.Info($"{nameof(ExportRequestingStage)} until answer is not applicable - stage is not passed - throw an Exception");

			throw new Exception("Not applicable answer");
		}
	}
}
