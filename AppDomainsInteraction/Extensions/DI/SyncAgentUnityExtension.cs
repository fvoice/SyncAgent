using AppDomainsInteraction.Contracts;
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
			Container.RegisterType<ISyncAgentScheduler, SyncAgentScheduler>(new ContainerControlledLifetimeManager());
			Container.RegisterType<ISyncAgentTask, JobSchedulerJob>(SyncAgentTaskType.JobScheduler.ToString());
			Container.RegisterType<ISchedulerListener, SyncAgentSchedulerListener>();

			Container.RegisterType<ISyncAgentTaskRepository, SyncAgentTaskRepository>();
		}
	}
}
