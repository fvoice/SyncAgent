using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppDomainsInteraction.Contracts;
using AppDomainsInteraction.Isolation;
using AppDomainsInteraction.Storage.Model;
using NLog;
using Quartz;
using Unity;

namespace AppDomainsInteraction.Scheduler.Jobs
{
	public class IsolatedWorkExecutorJob : IJob, ISyncAgentJob
	{
		readonly Logger _logger = LogManager.GetCurrentClassLogger();
		private readonly IUnityContainer _container;

		public IsolatedWorkExecutorJob(IUnityContainer container)
		{
			_container = container;
		}

		public async Task Execute(IJobExecutionContext context)
		{
			try
			{
				if (context.MergedJobDataMap[nameof(SyncAgentTask)] is SyncAgentTask syncAgentTask)
				{
					//todo check execution lock
					//todo set execution lock
					var isolatedPool = _container.Resolve<IIsolatedWorkPool>();

					var parameters = new IsolatedWorkParameters {SyncAgentTask = syncAgentTask};

					var action = new Action<IsolatedWorkParameters>((isolatedParameters) =>
					{
						var isolatedLogger = LogManager.GetCurrentClassLogger();
						var isolatedSyncAgentTask = isolatedParameters.SyncAgentTask;
						try
						{
							var isolatedContainer = Bootstrapper.ConfigureContainer();

							var scenario = isolatedContainer.Resolve<ISyncAgentScenario>(isolatedSyncAgentTask.Scenario);

							scenario.Initialize(isolatedSyncAgentTask);
							scenario.ExecuteCurrentStage();
							scenario.NextStage();
						}
						catch (Exception e)
						{
							isolatedLogger.Warn(e);
						}
					});

					isolatedPool.ExecuteIsolated(syncAgentTask.ConnectionId, action, parameters);
				}
			}
			catch (Exception e)
			{
				_logger.Warn(e);
			}
		}

		public async Task PlanExecution(ISyncAgentScheduler scheduler, Dictionary<string, object> dataParams = null)
		{
			if (dataParams != null && dataParams.ContainsKey(nameof(SyncAgentTask))) //todo keys to const
			{
				if (dataParams[nameof(SyncAgentTask)] is SyncAgentTask syncAgentTask)
				{
					IJobDetail job = JobBuilder.Create<IsolatedWorkExecutorJob>()
						.WithIdentity(syncAgentTask.Id.ToString())
						.Build();

					ITrigger trigger = TriggerBuilder.Create()
						.StartNow()
						.Build();

					job.JobDataMap.Put(nameof(SyncAgentTask), syncAgentTask);

					await scheduler.PlanJob(job, trigger);
					return;
				}
			}
			throw new ArgumentException($"Parameter {nameof(SyncAgentTask)} is required to plan execution of {nameof(IsolatedWorkExecutorJob)}");
		}

		//private void IsolatedExecute(IsolatedWorkParameters parameters)
		//{
		//	var isolatedLogger = LogManager.GetCurrentClassLogger();
		//	var syncAgentTask = parameters.SyncAgentTask;
		//	try
		//	{
		//		var isolatedContainer = Bootstrapper.ConfigureChildContainer();

		//		var scenario = isolatedContainer.Resolve<ISyncAgentScenario>(syncAgentTask.Scenario);

		//		scenario.Initialize(syncAgentTask);
		//		scenario.ExecuteCurrentStage();
		//		scenario.NextStage();
		//	}
		//	catch (Exception e)
		//	{
		//		isolatedLogger.Warn(e);
		//	}
		//}
	}
}
