using System;
namespace ReactTest.Models
{
	public class Node
	{
		public Node()
		{
		}

		//public bool IsRoot { get; set; }

		public int Id { get; set; }

		public string Name { get; set; }

		public int? ParentId { get; set; }

		public Node? Parent { get; set; }

		public List<Node> Children { get; set; }
	}
}

