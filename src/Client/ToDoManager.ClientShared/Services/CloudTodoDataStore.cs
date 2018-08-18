using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

using Newtonsoft.Json;
using ToDoManager.Model;
using ToDoManager.ClientShared;

namespace ToDoManager
{
	public class CloudTodoDataStore : ITodoDataStore
	{
		HttpClient client;
		IEnumerable<ToDoItemModel> items;

		public CloudTodoDataStore()
		{
			client = new HttpClient();
			client.BaseAddress = new Uri($"{Consts.BackendUrl}/");

			items = new List<ToDoItemModel>();
		}

		public async Task<IEnumerable<ToDoItemModel>> GetItemsAsync(bool forceRefresh = false)
		{
			if (forceRefresh)
			{
				var json = await client.GetStringAsync($"api/item");
				items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<ToDoItemModel>>(json));
			}

			foreach (var item in items)
			{
				item.Created = item.Created.ToLocalTime();
				if(item.IsDone)
				{
					item.FinishedDate = item.FinishedDate.ToLocalTime();
				}
			}

			return items.OrderByDescending(n => n.Created).ToList();
		}

		public async Task<ToDoItemModel> GetItemAsync(Guid id)
		{
			if (id != null)
			{
				var json = await client.GetStringAsync($"api/item/{id}");
				return await Task.Run(() => JsonConvert.DeserializeObject<ToDoItemModel>(json));
			}

			return null;
		}

		public async Task<ToDoItemModel> AddItemAsync(ToDoItemModel item)
		{
			if (item == null)
				throw new Exception("Cannot send null to backend");

			var serializedItem = JsonConvert.SerializeObject(item);

			var response = await client.PostAsync($"api/Item/AddNewTodoItem", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
			return await ProcessResponse(response);
	
		}

		private async Task<ToDoItemModel> ProcessResponse(HttpResponseMessage response)
		{
			if (response.IsSuccessStatusCode)
			{
				var strRet = await response.Content.ReadAsStringAsync();
				try
				{
					var retVal = JsonConvert.DeserializeObject<ToDoItemModel>(strRet);
					retVal.Created = retVal.Created.ToLocalTime();
					retVal.FinishedDate = retVal.FinishedDate.ToLocalTime();
					return retVal;
				}
				catch (Exception)
				{
					throw new Exception("Failed adding item - deserialization failed");
				}
			}
			else
			{
				throw new Exception("Failed adding item");
			}
		}

		public async Task<ToDoItemModel> SetDoneTodo(Guid itemGuid)
		{
			if (itemGuid == Guid.Empty)
				throw new Exception("Cannot set TodoItem with empty guid to 'done'");

			var serializedItem = JsonConvert.SerializeObject(itemGuid);
			var response = await client.PostAsync("api/Item/TodoItemDone", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
			return await ProcessResponse(response);
		}

		public async Task<bool> DeleteItemAsync(Guid id)
		{
			if (id == null)
				return false;

			var response = await client.DeleteAsync($"api/item/{id}");
			return response.IsSuccessStatusCode;
		}

		public async Task<IEnumerable<ToDoItemModel>> GetAllTodoItems()
		{
			var json = await client.GetStringAsync($"api/item/GetAllTodoItems");
			return await Task.Run(() => JsonConvert.DeserializeObject<List<ToDoItemModel>>(json)
			                             .OrderByDescending(n => n.Created).ToList());
		}

		public async Task<IEnumerable<ToDoItemModel>> GetDoneTodoItems()
		{
			var json = await client.GetStringAsync($"api/item/GetDoneTodoItems");
			return await Task.Run(() => JsonConvert.DeserializeObject<List<ToDoItemModel>>(json)
			                      .OrderByDescending(n => n.Created).ToList());
		}
	}
}
