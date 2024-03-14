using System;
namespace ReactTest.Models.InputModels
{
	public class RenameNodeInputModel
	{
		public string TreeName { get; set; }
		public int NodeId { get; set; }
		public string NewNodeName { get; set; }
	}
}

