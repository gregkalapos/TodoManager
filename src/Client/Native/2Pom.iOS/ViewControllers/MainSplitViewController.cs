using System;

using UIKit;

namespace Pom.iOS.ViewControllers
{
	public partial class MainSplitViewController : UISplitViewController
	{
		protected AllCategoriesTableViewController masterView;
		protected UINavigationController detailView;

		public MainSplitViewController(IntPtr handle) : base(handle)
		{


		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			masterView = Storyboard.InstantiateViewController("AllCategoriesTableViewController") as AllCategoriesTableViewController;
			detailView = Storyboard.InstantiateViewController("RootNavigationController") as UINavigationController; 
			ViewControllers = new UIViewController[] { masterView, detailView };
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}


	}
}

