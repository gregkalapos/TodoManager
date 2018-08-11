﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2Pom.Uwp.Views;
using ToDoManager;
using ToDoManager.ClientShared.Services;
using ToDoManager.Model;
using Windows.UI.Xaml.Controls;

namespace _2Pom.Uwp.Services
{
	class UwpNavigationService : INavigation
	{
		private ViewModelLocator viewModelLocator;

		public UwpNavigationService(ViewModelLocator viewModelLocator)
		{
			this.viewModelLocator = viewModelLocator;
		}

		public void GoToNewItemPage()
		{
			ServiceContainer.MainFrame.Navigate(typeof(NewItemPage));
		}

		public void GoToItemDetailPage(ToDoItemModel selectedItem)
		{
			this.viewModelLocator.ItemDetailViewModel = new ItemDetailViewModel(new UwpNavigationService(this.viewModelLocator), new CloudDataStore(), selectedItem);
			ServiceContainer.MainFrame.Navigate(typeof(ItemDetailPage));
		}

		public async Task PopAsync()
		{
			await ServiceContainer.MainFrame.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
			 {
				 ServiceContainer.MainFrame.GoBack();
			 });
		}
	}
}