using Microsoft.EntityFrameworkCore;
using ReactTest.Models;

namespace ReactTest.Database
{
	public class NodesRepository : INodesRepository
	{
        private readonly DbContext _context;
        private readonly DbSet<Node> _dbSet;

        public NodesRepository(ApplicationContext context)
		{
			_context = context;
			_dbSet = context.Nodes;
		}

		public Node GetTree(string treeName)
		{
            List<Node> all = _dbSet.Include(x => x.Parent).ToList();
            if (!all.Any(x => x.Name == treeName && x.ParentId == null))
            {
                return null;
            }
            TreeHelper.ITree<Node> virtualRootNode = all.ToTree((parent, child) => child.ParentId == parent.Id);
            List<TreeHelper.ITree<Node>> flattenedListOfNodes = virtualRootNode.Children.Flatten(node => node.Children).ToList();
            TreeHelper.ITree<Node> node = flattenedListOfNodes.First(node => node.Data.Name == treeName && node.Data.ParentId == null);

            return node.Data;
        }

        public Node GetById(int id)
        {
            return
                _dbSet.Include(x => x.Parent)
                        .ThenInclude(x => x.Children)
                    .Where(x => x.Id == id)
                    .FirstOrDefault();
        }

        public List<Node> GetAllRoots()
        {
            return _dbSet.Where(x => x.ParentId == null).ToList();
        }

        public void Add(Node node)
        {
            _dbSet.Add(node);
            _context.SaveChanges();
        }

        public void Update(Node node)
        {
            _context.Entry(node).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Node node)
        {
            _dbSet.Remove(node);
            _context.SaveChanges();
        }

        public bool CheckIfNodeBelongsToTree(string treeName, int nodeId)
        {
            List<Node> all = _dbSet.Include(x => x.Parent).ToList();
            TreeHelper.ITree<Node> virtualRootNode = all.ToTree((parent, child) => child.ParentId == parent.Id);
            List<TreeHelper.ITree<Node>> flattenedListOfNodes = virtualRootNode.Children.Flatten(node => node.Children).ToList();
            TreeHelper.ITree<Node> node = flattenedListOfNodes.First(node => node.Data.Id == nodeId);

            while (true)
            {
                if (node?.Parent?.Data == null)
                {
                    return node?.Data.Name == treeName;
                }
                node = node.Parent;
            }
        }
    }
}

