using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2Pom.Uwp.Views;
using ToDoManager;
using ToDoManager.ClientShared.Services;
using ToDoManager.ClientShared.Services.LocalData;
using ToDoManager.Model;
using ToDoManager.ViewModels;
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
			this.viewModelLocator.ItemDetailViewModel = new ItemDetailViewModel(new UwpNavigationService(this.viewModelLocator), new LocalTodoDataStore(), selectedItem);
			ServiceContainer.MainFrame.Navigate(typeof(ItemDetailPage));
		}

		public async Task PopAsync()
		{
			await ServiceContainer.MainFrame.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
			 {
				 ServiceContainer.MainFrame.GoBack();
			 });
		}

		public void GoToPomodoroPage(ToDoItemModel selectedItem)
		{
			viewModelLocator.PomodoroViewModel = new PomodoroViewModel(selectedItem, new UwpNavigationService(viewModelLocator));
			ServiceContainer.MainFrame.Navigate(typeof(PomodoroPage));
		}
	}
}
