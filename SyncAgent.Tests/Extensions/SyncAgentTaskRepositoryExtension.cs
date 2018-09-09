using System.IO;
using AppDomainsInteraction.Contracts;

namespace SyncAgent.Tests.Extensions
{
	public static class SyncAgentTaskRepositoryExtension
	{
		public static void CleanStorage(this ISyncAgentTaskRepository repository)
		{
			if (File.Exists(repository.StoragePath))
			{
				File.Delete(repository.StoragePath);
			}
		}
	}
}
