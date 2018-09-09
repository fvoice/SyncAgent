using System;
using System.Threading.Tasks;
using AppDomainsInteraction.Contracts;
using AppDomainsInteraction.Scheduler.Jobs;
using Quartz;

namespace AppDomainsInteraction.Storage.Model
{
	[Serializable]
	public class SyncAgentTask : ISyncAgentTask
	{
		public SyncAgentTask()
		{
			Id = Guid.NewGuid();
			State = SyncAgentTaskState.Created;
			Planned = DateTime.Now;
		}

		public Guid Id { get; set; }

		public string Scenario { get; set; }

		public string Login { get; set; }

		public SyncAgentTaskState State { get; set; }

		public DateTime? Started { get; set; }

		public DateTime? Finished { get; set; }

		public DateTime? Planned { get; set; }

		public async Task PlanExecution(ISyncAgentScheduler scheduler)
		{
			IJobDetail job = JobBuilder.Create<IsolatedWorkExecutorJob>()
				.WithIdentity(Id.ToString(), nameof(IsolatedWorkExecutorJob))
				.Build();

			ITrigger trigger = TriggerBuilder.Create()
				.StartNow()
				.Build();

			job.JobDataMap.Put(nameof(SyncAgentTask), this);

			await scheduler.PlanJob(job, trigger);
		}
	}
}
