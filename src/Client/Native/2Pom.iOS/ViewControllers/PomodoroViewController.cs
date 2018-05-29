using System;
using Pom.iOS.Services;
using ToDoManager.Model;
using ToDoManager.ViewModels;
using UIKit;

namespace Pom.iOS.ViewControllers
{
	public partial class PomodoroViewController : UIViewController
	{
		partial void StartPomodoroButton_TouchUpInside(UIButton sender)
		{
			_vm.StartButtonTouched.Execute(null);
		}

		PomodoroViewModel _vm;
		public ToDoItemModel SelectedTodoItem { get; set; } 
		public PomodoroViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			_vm = new PomodoroViewModel(SelectedTodoItem, new IosNavigation(this));
			Title = "Pomodoro";

			_vm.PropertyChanged += (sender, e) => 
			{
				if(e.PropertyName == nameof(_vm.TimeLeftText))
				{
					TimeRemainingLabel.Text = _vm.TimeLeftText;
				}
			};
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

