using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ToDoManager.Model;

namespace ToDoManager.Services
{
    public class CloudPomodoroDataStorage : IPomodoroDataStorage
    {
        HttpClient client;

        public CloudPomodoroDataStorage()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.BackendUrl}/");

        }

        public async Task<PomodoroItemModel> AddNewPomodoroItemAsync(PomodoroItemModel newItem)
        {
			if (newItem == null)
				throw new Exception("Cannot send null to the backend");

            var serializedItem = JsonConvert.SerializeObject(newItem);

            var response = await client.PostAsync($"api/PomodoroItems", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            if(response.IsSuccessStatusCode)
			{
				var strRet = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<PomodoroItemModel>(strRet);
			}

			throw new Exception("Failed Adding new item");
        }

		public async Task<List<PomodoroItemModel>> GetPomodorosForItem(Guid todoItemGuid)
		{
			var response = await client.GetAsync($"api/PomodoroItems/GetPomodoroForTodo/{todoItemGuid}");

			if (response.IsSuccessStatusCode)
			{
				var strResponse = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<List<PomodoroItemModel>>(strResponse);
			}

			throw new Exception($"Failed loading Pomodoro Items for {todoItemGuid}");
		}
	}
}
