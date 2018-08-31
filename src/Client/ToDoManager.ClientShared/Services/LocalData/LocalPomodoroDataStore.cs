using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoManager.ClientShared.LocalData;
using ToDoManager.Model;
using ToDoManager.Services;

namespace ToDoManager.ClientShared.Services.LocalData
{
	public class LocalPomodoroDataStore : IPomodoroDataStore
	{
		private ToDoDataContext toDoDataContext = new ToDoDataContext();

		public async Task<PomodoroItemModel> AddNewPomodoroItemAsync(PomodoroItemModel newItem)
		{
			var newEntity = Mapper.Map<PomodoroItemEntity>(newItem);
			newEntity.Operation = ClientShared.LocalData.Sync.SyncOperation.Insert;

			var tracker = toDoDataContext.PomodoroItems.Add(newEntity);

			var val = await toDoDataContext.SaveChangesAsync();
			return tracker.Entity;
		}

		public async Task<List<PomodoroItemModel>> GetPomodorosForItem(Guid todoItemGuid)
		{
		 	var items = await toDoDataContext.PomodoroItems.Where(n => n.ToDoItemGuid == todoItemGuid).ToListAsync();

			return items.Select(n => n as PomodoroItemModel).ToList();
		} 
	}
}
