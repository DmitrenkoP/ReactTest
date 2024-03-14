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
    public class JournalController : ControllerBase
	{
		private IJournalRepository repository;
		public JournalController(IJournalRepository repository)
		{
			this.repository = repository;
		}

        [Route("getSingle")]
        [HttpPost]
        public StoredException GetSingle(GetSingleJournalEntryInputModel model)
        {
            var entry = repository.GetById(model.Id);
            if (entry == null)
            {
                throw new SecureException("Entry not found");
            }

            return entry;
        }

        [Route("getRange")]
        [HttpPost]
        public RangeOutputModel GetRange(GetRangeInputModel model)
        {
            var entries = repository.GetRange(model);

            var count = repository.Count();

            var skipped = entries.Skip(model.Skip).Take(model.Take);

            var outputModel = new RangeOutputModel()
            {
                Skip = model.Skip,
                Count = count
            };
            if (skipped.Any())
            {
                outputModel.Items = skipped.Select(x => new JournalEntryListItemModel() { Id = x.Id, CreatedAt = x.CreatedAt });
            }
            else
            {
                outputModel.Items = null;
            }

            return outputModel;
        }
    }
}

