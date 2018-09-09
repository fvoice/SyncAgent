using System.Collections.Generic;
using AppDomainsInteraction.Storage.Model;

namespace AppDomainsInteraction.Contracts
{
	public interface ISyncAgentTaskRepository
	{
		string StoragePath { get; }

		IList<SyncAgentTask> GetAll();
		IList<SyncAgentTask> GetPlanned(int limit = 0);

		void Save(SyncAgentTask task);
	}
}