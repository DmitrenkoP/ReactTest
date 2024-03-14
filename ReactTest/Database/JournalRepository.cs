using System;
using Microsoft.EntityFrameworkCore;
using ReactTest.Models;
using ReactTest.Models.InputModels;

namespace ReactTest.Database
{
	public class JournalRepository : IJournalRepository
	{
        private readonly DbContext _context;
        private readonly DbSet<StoredException> _dbSet;

        public JournalRepository(ApplicationContext context)
		{
            _context = context;
            _dbSet = context.Exceptions;
        }

        public List<StoredException> GetRange(GetRangeInputModel model)
        {
            return
                _dbSet.Where(x =>
                x.CreatedAt >= model.From && x.CreatedAt <= model.To
                && (x.Path.Contains(model.Search) || x.Body.Contains(model.Search) || x.StackTrace.Contains(model.Search)))
                    .ToList();
        }

        public StoredException GetById(int id)
        {
            return
                _dbSet.Where(x => x.Id == id)
                    .FirstOrDefault();
        }

        public void Add(StoredException exception)
        { 
            _dbSet.Add(exception);
            _context.SaveChanges();
        }

        public int Count()
        {
            return _dbSet.Count();
        }
	}
}

