using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GenBomDefault;
using NLog;
using Quartz;

namespace AppDomainsInteraction
{
	public class IsolatedWorkExecutorJob : IJob
	{
		readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();

		public async Task Execute(IJobExecutionContext context)
		{
			try
			{
				using (Isolated<IsolatedWork> isolated = new Isolated<IsolatedWork>())
				{
					if (Program.Toggle)
					{
						isolated.LoadAssembly(new AssemblyName("GenBom1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"));
					}
					else
					{
						isolated.LoadAssembly(new AssemblyName("GenBom2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"));
					}

					//CancellationTokenSource source = new CancellationTokenSource();
					var token = new CancellationTokenContainer();
					//token.Token = context.CancellationToken;
					File.Delete(".cancel");


					isolated.Value.Execute((toggle, t) =>
					{
						Logger _localLogger = NLog.LogManager.GetCurrentClassLogger();

						var assembliesList = string.Join("\n - ", AppDomain.CurrentDomain.GetAssemblies().Select(x => x.FullName));
						_localLogger.Info($"{AppDomain.CurrentDomain.FriendlyName} assemblies: \n - {assembliesList}");

						//var entityTypeLocal = toggle ? typeof(GenBom1.Entity) : typeof(GenBom2.Entity);

						var typeString = toggle ? "GenBom1.Entity" : "GenBom2.Entity";
						var assemblyString = toggle ? "GenBom1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" : "GenBom2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

						//var entity = (EntityBase)Activator.CreateInstance(entityTypeLocal);
						var entity = (EntityBase)Activator.CreateInstance(assemblyString, typeString).Unwrap();
						entity.Name = "123";
						entity.Save();

						for (int i = 0; i < 20; i++)
						{
							if (File.Exists(".cancel"))
							{
								_localLogger.Info("Task is cancelling");
								File.Delete(".cancel");
								break;
							}
							_localLogger.Info("Do work");
							Thread.Sleep(1000);
						}

						_logger.Info($"IsolatedWork AppDomain - {AppDomain.CurrentDomain.FriendlyName}, Thread - {Thread.CurrentThread.ManagedThreadId}");
					}, Program.Toggle, token);

					//change a GenBom version
					Program.Toggle = !Program.Toggle;
				}
			}
			catch (Exception e)
			{
				_logger.Warn(e);
			}

			_logger.Error($"IsolatedWorkExecutorJob AppDomain - {AppDomain.CurrentDomain.FriendlyName}, Thread - {Thread.CurrentThread.ManagedThreadId}");
		}
	}
}
