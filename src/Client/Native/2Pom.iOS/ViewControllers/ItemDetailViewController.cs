using System;
using Foundation;
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

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			if(segue.Identifier == "StartPomodoroButtonTouchedSegue")
			{
				if (segue.DestinationViewController is PomodoroViewController targetVm ) 
				{
					targetVm.SelectedTodoItem = SelectedToDoItem;
				}
			}
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			_vm = new ItemDetailViewModel(new IosNavigation(this), new CloudDataStore(), SelectedToDoItem);
			Title = _vm.SelectedItem.Title;
			TitleLabel.Text = _vm.SelectedItem.Title;
			DescriptionTextView.Text = _vm.SelectedItem.Description;
			CreatedDateLabel.Text = _vm.SelectedItem.Created.ToString();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

