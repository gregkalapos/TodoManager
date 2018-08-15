using System;
using UIKit;

namespace Pom.iOS.ViewControllers
{
	public partial class SchedulePopOverViewController : UIViewController
	{
		partial void UIButton21630_TouchUpInside(UIButton sender)
		{
			if (ItemDetailViewController.ViewModel != null)
			{
				DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(
								   new DateTime(2001, 1, 1, 0, 0, 0));

				ItemDetailViewController.ViewModel.SelectedScheduleDateTime = reference.AddSeconds(ScheduleDatePicker.Date.SecondsSinceReferenceDate);

				ItemDetailViewController?.ViewModel.SaveScheduleButtonTouched.Execute(null);
			}

			DismissModalViewController(true);
		}

		public ItemDetailViewController ItemDetailViewController
		{
			get;
			set;
		}

		public SchedulePopOverViewController(IntPtr handle) : base(handle) { }


	}
}