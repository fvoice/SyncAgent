using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AppDomainsInteraction.Scheduler;
using AppDomainsInteraction.Scheduler.Contracts;
using NLog;
using Quartz;

namespace AppDomainsInteraction
{
	[DisallowConcurrentExecution]
	public class PlanTasksJob : IJob, ISyncAgentTask
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();
		private readonly ISyncAgentScheduler _scheduler;

		public PlanTasksJob(ISyncAgentScheduler scheduler)
		{
			_scheduler = scheduler;
		}

		public async Task Execute(IJobExecutionContext context)
		{
			try
			{
				_logger.Info($"PlatTaskJob AppDomain - {AppDomain.CurrentDomain.FriendlyName}, Thread - {Thread.CurrentThread.ManagedThreadId}");

				var assembliesList = string.Join("\n - ", AppDomain.CurrentDomain.GetAssemblies().Select(x => x.FullName));
				await Console.Out.WriteLineAsync($"{AppDomain.CurrentDomain.FriendlyName} assemblies: \n - {assembliesList}");

				if (!File.Exists(".cancel"))
				{
					_logger.Info("Requesting cancellation");
					using (File.Create(".cancel")) { }
					Thread.Sleep(1000);
				}

				IJobDetail job = JobBuilder.Create<IsolatedWorkExecutorJob>() //schedule execution of isolated job
					.WithIdentity(new JobKey("IsolatedWorkExecutorJob"))
					.Build();

				ITrigger trigger = TriggerBuilder.Create()
					.WithIdentity("trigger2", "group2")
					.StartNow()
					.Build();

				await context.Scheduler.ScheduleJob(job, trigger);
			}
			catch (Exception e)
			{
				_logger.Info(e);
			}
		}

		public async Task PlanExecution()
		{
			IJobDetail job = JobBuilder.Create<PlanTasksJob>()
				.WithIdentity(nameof(PlanTasksJob))
				.Build();

			ITrigger trigger = TriggerBuilder.Create()
				.StartNow()
				.WithSimpleSchedule(x => x
					.WithIntervalInSeconds(5)
					.WithMisfireHandlingInstructionIgnoreMisfires()
					.RepeatForever())
				.Build();

			await _scheduler.Scheduler.ScheduleJob(job, trigger);
		}
	}
}
