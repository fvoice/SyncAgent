namespace SyncAgent.Tests.Integration.Base
{
	public class BaseTests
	{
		public CustomAssert CustomAssert { get; }

		public BaseTests()
		{
			CustomAssert = new CustomAssert();
		}
	}
}
