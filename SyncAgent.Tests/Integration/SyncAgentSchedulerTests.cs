using System;
using System.IO;
using System.Threading.Tasks;
using AppDomainsInteraction;
using AppDomainsInteraction.Contracts;
using AppDomainsInteraction.Extensions.DI;
using AppDomainsInteraction.Scheduler.Jobs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quartz.Unity;
using SyncAgent.Tests.Extensions;
using SyncAgent.Tests.Integration.Base;
using Unity;
using Unity.Interception.Utilities;

namespace SyncAgent.Tests.Integration
{
	[TestClass]
	public class SyncAgentSchedulerTests : BaseTests
	{
		private IUnityContainer _container;
		private ISyncAgentScheduler _scheduler;
		private ISyncAgentTaskRepository _repository;

		[TestInitialize]
		public void Initialize()
		{
			_container = Bootstrapper.ConfigureContainer();
			_container.RegisterTestDoubles();

			_scheduler = _container.Resolve<ISyncAgentScheduler>();
			_repository = _container.Resolve<ISyncAgentTaskRepository>();
		}

		[TestCleanup]
		public void Cleanup()
		{
			_scheduler.Stop();
			_repository.CleanStorage();
		}

		[TestMethod]
		public void SyncAgentSchedulerShouldStart()
		{
			Action action = () => _scheduler.Start();

			CustomAssert.EnsureNoException(action);
		}

		[TestMethod]
		public void SyncAgentSchedulerShouldStop()
		{
			Action action = () => _scheduler.Stop();

			CustomAssert.EnsureNoException(action);
		}

		[TestMethod]
		public async Task SyncAgentSchedulerShouldStartJobSchedulerJob()
		{
			await _scheduler.Start();

			var exist = await _scheduler.CheckJobExists(nameof(JobSchedulerJob));

			Assert.IsTrue(exist, "Expected JobSchedulerJob is planned in the scheduler, but is is not");
		}

		[TestMethod]
		public async Task SyncAgentSchedulerShouldNotFindNonExistJob()
		{
			await _scheduler.Start();

			var exist = await _scheduler.CheckJobExists(Guid.NewGuid().ToString());

			Assert.IsFalse(exist, "Expected scheduler hasn't found nonexistent job, but is has found");
		}


	}
}
