using System;
using System.Collections.Generic;

namespace ToDoManager.Data.Entities
{
	public class ToDoItem
	{
		public Guid Id { get; set; }
		public String Title { get; set; }
		public String Description { get; set; }
		public DateTime Created { get; set; }
		public Guid User { get; set; }
		public Boolean IsDone { get; set; }
		public DateTime FinishedDate { get; set; }

		public List<PomodoroItem> Pomodoros
		{
			get;
			set;
		}
	}
}
