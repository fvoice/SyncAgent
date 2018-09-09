using System.Threading.Tasks;

namespace AppDomainsInteraction.Contracts
{
	public interface ISyncAgentTask
	{
		Task PlanExecution(ISyncAgentScheduler scheduler);
	}
}
