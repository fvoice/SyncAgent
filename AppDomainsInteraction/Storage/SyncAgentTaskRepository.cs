using System;
using System.Collections.Generic;
using System.IO;
using AppDomainsInteraction.Contracts;
using AppDomainsInteraction.Storage.Model;
using LiteDB;
using Unity.Attributes;

namespace AppDomainsInteraction.Storage
{
	public class SyncAgentTaskRepository : ISyncAgentTaskRepository
	{
		protected readonly string _storageFileName = "SyncAgentTasks.db"; 
		private readonly string _storageFileNamePath = "Storage"; //todo to config

		public virtual string StoragePath => Path.Combine(_storageFileNamePath, _storageFileName);

		public SyncAgentTaskRepository()
		{
			EnsureStorageDirectoryExists();
		}

		public IList<SyncAgentTask> GetAll()
		{
			using (var db = new LiteRepository(StoragePath))
			{
				return db.Query<SyncAgentTask>().ToList();
			}
		}

		public IList<SyncAgentTask> GetPlanned(int limit = 0)
		{
			using (var db = new LiteRepository(StoragePath))
			{
				return db.Query<SyncAgentTask>()
					.Where(x => x.Planned < DateTime.Now 
					            && (x.State == SyncAgentTaskState.Pending || x.State == SyncAgentTaskState.Planned || x.State == SyncAgentTaskState.Error))
					.Limit(limit > 0 ? limit : Int32.MaxValue)
					.ToList();
			}
		}

		public void Save(SyncAgentTask task)
		{
			using (var db = new LiteDatabase(StoragePath))
			{
				var tasks = db.GetCollection<SyncAgentTask>();
				tasks.EnsureIndex(x => x.Planned);
				tasks.Insert(task);
			}
		}

		private void EnsureStorageDirectoryExists()
		{
			if (!Directory.Exists(_storageFileNamePath))
			{
				Directory.CreateDirectory(_storageFileNamePath);
			}
		}
	}
}