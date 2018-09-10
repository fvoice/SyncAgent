using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AppDomainsInteraction.Contracts;
using AppDomainsInteraction.Storage.Model;
using NLog;
using Quartz;
using Unity;

namespace AppDomainsInteraction.Scheduler.Jobs
{
	[DisallowConcurrentExecution]
	public class JobSchedulerJob : IJob, ISyncAgentJob
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();
		private readonly ISyncAgentTaskRepository _repository;
		private readonly IUnityContainer _container;

		public JobSchedulerJob(ISyncAgentTaskRepository repository, IUnityContainer container)
		{
			_repository = repository;
			_container = container;
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

				if (!(context.MergedJobDataMap["Scheduler"] is ISyncAgentScheduler scheduler))
				{
					throw new Exception("Scheduler parameter is required for JobSchedulerJob");
				}

				foreach (var syncAgentTask in tasksForPlanning)
				{
					if (await scheduler.CheckJobExists(syncAgentTask.Id.ToString()))
					{
						continue;
					}
					
					//todo check "plan limit" parameter
					//todo check task's execution lock mb it is alredy in executing state
					var syncJob = _container.Resolve<ISyncAgentJob>(SyncAgentTaskType.IsolatedWorkExecutor.ToString());
					await syncJob.PlanExecution(scheduler, new Dictionary<string, object>()
					{
						{
							nameof(SyncAgentTask), syncAgentTask
						}
					});
				}
			}
			catch (Exception e)
			{
				_logger.Info(e);
			}
		}

		public async Task PlanExecution(ISyncAgentScheduler scheduler, Dictionary<string, object> dataParams = null)
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
