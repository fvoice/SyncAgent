using System.IO;
using AppDomainsInteraction.Contracts;
using Unity.Interception.Utilities;

namespace SyncAgent.Tests.Extensions
{
	public static class SyncAgentTaskRepositoryExtension
	{
		public static void CleanStorage(this ISyncAgentTaskRepository repository)
		{
			var tasksStoragePath = Path.GetDirectoryName(repository.StoragePath);
			if (tasksStoragePath != null) Directory.GetFiles(tasksStoragePath).ForEach(File.Delete);
		}
	}
}
