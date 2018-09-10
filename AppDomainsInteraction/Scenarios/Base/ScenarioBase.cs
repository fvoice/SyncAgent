using System;
using AppDomainsInteraction.Contracts;
using AppDomainsInteraction.Storage.Model;
using Unity;

namespace AppDomainsInteraction.Scenarios.Base
{
	public abstract class ScenarioBase<T> : ISyncAgentScenario where T : StagesBase, new()
	{
		private SyncAgentTask _syncAgentTask;
		private readonly IUnityContainer _container;

		protected T CurrentStage;
		protected ISyncAgentScenarioStage CurrentStageExecutor;

		protected ScenarioBase(IUnityContainer container)
		{
			_container = container;
		}

		public virtual void Initialize(SyncAgentTask syncAgentTask)
		{
			_syncAgentTask = syncAgentTask;

			CurrentStage = new T();
		}

		public virtual void ExecuteCurrentStage()
		{
			if (_syncAgentTask == null) throw new ArgumentNullException($"{nameof(_syncAgentTask)} is reqired");

			CurrentStageExecutor = _container.Resolve<ISyncAgentScenarioStage>($"{typeof(T).Name}.{CurrentStage.Current}");
		}

		public void NextStage()
		{
			CurrentStage.Next();
		}
	}
}
