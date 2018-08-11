using System;
using System.Threading.Tasks;
using ToDoManager.ClientShared.Services;
using ToDoManager.Model;
using UIKit;

namespace Pom.iOS.Services
{
	public class IosNavigation: INavigation
	{
		UIViewController _viewController;

		public IosNavigation(UIViewController viewController)
		{
			_viewController = viewController;
		}

		public void GoToItemDetailPage(ToDoItemModel selectedItem)
		{
			_viewController.PerformSegue("ToDoItemSelectedSegue", _viewController);
		}

		public void GoToNewItemPage()
		{
			//this one is implemented with a segue
		}

		public Task PopAsync()
		{
			_viewController.NavigationController.PopViewController(true);
			return Task.FromResult(0);
		}
	}
}
