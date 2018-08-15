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

		public ItemDetailViewModel ViewModel => _vm;

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
			if(segue.Identifier == "ShowSchedulePopOverSegue")
			{
				if (segue.DestinationViewController is SchedulePopOverViewController targetVm)
				{
					targetVm.ItemDetailViewController = this;
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

			if(_vm.SelectedItem.IsDone)
			{
				FinishedValueLabel.Text = _vm.SelectedItem.FinishedDate.ToString();

				DoneButton.Hidden = true;
				StartPomodoroButton.Hidden = true;
			}
			else
			{
				FinishedValueLabel.Hidden = true;
				FinishedLabel.Hidden = true;
			}

			_vm.PropertyChanged += (sender, e) =>
			{
				InvokeOnMainThread(() =>
				{
					switch (e.PropertyName)
					{
						case nameof(_vm.NumberOfPomodoros):
							NumberOfPomodorosValueLable.Text = _vm.NumberOfPomodoros.ToString();
							break;
						case nameof(_vm.MinsOfPomodoros):
							NumberOfMinutesValueLabel.Text = _vm.MinsOfPomodoros.ToString();
							break;
						default:
							break;
					}
				});
			};
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

