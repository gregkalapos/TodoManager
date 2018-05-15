using System;
using System.Threading.Tasks;

namespace ToDoManager.ClientShared.Services
{
	public interface INavigation
	{
		Task PopAsync();
	}
}
