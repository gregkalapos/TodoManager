using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoManager.Model;

namespace ToDoManager.Services
{
	public interface IPomodoroDataStore
	{
		Task<PomodoroItemModel> AddNewPomodoroItemAsync(PomodoroItemModel newItem);

		Task<List<PomodoroItemModel>> GetPomodorosForItem(Guid todoItemGuid);
	}
}
