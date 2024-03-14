using System;
namespace ReactTest.Models.OutputModels
{
	public class NodeOutputModel
	{
        public int Id { get; set; }

        public string Name { get; set; }

        public List<NodeOutputModel> Children { get; set; }
    }
}

