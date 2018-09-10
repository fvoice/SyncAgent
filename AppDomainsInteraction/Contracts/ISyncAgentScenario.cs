using AppDomainsInteraction.Storage.Model;

namespace AppDomainsInteraction.Contracts
{
	public interface ISyncAgentScenario
	{
		void Initialize(SyncAgentTask syncAgentTask);

		void ExecuteCurrentStage();

		void NextStage();
	}
}
