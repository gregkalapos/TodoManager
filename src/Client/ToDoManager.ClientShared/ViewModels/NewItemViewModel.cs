﻿using System;
using ToDoManager.ClientShared;
using ToDoManager.ClientShared.Services;
using ToDoManager.ClientShared.Tools;
using ToDoManager.Model;

namespace ToDoManager.ViewModels
{
	public class NewItemViewModel : BaseViewModel
	{
		public String NewItemTitle { get; set; }
		public String NewItemDescription { get; set; }
		public Boolean IsLoading { get; set; }

		public Command SaveButtonTouched { get; }
		public Command CancelButtonTouched { get; }

		private INavigation navigation;
		private ITodoDataStore _dataStore;

		public NewItemViewModel(INavigation navigation, ITodoDataStore dataStore)
		{
			this.navigation = navigation;
			_dataStore = dataStore;
			CancelButtonTouched = new Command(async () => await navigation.PopAsync());
			SaveButtonTouched = new Command(async () =>
			{
				try
				{
					IsLoading = true;

					var newTodoItem = new ToDoItemModel
					{
						Title = NewItemTitle,
						Description = NewItemDescription,
						Created = DateTime.UtcNow,
					};

					try
					{
						var retVal = await _dataStore.AddItemAsync(newTodoItem);
						MessagingCenter.Send(this, Consts.AddNewToDoItemStr, retVal);
						await navigation.PopAsync();
					}
					catch (Exception e)
					{
						//TODO: popup or something like that...
					}
				}
				finally
				{
					IsLoading = false;
				}
			});
		}
	}
}
