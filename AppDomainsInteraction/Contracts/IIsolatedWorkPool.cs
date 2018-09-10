using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDomainsInteraction.Isolation;

namespace AppDomainsInteraction.Contracts
{
	public interface IIsolatedWorkPool
	{
		void ExecuteIsolated(string isolationKey, Action<IsolatedWorkParameters> action, IsolatedWorkParameters parameters);
	}
}
