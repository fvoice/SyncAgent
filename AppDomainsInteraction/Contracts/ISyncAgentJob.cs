using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDomainsInteraction.Contracts
{
	public interface ISyncAgentJob
	{
		Task PlanExecution(ISyncAgentScheduler scheduler, Dictionary<string, object> dataParams = null);
	}
}
