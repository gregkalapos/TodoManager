using System;
using System.Threading.Tasks;
using ToDoManager.ClientShared.Services;


namespace ToDoManager.Services
{
	public class XamarinFormsNavigation: INavigation
	{
		private Xamarin.Forms.INavigation _innerNavigation;

		public XamarinFormsNavigation(Xamarin.Forms.INavigation navigation)
		{
			_innerNavigation = navigation;
		}

		public async Task PopAsync()
		{
			await _innerNavigation.PopAsync();
		}
	}
}
