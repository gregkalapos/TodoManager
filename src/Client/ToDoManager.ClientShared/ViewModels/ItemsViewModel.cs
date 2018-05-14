using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using ToDoManager.Model;
using ToDoManager.ViewModels;
using Xamarin.Forms;
using System.Collections.Generic;

namespace ToDoManager
{
	public class ItemsViewModel : BaseViewModel
	{
		public ObservableCollection<ToDoItemModel> Items { get; set; }

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

		public ItemsViewModel()
		{
			Title = "Browse";
			Items = new ObservableCollection<ToDoItemModel>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(() => DataStore.GetItemsAsync(true)));
			LoadDoneItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(() => DataStore.GetDoneTodoItems()));
			LoadAllItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(() => DataStore.GetAllTodoItems()));

			MessagingCenter.Subscribe<NewItemViewModel, ToDoItemModel>(this, Consts.AddNewToDoItemStr, (obj, newTodoItem) =>
			{
				Items.Insert(0, newTodoItem);
			});

			MessagingCenter.Subscribe<ItemDetailViewModel, ToDoItemModel>(this, Consts.DoneTodoItemStr, (obj, finishedItem) =>
			{
				var itemToRemove = Items.Where(n => n.Id == finishedItem.Id).FirstOrDefault();

				if(itemToRemove != null)
				{
					Items.Remove(itemToRemove);
				}
			});
		}

		async Task ExecuteLoadItemsCommand(Func<Task<IEnumerable<ToDoItemModel>>> downloadMethod)
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				Items.Clear();
				var items = await downloadMethod();
				foreach (var item in items)
				{
					Items.Add(item);
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
