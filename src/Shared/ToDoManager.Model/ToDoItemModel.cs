using System;
namespace ToDoManager.Model
{
	public class ToDoItemModel
	{
		public Guid Id { get; set; }
		public String Title { get; set; }
		public String Description { get; set; }
		public DateTime Created { get; set; }
		public Guid User { get; set; }
		public Boolean IsDone { get; set; }
		public DateTime FinishedDate { get; set; }
	}
}
