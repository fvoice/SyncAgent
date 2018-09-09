using System;
using System.Threading.Tasks;
using AppDomainsInteraction.Contracts;
using AppDomainsInteraction.Extensions.DI;
using AppDomainsInteraction.Scheduler.Jobs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quartz.Unity;
using SyncAgent.Tests.Extensions;
using SyncAgent.Tests.Integration.Base;
using Unity;

namespace SyncAgent.Tests.Integration
{
	[TestClass]
	public class SyncAgentSchedulerTests : BaseTests
	{
		private IUnityContainer _container;
		private ISyncAgentScheduler _scheduler;

		[TestInitialize]
		public void Initialize()
		{
			_container = new UnityContainer();
			_container.AddNewExtension<QuartzUnityExtension>();
			_container.AddNewExtension<SyncAgentUnityExtension>();

			_scheduler = _container.Resolve<ISyncAgentScheduler>();
		}

		[TestCleanup]
		public void Cleanup()
		{
			_scheduler.Stop();
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
