using System;
using ReactTest.Models;

namespace ReactTest.Database
{
	public interface INodesRepository
	{
        Node GetTree(string treeName);

        Node GetById(int id);

        List<Node> GetAllRoots();

        void Add(Node node);

        void Update(Node node);

        void Delete(Node node);

        bool CheckIfNodeBelongsToTree(string treeName, int nodeId);
    }
}

