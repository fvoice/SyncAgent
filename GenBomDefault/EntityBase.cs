using System;

namespace GenBomDefault
{
	[Serializable]
	public abstract class EntityBase
	{
		public string Name { get; set; }

		public abstract void Save();
	}
}
