using System;
namespace ReactTest.Models.OutputModels
{
	public class ExceptionResponseModel
	{
		public ExceptionResponseModel()
		{
		}

		public string Type { get; set; }
		public int Id { get; set; }
		public DataItem Data { get; set; }
	}

    public class DataItem
    {
        public string Message { get; set; }
    }
}

