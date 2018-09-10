using System;

namespace AppDomainsInteraction.Isolation
{
	public class IsolatedWorkContainer : MarshalByRefObject
	{
		private readonly AppDomain _domain;

		public IsolatedWorkContainer(string key)
		{
			Key = key; //connectionId

			_domain = AppDomain.CreateDomain("Isolated:" + Guid.NewGuid(), null, AppDomain.CurrentDomain.SetupInformation);
			
			//todo load necessary libraries - GeneratedBOM of specific version and so on
		}

		public string Key { get; }
		public IsolatedWork Executor { get; set; }

		public void Execute(Action<IsolatedWorkParameters> action, IsolatedWorkParameters parameters)
		{
			if (Executor == null)
			{
				Type type = typeof(IsolatedWork);
				Executor = (IsolatedWork)_domain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);
			}

			Executor.Execute(action, parameters);
		}
	}
}
