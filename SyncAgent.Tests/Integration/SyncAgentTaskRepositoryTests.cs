using System;
using System.Linq;
using AppDomainsInteraction;
using AppDomainsInteraction.Contracts;
using AppDomainsInteraction.Extensions.DI;
using AppDomainsInteraction.Storage.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quartz.Unity;
using SyncAgent.Tests.Extensions;
using SyncAgent.Tests.Integration.Base;
using Unity;
using Unity.Resolution;

namespace SyncAgent.Tests.Integration
{
	[TestClass]
	public class SyncAgentTaskRepositoryTests : BaseTests
	{
		private IUnityContainer _container;
		private ISyncAgentTaskRepository _repository;

		[TestInitialize]
		public void Initialize()
		{
			_container = Bootstrapper.ConfigureContainer();
			_container.RegisterTestDoubles();

			_repository = _container.Resolve<ISyncAgentTaskRepository>();
		}

		[TestCleanup]
		public void Cleanup()
		{
			_repository.CleanStorage();
		}

		[TestMethod]
		public void SyncAgentTaskRepositoryShouldSaveAndReturnOneTask()
		{
			_repository.Save(new SyncAgentTask());
			var tasks = _repository.GetAll();

			CustomAssert.EnsureCountIsEqual(tasks.ToList(), 1);
		}

		[TestMethod]
		public void SyncAgentTaskRepositoryShouldSaveAndReturnSeveralTask()
		{
			_repository.Save(new SyncAgentTask());
			_repository.Save(new SyncAgentTask());
			var tasks = _repository.GetAll();

			CustomAssert.EnsureCountIsEqual(tasks.ToList(), 2);
		}

		[TestMethod]
		public void SyncAgentTaskRepositoryShouldReturnPlannedTaskWithPendingState()
		{
			var expectedState = SyncAgentTaskState.Pending;
			_repository.Save(new SyncAgentTask() {State = expectedState });
			var tasks = _repository.GetPlanned();

			CustomAssert.EnsureCountIsEqual(tasks.ToList(), 1);
			Assert.IsTrue(tasks.Single().State == expectedState, $"Expected task with state - {expectedState}, but {tasks.Single().State} detected");
		}

		[TestMethod]
		public void SyncAgentTaskRepositoryShouldReturnPlannedTaskWithErrorState()
		{
			var expectedState = SyncAgentTaskState.Error;
			_repository.Save(new SyncAgentTask() { State = expectedState });
			var tasks = _repository.GetPlanned();

			CustomAssert.EnsureCountIsEqual(tasks.ToList(), 1);
			Assert.IsTrue(tasks.Single().State == expectedState, $"Expected task with state - {expectedState}, but {tasks.Single().State} detected");
		}
	}
}
