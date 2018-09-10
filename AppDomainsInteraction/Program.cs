using System;
using System.Threading.Tasks;
using AppDomainsInteraction;
using AppDomainsInteraction.Contracts;
using AppDomainsInteraction.Extensions.DI;
using Quartz.Unity;
using Unity;

public class Program
{
	public static bool Toggle = true;

	private static void Main(string[] args)
	{
		var container = Bootstrapper.ConfigureContainer();

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
		catch (Exception e)
		{
			Console.WriteLine(e);
		}
	}
}