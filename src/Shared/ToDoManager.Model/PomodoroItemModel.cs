using System;
namespace ToDoManager.Model
{
    public class PomodoroItemModel
    {
        public Guid Id { get; set; }

		/// <summary>
		/// When the pomodoro was finished
		/// </summary>
        public DateTime DateTimeInUtc { get; set; }
        public Guid ToDoItemGuid { get; set; }
        public int LengthInSec { get; set; }
        public int NumberOfInterruptions { get; set; }

		public Boolean Aborted { get; set; }
	}
}
