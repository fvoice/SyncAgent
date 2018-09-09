using System;
using System.Collections;
using SyncAgent.Tests.Integration.Base;

namespace SyncAgent.Tests.Extensions
{
	public static class CustomAssertExtension
	{
		public static void EnsureCountIsEqual(this CustomAssert assert, ICollection collection, int expectedCount)
		{
			if (collection.Count != expectedCount)
			{
				throw new Exception($"Expected {expectedCount} record(s) but {collection.Count} detected");
			}
		}

		public static void EnsureNoException(this CustomAssert assert, Action action)
		{
			try
			{
				action.Invoke();
			}
			catch (Exception e)
			{
				throw new Exception($"Expected no exception but {e.GetType().Name} with message - {e.Message} detected");
			}
		}
	}
}
