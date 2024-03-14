using System;
using Microsoft.AspNetCore.Mvc;
using ReactTest.Database;
using ReactTest.Models;
using ReactTest.Models.InputModels;
using ReactTest.Models.OutputModels;

namespace ReactTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TreeController : ControllerBase
	{
		private INodesRepository repository;
		public TreeController(INodesRepository repository)
		{
			this.repository = repository;
		}

        [Route("get")]
        [HttpPost]
		public NodeOutputModel GetTree(GetTreeInputModel model)
		{
			var treeName = model.TreeName;
			var tree = repository.GetTree(treeName);
			if (tree == null)
			{
				tree = new Node { Name = treeName };
				repository.Add(tree);
			}
			return Map(tree);
		}

        [Route("node/create")]
        [HttpPost]
		public NodeOutputModel Create(CreateNodeInputModel model)
		{
			var parent = repository.GetById(model.ParentNodeId);
			if (parent == null)
			{
				throw new SecureException("Node not found");
			}
			else if (!repository.CheckIfNodeBelongsToTree(model.TreeName, model.ParentNodeId))
			{
				throw new SecureException("Requested node was found, but it doesn't belong your tree");
			}
			else if (NotUniqueName(parent, model.NodeName))
			{
				throw new SecureException("A new node name must be unique across all siblings");
			}
			else 
			{
				var node = new Node
				{
					Name = model.NodeName,
					ParentId = model.ParentNodeId,
					Parent = parent
				};
				repository.Add(node);
				return Map(node);
			}
		}

        [Route("node/update")]
        [HttpPost]
		public NodeOutputModel Rename(RenameNodeInputModel model)
		{
			var node = repository.GetById(model.NodeId);
			if (node == null)
			{
				throw new SecureException("Node not found");
			}
			else if (!repository.CheckIfNodeBelongsToTree(model.TreeName, node.Id))
			{
				throw new SecureException("Requested node was found, but it doesn't belong your tree");
			}
			else if (node.Parent != null)
			{
				if (NotUniqueName(node.Parent, model.NewNodeName))
				{
					throw new SecureException("A new node name must be unique across all siblings");
				}
                node.Name = model.NewNodeName;
                repository.Update(node);
                return Map(node);
            }
			else
			{
				if (NotUniqueRootName(model.NewNodeName))
				{
                    throw new SecureException("A new root name must be unique across all roots");
                }
				node.Name = model.NewNodeName;
				repository.Update(node);
				return Map(node);
			}
        }

        [Route("node/delete")]
        [HttpPost]
		public void Delete(DeleteNodeInputModel model)
		{
            var node = repository.GetById(model.NodeId);
			if (node == null)
			{
				throw new SecureException("Node not found");
			}
			else if (!repository.CheckIfNodeBelongsToTree(model.TreeName, node.Id))
			{
				throw new SecureException("Requested node was found, but it doesn't belong your tree");
			}
			else
			{
				repository.Delete(node);
			}
        }


		private NodeOutputModel Map(Node node)
		{
			var model = new NodeOutputModel();
			model.Id = node.Id;
			model.Name = node.Name;
			model.Children = new List<NodeOutputModel>();
			if (node.Children != null)
			{
				foreach (var child in node.Children)
				{
					model.Children.Add(Map(child));
				}
			}
			return model;
		}

		private bool NotUniqueName(Node node, string newNodeName)
		{
			return node.Children.Any(x => x.Name == newNodeName);
		}

		private bool NotUniqueRootName(string newRootName)
		{
			var roots = repository.GetAllRoots();
			return roots.Any(x => x.Name == newRootName);
		}
    }
}

