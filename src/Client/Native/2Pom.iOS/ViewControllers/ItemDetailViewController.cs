using System;
using Pom.iOS.Services;
using ToDoManager;
using ToDoManager.Model;
using UIKit;

namespace Pom.iOS.ViewControllers
{
	public partial class ItemDetailViewController : UIViewController
	{
		partial void DoneButton_TouchUpInside(UIButton sender)
		{
			_vm.DoneButtonTouched.Execute(null);

		}

		public ToDoItemModel SelectedToDoItem
		{
			get;
			set;
		}
		ItemDetailViewModel _vm;
		public ItemDetailViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			_vm = new ItemDetailViewModel(new IosNavigation(this), new CloudDataStore(), SelectedToDoItem);
			Title = _vm.SelectedItem.Title;
				// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

