using System;
using System.Collections.Generic;
using System.ComponentModel;
using AppDomainsInteraction.Contracts;
using AppDomainsInteraction.Scenarios.Base;
using AppDomainsInteraction.Storage.Model;
using Unity;

namespace AppDomainsInteraction.Scenarios.GettingPlannedTasksScenario
{
	public class GettingPlannedTasksScenarioClient : ScenarioBase<GettingPlannedTasksScenarioClientStages>
	{
		public GettingPlannedTasksScenarioClient(IUnityContainer container) : base(container)
		{
		}

		public override void Initialize(SyncAgentTask syncAgentTask)
		{
			base.Initialize(syncAgentTask);

			CurrentStage.Current = "ExportRequesting";
			//todo read state
		}

		public override void ExecuteCurrentStage()
		{
			base.ExecuteCurrentStage();

			CurrentStageExecutor.Execute();
		}
	}
}
