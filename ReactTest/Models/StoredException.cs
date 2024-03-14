using System;
namespace ReactTest.Models
{
	public class StoredException
	{
		public int Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public string Path { get; set; }
		public string Body { get; set; }
		public string StackTrace { get; set; }
	}
}

