using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using AppDomainsInteraction.Contracts;
using Quartz;
using Quartz.Impl;
using Unity;

namespace AppDomainsInteraction.Scheduler
{
	public class SyncAgentScheduler : ISyncAgentScheduler
	{
		private readonly ISchedulerFactory _schedulerFactory;
		private readonly ISchedulerListener _schedulerListener;
		private readonly IUnityContainer _container;
		private IScheduler _scheduler;

		public SyncAgentScheduler(ISchedulerFactory schedulerFactory, ISchedulerListener schedulerListener, IUnityContainer container)
		{
			_schedulerFactory = schedulerFactory;
			_schedulerListener = schedulerListener;
			_container = container;
		}

		public async Task Start()
		{
			if (_scheduler != null && _scheduler.IsStarted) return;

			NameValueCollection props = new NameValueCollection
			{
				{ "quartz.scheduler.instanceName", "WebSyncScheduler" },
				{ "quartz.threadPool.threadCount", "20" } //todo config
			};
			((StdSchedulerFactory)_schedulerFactory).Initialize(props);

			_scheduler = await _schedulerFactory.GetScheduler(CancellationToken.None);

			//_scheduler.ListenerManager.AddSchedulerListener(_schedulerListener);

			await _scheduler.Start();

			//schedule execution of planning task
			var planTask = _container.Resolve<ISyncAgentJob>(SyncAgentTaskType.JobScheduler.ToString());
			await planTask.PlanExecution(this);
		}

		public async Task Stop()
		{
			if (_scheduler.IsShutdown) return;

			await _scheduler.Shutdown();
		}

		public async Task PlanJob(IJobDetail jobDetail, ITrigger trigger)
		{
			await _scheduler.ScheduleJob(jobDetail, trigger);
		}

		public async Task<bool> CheckJobExists(string jobKey)
		{
			return await _scheduler.CheckExists(new JobKey(jobKey));
		}
	}
}
