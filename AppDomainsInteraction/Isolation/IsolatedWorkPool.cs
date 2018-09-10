using System;
using System.Collections.Generic;
using System.Linq;
using AppDomainsInteraction.Contracts;
using AppDomainsInteraction.Storage.Model;

namespace AppDomainsInteraction.Isolation
{
	public class IsolatedWorkPool : IIsolatedWorkPool
	{
		private readonly List<IsolatedWorkContainer> _available = new List<IsolatedWorkContainer>();
		private readonly List<IsolatedWorkContainer> _inUse = new List<IsolatedWorkContainer>();

		public void ExecuteIsolated(string isolationKey, Action<IsolatedWorkParameters> action, IsolatedWorkParameters parameters)
		{
			var isolated = GetObject(isolationKey);
			try
			{
				isolated.Execute(action, parameters);
			}
			finally
			{
				ReleaseObject(isolated);
			}
		}

		private IsolatedWorkContainer GetObject(string key)
		{
			lock (_available)
			{
				

				if (_available.Count(x => Compare(x, key)) != 0)
				{
					IsolatedWorkContainer isolated = _available.First(x => Compare(x, key));
					_inUse.Add(isolated);
					_available.Remove(isolated);
					return isolated;
				}
				else
				{
					//todo limits on pool size
					IsolatedWorkContainer isolated = new IsolatedWorkContainer(key);
					_inUse.Add(isolated);
					return isolated;
				}
			}
		}

		private void ReleaseObject(IsolatedWorkContainer isolated)
		{
			CleanUp(isolated);

			lock (_available)
			{
				_available.Add(isolated);
				_inUse.Remove(isolated);
			}
		}

		private void CleanUp(IsolatedWorkContainer isolated)
		{
			isolated.Executor = null;
		}

		private bool Compare(IsolatedWorkContainer x, string key)
		{
			return (x.Key == key) || x.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase); //null keys are also keys
		}

	}
}
