using System;
using ToDoManager.ClientShared.ViewModels;
using UIKit;

namespace Pom.iOS.ViewControllers
{
	public partial class AuthenticationViewController : UIViewController
	{

		AuthenticationViewModel _vm;

		public AuthenticationViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			_vm = new AuthenticationViewModel();
		}

		partial void UIButton18484_TouchUpInside(UIButton sender)
		{
			_vm.StartAuthentication();

			PresentViewController(_vm.OAuth2Authenticator.GetUI(),true, null);
			_vm.AuthCompleted += (s, e) => 
			{
				this.DismissViewController(true, null);
			};
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

