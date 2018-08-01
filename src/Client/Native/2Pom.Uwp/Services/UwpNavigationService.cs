using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2Pom.Uwp.Views;
using ToDoManager.ClientShared.Services;
using Windows.UI.Xaml.Controls;

namespace _2Pom.Uwp.Services
{
	class UwpNavigationService : INavigation
	{
		private readonly Frame frame;

		public void GoToNewItemPage()
		{
			ServiceContainer.MainFrame.Navigate(typeof(NewItemPage));
		}

		public Task PopAsync()
		{
			throw new NotImplementedException();
		}
	}
}
