using System;
namespace ReactTest.Models.OutputModels
{
	public class RangeOutputModel
	{
		public int Skip { get; set; }
		public int Count { get; set; }
		public IEnumerable<JournalEntryListItemModel> Items { get; set; }
	}
}

