using System;

namespace AppDomainsInteraction.Storage.Model
{
	[Serializable]
	public class SyncAgentTask
	{
		public SyncAgentTask()
		{
			Id = Guid.NewGuid();
			State = SyncAgentTaskState.Created;
			Planned = DateTime.Now;
		}

		public Guid Id { get; set; }

		public string Scenario { get; set; }

		public string Login { get; set; }

		public string ConnectionId { get; set; }

		public SyncAgentTaskState State { get; set; }

		public DateTime? Started { get; set; }

		public DateTime? Finished { get; set; }

		public DateTime? Planned { get; set; }
	}
}
