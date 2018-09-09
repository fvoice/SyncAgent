using System;
using System.Threading.Tasks;
using AppDomainsInteraction.Contracts;
using AppDomainsInteraction.Extensions.DI;
using Quartz.Unity;
using Unity;

public class Program
{
	public static bool Toggle = true;

	private static void Main(string[] args)
	{
		IUnityContainer container = new UnityContainer();
		container.AddNewExtension<QuartzUnityExtension>();
		container.AddNewExtension<SyncAgentUnityExtension>();

		RunProgram(container).GetAwaiter().GetResult();

		Console.WriteLine("Press any key to close the application");
		Console.ReadKey();
	}

	private static async Task RunProgram(IUnityContainer container)
	{
		try
		{
			var scheduler = container.Resolve<ISyncAgentScheduler>();
			await scheduler.Start();
		}
		catch (Exception se)
		{
			Console.WriteLine(se);
		}
	}
}