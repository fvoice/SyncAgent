using System;
using GenBomDefault;

namespace GenBom1
{
    public class Entity : EntityBase
	{
	    public override void Save()
	    {
			Console.WriteLine($"Entity with Name - {Name} is saved");
	    }
    }
}
