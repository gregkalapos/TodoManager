using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2Pom.Uwp.Services;
using ToDoManager;
using Windows.UI.Xaml.Controls;

namespace _2Pom.Uwp
{
	public class ViewModelLocator
	{
		public ViewModelLocator()
		{
			ItemsViewModel = new ItemsViewModel(new CloudDataStore(), new UwpNavigationService(this));
			ItemDetailViewModel = new ItemDetailViewModel(new UwpNavigationService(this), new CloudDataStore());
		}

		public ItemsViewModel ItemsViewModel { get; set; }

		public ItemDetailViewModel ItemDetailViewModel { get; set; }
	}
}
