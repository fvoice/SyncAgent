using AppDomainsInteraction.Contracts;
using AppDomainsInteraction.Isolation;
using AppDomainsInteraction.Scenarios;
using AppDomainsInteraction.Scenarios.GettingPlannedTasksScenario;
using AppDomainsInteraction.Scenarios.GettingPlannedTasksScenario.Stages.Client;
using AppDomainsInteraction.Scheduler;
using AppDomainsInteraction.Scheduler.Jobs;
using AppDomainsInteraction.Storage;
using Quartz;
using Unity;
using Unity.Extension;
using Unity.Lifetime;

namespace AppDomainsInteraction.Extensions.DI
{
	public class SyncAgentUnityExtension : UnityContainerExtension
	{
		protected override void Initialize()
		{
			//scheduler
			Container.RegisterType<ISyncAgentScheduler, SyncAgentScheduler>(new ContainerControlledLifetimeManager());
			Container.RegisterType<ISchedulerListener, SyncAgentSchedulerListener>();

			//schecduler jobs
			Container.RegisterType<ISyncAgentJob, JobSchedulerJob>(SyncAgentTaskType.JobScheduler.ToString());
			Container.RegisterType<ISyncAgentJob, IsolatedWorkExecutorJob>(SyncAgentTaskType.IsolatedWorkExecutor.ToString());
			
			//scenarios
			Container.RegisterType<ISyncAgentScenario, GettingPlannedTasksScenarioClient>(SyncAgentScenario.GettingPlannedTasks.ToString());

			//stages
			Container.RegisterType<ISyncAgentScenarioStage, ExportRequestingStage>($"{nameof(GettingPlannedTasksScenarioClientStages)}.ExportRequesting"); //todo to const
			Container.RegisterType<ISyncAgentScenarioStage, ExportWaitingStage>($"{nameof(GettingPlannedTasksScenarioClientStages)}.ExportWaiting"); //todo to const

			//repositories
			Container.RegisterType<ISyncAgentTaskRepository, SyncAgentTaskRepository>();

			//isolation
			Container.RegisterType<IIsolatedWorkPool, IsolatedWorkPool>( new ContainerControlledLifetimeManager());
		}
	}
}
