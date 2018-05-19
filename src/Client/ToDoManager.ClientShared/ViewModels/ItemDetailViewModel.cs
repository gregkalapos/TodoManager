using System;
using ToDoManager.Model;

using ToDoManager.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using ToDoManager.ViewModels;
using ToDoManager.ClientShared.Services;
using ToDoManager.ClientShared.Tools;
using ToDoManager.ClientShared;

namespace ToDoManager
{
	public class ItemDetailViewModel : BaseViewModel
	{
		private IDataStore<ToDoItemModel> _dataStore;
		private INavigation _navigation;
		private ToDoItemModel _selectedItem { get; set; }

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

		public ItemDetailViewModel(INavigation navigation, IDataStore<ToDoItemModel> dataStore, ToDoItemModel item = null)
		{
			_dataStore = dataStore;
			_navigation = navigation;
			//PomodoroChart = new BarChart() { Entries = new List<Entry>() };
			Title = item?.Title;
			_selectedItem = item;
			PopulateChart();

			MessagingCenter.Subscribe<PomodoroViewModel, PomodoroItemModel>(Consts.AddNewToDoItemStr, (newPomodoroItem) =>
			{
				if (newPomodoroItem is PomodoroItemModel newItem)
				{
					Pomodoros.Add(newItem);
					NumberOfPomodoros++;
				}
			});

			DoneButtonTouched = new Command(async () =>
			{
				var newItem = await _dataStore.SetDoneTodo(_selectedItem.Id);

				MessagingCenter.Send(this, Consts.DoneTodoItemStr, newItem);
				await _navigation.PopAsync();	
			});
		}

		public ToDoItemModel SelectedItem => this._selectedItem;

		private async void PopulateChart()
		{
			try
			{
				Pomodoros = await pomodoroDataStorage.GetPomodorosForItem(_selectedItem.Id);
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
