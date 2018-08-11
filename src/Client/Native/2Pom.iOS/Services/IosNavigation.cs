﻿using System;
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
			throw new NotImplementedException();
		}

		public void GoToNewItemPage()
		{
			throw new NotImplementedException();
		}

		public Task PopAsync()
		{
			_viewController.NavigationController.PopViewController(true);
			return Task.FromResult(0);
		}
	}
}
