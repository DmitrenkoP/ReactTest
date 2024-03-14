using System;
namespace ReactTest
{
	public class SecureException : Exception
	{
		public SecureException(string message)
			: base(message)
		{
		}
	}
}

