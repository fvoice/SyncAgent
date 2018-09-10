using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDomainsInteraction.Contracts;
using SyncAgent.Tests.Test_doubles;
using Unity;

namespace SyncAgent.Tests.Extensions
{
	public static class UnityExtension
	{
		public static void RegisterTestDoubles(this IUnityContainer container)
		{
			container.RegisterType<ISyncAgentTaskRepository, SyncAgentTaskRepositoryConcurrent>();
		}
	}
}
