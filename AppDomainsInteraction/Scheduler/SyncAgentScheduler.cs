using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace AppDomainsInteraction.Scheduler
{
	public interface ISyncAgentScheduler
	{
		IScheduler Scheduler { get; }
		Task Start();
		Task Stop();
	}

	public class SyncAgentScheduler : ISyncAgentScheduler
	{
		private readonly ISchedulerFactory _schedulerFactory;
		private readonly ISchedulerListener _schedulerListener;

		public SyncAgentScheduler(ISchedulerFactory schedulerFactory, ISchedulerListener schedulerListener)
		{
			_schedulerFactory = schedulerFactory;
			_schedulerListener = schedulerListener;
		}

		public IScheduler Scheduler { get; private set; }

		public async Task Start()
		{
			NameValueCollection props = new NameValueCollection
			{
				{ "quartz.scheduler.instanceName", "WebSyncScheduler" },
				{ "quartz.threadPool.threadCount", "10" } //todo config
			};
			((StdSchedulerFactory)_schedulerFactory).Initialize(props);

			Scheduler = await _schedulerFactory.GetScheduler(CancellationToken.None);

			Scheduler.ListenerManager.AddSchedulerListener(_schedulerListener);

			await Scheduler.Start();
		}

		public async Task Stop()
		{
			await Scheduler.Shutdown();
		}
	}
}
