using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AppDomainsInteraction.Contracts;
using NLog;
using Quartz;

namespace AppDomainsInteraction.Scheduler.Jobs
{
	[DisallowConcurrentExecution]
	public class JobSchedulerJob : IJob, ISyncAgentTask
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();
		private readonly ISyncAgentTaskRepository _repository;

		public JobSchedulerJob(ISyncAgentTaskRepository repository)
		{
			_repository = repository;
		}

		public async Task Execute(IJobExecutionContext context)
		{
			try
			{
				_logger.Info($"PlatTaskJob AppDomain - {AppDomain.CurrentDomain.FriendlyName}, Thread - {Thread.CurrentThread.ManagedThreadId}");

				var assembliesList = string.Join("\n - ", AppDomain.CurrentDomain.GetAssemblies().Select(x => x.FullName));
				await Console.Out.WriteLineAsync($"{AppDomain.CurrentDomain.FriendlyName} assemblies: \n - {assembliesList}");

				//todo pass limit parameter
				var tasksForPlanning = _repository.GetPlanned();

				var scheduler = context.MergedJobDataMap["Scheduler"] as ISyncAgentScheduler; //todo to cinst

				foreach (var syncAgentTask in tasksForPlanning)
				{
					//todo check if task is already planned
					//todo check "plan limit" parameter
					//todo check task's execution lock mb it is alredy in executing state
					await syncAgentTask.PlanExecution(scheduler);
				}
			}
			catch (Exception e)
			{
				_logger.Info(e);
			}
		}

		public async Task PlanExecution(ISyncAgentScheduler scheduler)
		{
			IJobDetail job = JobBuilder.Create<JobSchedulerJob>()
				.WithIdentity(nameof(JobSchedulerJob))
				.Build();

			ITrigger trigger = TriggerBuilder.Create()
				.StartNow()
				.WithSimpleSchedule(x => x
					.WithIntervalInSeconds(5)
					.WithMisfireHandlingInstructionIgnoreMisfires()
					.RepeatForever())
				.Build();

			job.JobDataMap.Put("Scheduler", scheduler); //todo to const

			await scheduler.PlanJob(job, trigger);
		}
	}
}
