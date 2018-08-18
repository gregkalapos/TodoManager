using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoManager.Model;

namespace ToDoManager
{
	public class MockDataStore : ITodoDataStore
	{
		List<ToDoItemModel> items;

		public MockDataStore()
		{
			items = new List<ToDoItemModel>();
			var mockItems = new List<ToDoItemModel>
			{
				new ToDoItemModel { Id = Guid.NewGuid(), Title = "First item", Description="This is an item description." },
				new ToDoItemModel { Id = Guid.NewGuid(), Title = "Second item", Description="This is an item description." },
				new ToDoItemModel { Id = Guid.NewGuid(), Title = "Third item", Description="This is an item description." },
		   };

			foreach (var item in mockItems)
			{
				items.Add(item);
			}
		}

		public async Task<ToDoItemModel> AddItemAsync(ToDoItemModel item)
		{
			items.Add(item);

			return await Task.FromResult(item);
		}

		public async Task<bool> DeleteItemAsync(Guid id)
		{
			var _item = items.Where((ToDoItemModel arg) => arg.Id == id).FirstOrDefault();
			items.Remove(_item);

			return await Task.FromResult(true);
		}

		public Task<IEnumerable<ToDoItemModel>> GetAllTodoItems()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<ToDoItemModel>> GetDoneTodoItems()
		{
			throw new NotImplementedException();
		}

		public async Task<ToDoItemModel> GetItemAsync(Guid id)
		{
			return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
		}

		public async Task<IEnumerable<ToDoItemModel>> GetItemsAsync(bool forceRefresh = false)
		{
			return await Task.FromResult(items);
		}

		public Task<ToDoItemModel> SetDoneTodo(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}
