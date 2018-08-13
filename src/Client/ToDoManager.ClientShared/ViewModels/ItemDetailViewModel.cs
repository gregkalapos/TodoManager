using System;
using ToDoManager.Model;
using System.Linq;

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

		public String Title { get { return _selectedItem.Title; } }
		public String Description { get { return _selectedItem.Description; } }

		//public BarChart PomodoroChart { get; set; }

		public Command BackButtonTouched { get; }

		private IPomodoroDataStorage pomodoroDataStorage = new CloudPomodoroDataStorage();

		public Command DoneButtonTouched { get; }

		private int numberOfPomodoros;
		public int NumberOfPomodoros
		{
			get { return numberOfPomodoros; }
			set
			{
				SetProperty(ref numberOfPomodoros, value);
			}
		}

		private int minsOfPomodoros;
		public int MinsOfPomodoros
		{
			get { return minsOfPomodoros; }
			set
			{
				SetProperty(ref minsOfPomodoros, value);
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
					MinsOfPomodoros = pomodoros.Sum(n => (n.LengthInSec / 60));
				}
				else
				{
					NumberOfPomodoros = 0;
					MinsOfPomodoros = 0;
				}
			}
		}

		public ItemDetailViewModel(INavigation navigation, IDataStore<ToDoItemModel> dataStore, ToDoItemModel item = null)
		{
			_dataStore = dataStore;
			_navigation = navigation;
			//PomodoroChart = new BarChart() { Entries = new List<Entry>() };
			_selectedItem = item;
			PopulateChart();

			MessagingCenter.Subscribe<PomodoroViewModel, PomodoroItemModel>(Consts.AddNewToDoItemStr, (newPomodoroItem) =>
			{
				if (newPomodoroItem is PomodoroItemModel newItem)
				{
					Pomodoros.Add(newItem);
					NumberOfPomodoros++;
					MinsOfPomodoros += (newItem.LengthInSec / 60);
				}
			});

			DoneButtonTouched = new Command(async () =>
			{
				var newItem = await _dataStore.SetDoneTodo(_selectedItem.Id);

				MessagingCenter.Send(this, Consts.DoneTodoItemStr, newItem);
				await _navigation.PopAsync();
			});

			BackButtonTouched = new Command(async () =>
			{
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
				NumberOfPomodoros = 0;
				MinsOfPomodoros = 0;
			}
		}
	}
}
