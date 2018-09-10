
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomainsInteraction.Scenarios.Base
{
	public abstract class StagesBase
	{
		private string _current;

		protected StagesBase()
		{
			Stages = new List<string>();
		}

		protected virtual List<string> Stages { get; }

		public string Current
		{
			get => _current;
			set
			{
				if (Stages.IndexOf(value) >= 0)
				{
					_current = value;
				}
				else
				{
					throw new Exception($"Stage {value} is not valid stage for {nameof(GetType)}");
				}
			}
		}

		public void Next()
		{
			var curIndex = Stages.IndexOf(Current);
			Current = curIndex == Stages.Count - 1 ? Stages[curIndex] : Stages[curIndex + 1];
		}
	}
}
