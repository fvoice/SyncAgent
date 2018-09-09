using System.Threading.Tasks;
using Quartz;

namespace AppDomainsInteraction.Contracts
{
	public interface ISyncAgentScheduler
	{
		Task Start();
		Task Stop();

		Task PlanJob(IJobDetail jobDetail, ITrigger trigger);

		Task<bool> CheckJobExists(string jobKey);
	}
}
