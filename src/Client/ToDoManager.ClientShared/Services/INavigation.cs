using System;
using System.Threading.Tasks;
using ToDoManager.Model;

namespace ToDoManager.ClientShared.Services
{
	public interface INavigation
	{
		Task PopAsync();

		void GoToNewItemPage();

		void GoToItemDetailPage(ToDoItemModel selectedItem);

		void GoToPomodoroPage(ToDoItemModel selectedItem);
	}
}
