using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoManager;

namespace _2Pom.Uwp
{
	public class ViewModelLocator
	{
		public ViewModelLocator()
		{
			ItemsViewModel = new ItemsViewModel(new CloudDataStore());
		}

		public ItemsViewModel ItemsViewModel { get; set; }
	}
}
