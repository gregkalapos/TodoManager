using System;
namespace ToDoManager.Data.Entities
{
	public class PomodoroItem
	{
		public Guid Id { get; set; }
		public int LengthInSec { get; set; }
		public ToDoItem ToDoItem { get; set; }
		/// <summary>
		/// When the pomodoro was finished
		/// </summary>
		public DateTime DateTimeInUtc { get; set; }
		public int NumberOfInterruptions { get; set; }

		public Boolean Aborted { get; set; }
	}
}
