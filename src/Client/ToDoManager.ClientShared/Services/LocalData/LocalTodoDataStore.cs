﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoManager.ClientShared.LocalData;
using System.Linq;
using ToDoManager.Model;
using AutoMapper;

namespace ToDoManager.ClientShared.Services.LocalData
{
	public class LocalTodoDataStore : ITodoDataStore
	{
		private ToDoDataContext toDoDataContext = new ToDoDataContext();

		public async Task<ToDoItemModel> AddItemAsync(ToDoItemModel item)
		{
			var newEntity = Mapper.Map<TodoItemEntity>(item);
			newEntity.Operation = ClientShared.LocalData.Sync.SyncOperation.Insert;

			var tracker = toDoDataContext.Todos.Add(newEntity);

			var val = await toDoDataContext.SaveChangesAsync();
			return tracker.Entity;
		}

		public async Task<bool> DeleteItemAsync(Guid id)
		{
			var itemToRemove = toDoDataContext.Todos.Where(n => n.Id == id).FirstOrDefault();

			if (itemToRemove == null)
				throw new Exception($"{nameof(LocalTodoDataStore)}.{nameof(DeleteItemAsync)} called with a non existing id");

			toDoDataContext.Remove(itemToRemove);
			//TODO: Remove related entities

			var res = await toDoDataContext.SaveChangesAsync();
			return res == 1;
		}

		public Task<IEnumerable<ToDoItemModel>> GetAllTodoItems()
		 => Task.FromResult(toDoDataContext.Todos.ToList() as IEnumerable<ToDoItemModel>);

		public Task<IEnumerable<ToDoItemModel>> GetDoneTodoItems()
		=> Task.FromResult(toDoDataContext.Todos.Where(n => n.IsDone).ToList() as IEnumerable<ToDoItemModel>);

		public Task<ToDoItemModel> GetItemAsync(Guid id)
		=> Task.FromResult(toDoDataContext.Todos.Where(n => n.Id == id) as ToDoItemModel);

		public Task<IEnumerable<ToDoItemModel>> GetItemsAsync(bool forceRefresh = false)
		=> Task.FromResult(toDoDataContext.Todos.ToList() as IEnumerable<ToDoItemModel>);

		public async Task<ToDoItemModel> SetDoneTodo(Guid id)
		{
			var dbItem = toDoDataContext.Todos.Where(n => n.Id == id).FirstOrDefault();

			if(dbItem == null)
			{
				throw new Exception($"{nameof(LocalTodoDataStore)}.{nameof(SetDoneTodo)} called with an id that is not contained in the database");
			}

			dbItem.IsDone = true;
			dbItem.FinishedDate = DateTime.UtcNow;

			await toDoDataContext.SaveChangesAsync();

			return dbItem;
		}
	}
}
