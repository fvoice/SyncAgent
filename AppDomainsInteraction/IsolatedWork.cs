using System;
using System.Threading;

namespace AppDomainsInteraction
{
	public class IsolatedWork : MarshalByRefObject
	{
		public void Execute(Action action)
		{
			action.Invoke();
		}

		public void Execute(Action<bool, CancellationTokenContainer> action, bool param, CancellationTokenContainer token)
		{
			action.Invoke(param, token);
		}
	}

	[Serializable]
	public class CancellationTokenContainer
	{
		public bool IsCancelled { get; set; }
	}
}
