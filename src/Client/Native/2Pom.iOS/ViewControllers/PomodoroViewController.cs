using System;
using Foundation;
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
			PomodoroLengthPicker.Model = new PomodoroLengthPickerDataSource(_vm);

			Title = "Pomodoro";
			TimeRemainingLabel.Text = _vm.TimeLeftText;
			StartPomodoroButton.TitleLabel.Text = _vm.StartStopButtonText;

			_vm.PropertyChanged += (sender, e) => 
			{
				switch (e.PropertyName)
				{
					case nameof(_vm.TimeLeftText):
						TimeRemainingLabel.Text = _vm.TimeLeftText;
						break;
					case nameof(_vm.StartStopButtonText):
			 			StartPomodoroButton.SetTitle(_vm.StartStopButtonText, UIControlState.Normal);
						break;
					default:
						break;
				}
			};
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}
	}

	public class PomodoroLengthPickerDataSource : UIPickerViewModel
	{
		private PomodoroViewModel _vm;
		public PomodoroLengthPickerDataSource(PomodoroViewModel pomodoroViewController)
		 => _vm = pomodoroViewController;

		public override nint GetComponentCount(UIPickerView pickerView) => 1;

		public override nint GetRowsInComponent(UIPickerView pickerView, nint component) 
		=> _vm.PomodoroLengthOptions.Count;

		public override string GetTitle(UIPickerView pickerView, nint row, nint component)
		=> _vm.PomodoroLengthOptions[(int)row];

		public override void Selected(UIPickerView pickerView, nint row, nint component)
		{
			_vm.SelectedPomodoroLength = _vm.PomodoroLengthOptions[(int)row];
		}
	}
}

