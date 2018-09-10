using AppDomainsInteraction.Scenarios.Base;
using Unity;

namespace AppDomainsInteraction.Scenarios.GettingPlannedTasksScenario
{
	public class GettingPlannedTasksScenarioServer : ScenarioBase<GettingPlannedTasksScenarioServerStages>
	{
		public GettingPlannedTasksScenarioServer(IUnityContainer container) : base(container)
		{
		}
	}
}
