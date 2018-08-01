using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using ToDoManager.Model;
using System.Collections.Generic;
using ToDoManager.ClientShared.Tools;
using ToDoManager.ViewModels;
using ToDoManager.ClientShared;
using ToDoManager.ClientShared.Services;

namespace ToDoManager
{
	public class ItemsViewModel : BaseViewModel
	{
		private INavigation _navigation;

		public ObservableCollection<ToDoItemModel> ToDoItems { get; private set; }

		/// <summary>
		/// Loads the default item list (relevant, not done)
		/// </summary>
		/// <value>The load items command.</value>
		public Command LoadItemsCommand { get; }

		/// <summary>
		/// Loads items that are done
		/// </summary>
		/// <value>The load done items command.</value>
		public Command LoadDoneItemsCommand { get; }

		/// <summary>
		/// Loads every item (both done and not done)
		/// </summary>
		/// <value>The load all items command.</value>
		public Command LoadAllItemsCommand { get; }

		public Command AddNewButtonTocuhed { get; }

		private IDataStore<ToDoItemModel> _dataStore;

		public ItemsViewModel(IDataStore<ToDoItemModel> dataStore, INavigation navigation)
		{
			_dataStore = dataStore;
			_navigation = navigation;
			Title = "Browse";
			ToDoItems = new ObservableCollection<ToDoItemModel>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(() => _dataStore.GetItemsAsync(true)));
			LoadDoneItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(() => _dataStore.GetDoneTodoItems()));
			LoadAllItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(() => _dataStore.GetAllTodoItems()));

			MessagingCenter.Subscribe<NewItemViewModel, ToDoItemModel>(Consts.AddNewToDoItemStr, (newTodoItem) =>
			{
				if(newTodoItem is ToDoItemModel todoItem)
					ToDoItems.Insert(0, todoItem);
			});

			MessagingCenter.Subscribe<ItemDetailViewModel, ToDoItemModel>(Consts.DoneTodoItemStr, (finishedItemMsg) =>
			{
				if (finishedItemMsg is ToDoItemModel finishedItem)
				{
					var itemToRemove = ToDoItems.Where(n => n.Id == finishedItem.Id).FirstOrDefault();

					if (itemToRemove != null)
					{
						ToDoItems.Remove(itemToRemove);
					}
				}
			});

			AddNewButtonTocuhed = new Command(() =>
			{
				_navigation.GoToNewItemPage();
			});
		}

		async Task ExecuteLoadItemsCommand(Func<Task<IEnumerable<ToDoItemModel>>> downloadMethod)
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				ToDoItems.Clear();
				var items = await downloadMethod();
				foreach (var item in items)
				{
					ToDoItems.Add(item);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
