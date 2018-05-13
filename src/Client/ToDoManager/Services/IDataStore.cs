using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoManager
{
	public interface IDataStore<T>
	{
		Task<T> AddItemAsync(T item);
		Task<T> SetDoneTodo(Guid id);
		Task<bool> DeleteItemAsync(Guid id);
		Task<T> GetItemAsync(Guid id);
		Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);

		Task<IEnumerable<T>> GetAllTodoItems();
		Task<IEnumerable<T>> GetDoneTodoItems();
	}
}
