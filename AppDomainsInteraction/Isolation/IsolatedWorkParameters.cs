using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDomainsInteraction.Storage.Model;

namespace AppDomainsInteraction.Isolation
{
	[Serializable]
	public class IsolatedWorkParameters
	{
		public SyncAgentTask SyncAgentTask { get; set; }
	}
}
