using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Quartz;

namespace AppDomainsInteraction.Scheduler
{
	public class SyncAgentSchedulerListener : ISchedulerListener
	{
		private static readonly Logger Logger = NLog.LogManager.GetCurrentClassLogger();

		public Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = new CancellationToken())
		{
			return null;
		}

		public Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = new CancellationToken())
		{
			return null;
		}

		public Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = new CancellationToken())
		{
			return null;
		}

		public Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = new CancellationToken())
		{
			return null;
		}

		public Task TriggersPaused(string triggerGroup, CancellationToken cancellationToken = new CancellationToken())
		{
			return null;
		}

		public Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = new CancellationToken())
		{
			return null;
		}

		public Task TriggersResumed(string triggerGroup, CancellationToken cancellationToken = new CancellationToken())
		{
			return null;
		}

		public Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = new CancellationToken())
		{
			return null;
		}

		public Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = new CancellationToken())
		{
			return null;
		}

		public Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = new CancellationToken())
		{
			return null;
		}

		public Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = new CancellationToken())
		{
			return null;
		}

		public Task JobsPaused(string jobGroup, CancellationToken cancellationToken = new CancellationToken())
		{
			return null;
		}

		public Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = new CancellationToken())
		{
			return null;
		}

		public Task JobsResumed(string jobGroup, CancellationToken cancellationToken = new CancellationToken())
		{
			return null;
		}

		public Task SchedulerError(string msg, SchedulerException cause,
			CancellationToken cancellationToken = new CancellationToken())
		{
			Logger.Error(cause, msg);
			return null;
		}

		public Task SchedulerInStandbyMode(CancellationToken cancellationToken = new CancellationToken())
		{
			return null;
		}

		public Task SchedulerStarted(CancellationToken cancellationToken = new CancellationToken())
		{
			return null;
		}

		public Task SchedulerStarting(CancellationToken cancellationToken = new CancellationToken())
		{
			return null;
		}

		public Task SchedulerShutdown(CancellationToken cancellationToken = new CancellationToken())
		{
			return null;
		}

		public Task SchedulerShuttingdown(CancellationToken cancellationToken = new CancellationToken())
		{
			return null;
		}

		public Task SchedulingDataCleared(CancellationToken cancellationToken = new CancellationToken())
		{
			return null;
		}
	}

}
