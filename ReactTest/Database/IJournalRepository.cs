using System;
using ReactTest.Models;
using ReactTest.Models.InputModels;

namespace ReactTest.Database
{
	public interface IJournalRepository
	{
        List<StoredException> GetRange(GetRangeInputModel model);

        StoredException GetById(int id);

        void Add(StoredException exception);

        int Count();
    }
}

