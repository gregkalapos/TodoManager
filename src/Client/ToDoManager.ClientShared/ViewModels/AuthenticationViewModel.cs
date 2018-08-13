using System;
using Xamarin.Auth;

namespace ToDoManager.ClientShared.ViewModels
{
	public class AuthenticationViewModel : BaseViewModel
	{
		public AuthenticationViewModel()
		{
		}

		public event EventHandler<AuthenticatorCompletedEventArgs> AuthCompleted;

		OAuth2Authenticator auth;
		public OAuth2Authenticator OAuth2Authenticator => auth;
	
		public void StartAuthentication()
		{
			auth = new OAuth2Authenticator
			(
				clientId: "App ID from https://developers.facebook.com/apps",
				scope: "",
				authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
				redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"),
				isUsingNativeUI: false
			);

			auth.Completed += (sender, eventArgs) =>
			{
				AuthCompleted?.Invoke(this, eventArgs);

				if (eventArgs.IsAuthenticated)
				{
					// Use eventArgs.Account to do wonderful things
				}
				else
				{
					// The user cancelled
				}
			};
		}
	}
}
