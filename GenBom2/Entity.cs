using System;
using GenBomDefault;

namespace GenBom2
{
    public class Entity : EntityBase
	{
		public string CustomProperty { get; set; }

	    public override void Save()
	    {
			Console.WriteLine($"Entity with Name - {Name} and CustomProperty - {CustomProperty} is saved");
	    }
    }
}
