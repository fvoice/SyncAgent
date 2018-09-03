using System;
using System.Threading.Tasks;
using AppDomainsInteraction;
using AppDomainsInteraction.Scheduler;
using AppDomainsInteraction.Scheduler.Contracts;
using Quartz;
using Quartz.Unity;
using Unity;
using Unity.Lifetime;

public class Program
{
	public static bool Toggle = true;

	private static void Main(string[] args)
	{
		IUnityContainer container = new UnityContainer();

		container.AddNewExtension<QuartzUnityExtension>();

		container.RegisterType<ISyncAgentScheduler, SyncAgentScheduler>(new ContainerControlledLifetimeManager());
		container.RegisterType<ISyncAgentTask, PlanTasksJob>(SyncAgentTaskType.PlanTasks.ToString());
		container.RegisterType<ISchedulerListener, SyncAgentSchedulerListener>();


		RunProgramRunExample(container).GetAwaiter().GetResult();

		Console.WriteLine("Press any key to close the application");
		Console.ReadKey();
	}

	private static async Task RunProgramRunExample(IUnityContainer container)
	{
		try
		{
			var scheduler = container.Resolve<ISyncAgentScheduler>();
			await scheduler.Start();

			var planTask = container.Resolve<ISyncAgentTask>(SyncAgentTaskType.PlanTasks.ToString());
			await planTask.PlanExecution();
		}
		catch (Exception se)
		{
			Console.WriteLine(se);
		}
	}
}