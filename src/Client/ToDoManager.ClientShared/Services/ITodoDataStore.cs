using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoManager.Model;

namespace ToDoManager
{
	public interface ITodoDataStore
	{
		Task<ToDoItemModel> AddItemAsync(ToDoItemModel item);
		Task<ToDoItemModel> SetDoneTodo(Guid id);
		Task<bool> DeleteItemAsync(Guid id);
		Task<ToDoItemModel> GetItemAsync(Guid id);
		Task<IEnumerable<ToDoItemModel>> GetItemsAsync(bool forceRefresh = false);

		Task<IEnumerable<ToDoItemModel>> GetAllTodoItems();
		Task<IEnumerable<ToDoItemModel>> GetDoneTodoItems();
	}
}
