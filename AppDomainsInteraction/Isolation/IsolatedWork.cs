using System;

namespace AppDomainsInteraction.Isolation
{
	public class IsolatedWork : MarshalByRefObject
	{
		public void Execute(Action<IsolatedWorkParameters> action, IsolatedWorkParameters parameters)
		{
			action.Invoke(parameters);
		}
	}
}
