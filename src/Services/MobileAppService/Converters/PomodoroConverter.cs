using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoManager.Data.Entities;
using ToDoManager.Model;

namespace ToDoManager.MobileAppService.Converters
{
	public static class PomodoroConverter
	{
		public static PomodoroItemModel FromEntityToModel(PomodoroItem pomodoroItem)
		{
			var retVal = new PomodoroItemModel
			{
				DateTimeInUtc = pomodoroItem.DateTimeInUtc,
				Aborted = pomodoroItem.Aborted,
				LengthInSec = pomodoroItem.LengthInSec,
				NumberOfInterruptions = pomodoroItem.NumberOfInterruptions,
				Id = pomodoroItem.Id
			};

			if (pomodoroItem.ToDoItem != null)
			{
				retVal.ToDoItemGuid = pomodoroItem.ToDoItem.Id;
			}

			return retVal;
		}

		/// <summary>
		/// Does not set ToDoItem!!
		/// </summary>
		/// <param name="pomodoroItemModel"></param>
		/// <returns></returns>
		public static PomodoroItem FromModelToEntity(PomodoroItemModel pomodoroItemModel)
		{
			return new PomodoroItem
			{
				DateTimeInUtc = pomodoroItemModel.DateTimeInUtc,
				Aborted = pomodoroItemModel.Aborted,
				LengthInSec = pomodoroItemModel.LengthInSec,
				NumberOfInterruptions = pomodoroItemModel.NumberOfInterruptions,
				Id = pomodoroItemModel.Id,
			};
		}
	}
}