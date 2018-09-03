using System.Threading.Tasks;

namespace AppDomainsInteraction.Scheduler.Contracts
{
	public interface ISyncAgentTask
	{
		Task PlanExecution();
	}
}
