using System;
using System.IO;
using AppDomainsInteraction.Storage;

namespace SyncAgent.Tests.Test_doubles
{
	public class SyncAgentTaskRepositoryConcurrent : SyncAgentTaskRepository
	{
		private string _storagePath = "TestStorage";
		private string _storageFileName = Guid.NewGuid().ToString();

		public SyncAgentTaskRepositoryConcurrent()
		{
			if (!Directory.Exists(_storagePath))
			{
				Directory.CreateDirectory(_storagePath);
			}
		}

		public override string StoragePath => $"{_storagePath}\\{_storageFileName}";
	}
}
