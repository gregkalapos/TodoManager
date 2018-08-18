using System;
using Pom.iOS.Services;
using ToDoManager;
using ToDoManager.ViewModels;
using UIKit;

namespace Pom.iOS.ViewControllers
{
	public partial class NewItemViewController : UIViewController
	{
		partial void SaveButton_TouchUpInside(UIButton sender)
		{
			_vm.NewItemTitle = TitleTextField.Text;
			_vm.NewItemDescription = DescriptionTextField.Text;
			_vm.SaveButtonTouched.Execute(null);
		}

		NewItemViewModel _vm;
		
		public NewItemViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			_vm = new NewItemViewModel(new IosNavigation(this), new CloudTodoDataStore());

		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

