using System;
using ToDoManager.Model;

using ToDoManager.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using ToDoManager.ViewModels;
using ToDoManager.ClientShared.Services;
using ToDoManager.ClientShared.Tools;

namespace ToDoManager
{
	public class ItemDetailViewModel : BaseViewModel
	{
		private IDataStore<ToDoItemModel> DataStore; //TODO
		INavigation _navigation;
		public ToDoItemModel SelectedItem { get; set; }

		//public BarChart PomodoroChart { get; set; }

		private IPomodoroDataStorage pomodoroDataStorage = new CloudPomodoroDataStorage();

		private int numberOfPomodoros;

		public Command DoneButtonTouched { get; }

		public int NumberOfPomodoros
		{
			get { return numberOfPomodoros; }
			set
			{
				SetProperty(ref numberOfPomodoros, value);
			}
		}

		private List<PomodoroItemModel> pomodoros = new List<PomodoroItemModel>();
		public List<PomodoroItemModel> Pomodoros
		{
			get { return pomodoros; }
			set
			{
				pomodoros = value;
				if (pomodoros != null)
				{
					NumberOfPomodoros = pomodoros.Count;
				}
			}
		}

		public ItemDetailViewModel(INavigation navigation, ToDoItemModel item = null)
		{
			//_navigation = navigation;
			//PomodoroChart = new BarChart() { Entries = new List<Entry>() };
			Title = item?.Title;
			SelectedItem = item;
			PopulateChart();

			//Xamarin.Forms.MessagingCenter.Subscribe<PomodoroViewModel, PomodoroItemModel>(this, Consts.AddNewToDoItemStr, (obj, newPomodoroItem) =>
			//{
			//	Pomodoros.Add(newPomodoroItem);
			//	NumberOfPomodoros++;
			//});

			DoneButtonTouched = new Command(async () =>
			{
				var newItem = await DataStore.SetDoneTodo(SelectedItem.Id);

				//Xamarin.Forms.MessagingCenter.Send(this, Consts.DoneTodoItemStr, newItem);
				await _navigation.PopAsync();	
			});
		}

		private async void PopulateChart()
		{
			try
			{
				Pomodoros = await pomodoroDataStorage.GetPomodorosForItem(SelectedItem.Id);
			}
			catch
			{
				//TODO
			}

			//var entries = new[]
			//{
			//	new Entry(200)
			//	{
			//		Label = "January",
			//		ValueLabel = "200",
			//		Color = SKColor.Parse("#266489")
			//	},
			//	new Entry(400)
			//	{
			//	Label = "February",
			//	ValueLabel = "400",
			//	Color = SKColor.Parse("#68B9C0")
			//	},
			//	new Entry(-100)
			//	{
			//	Label = "March",
			//	ValueLabel = "-100",
			//	Color = SKColor.Parse("#90D585")
			//	}
			//};

			//PomodoroChart.Entries = entries;
		}
	}
}
