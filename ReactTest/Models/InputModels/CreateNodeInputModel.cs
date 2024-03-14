using System;
namespace ReactTest.Models.InputModels
{
	public class CreateNodeInputModel
	{
		public string TreeName { get; set; }
		public int ParentNodeId { get; set; }
		public string NodeName { get; set; }
	}
}

