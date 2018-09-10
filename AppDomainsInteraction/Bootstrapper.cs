using AppDomainsInteraction.Extensions.DI;
using Quartz.Unity;
using Unity;

namespace AppDomainsInteraction
{
	public static class Bootstrapper
	{
		private static IUnityContainer _container;

		public static IUnityContainer ConfigureContainer()
		{
			_container = new UnityContainer();
			_container.AddNewExtension<QuartzUnityExtension>();
			_container.AddNewExtension<SyncAgentUnityExtension>();

			return _container;
		}

		public static IUnityContainer ConfigureChildContainer()
		{
			return _container.CreateChildContainer();
		}
	}
}
