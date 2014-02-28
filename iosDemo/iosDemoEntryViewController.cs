// This file has been autogenerated from a class added in the UI designer.

using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Timesheet.Client.Shared.Features.Timesheets;
using iosDemon;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace iosDemo
{
	public partial class iosDemoEntryViewController : UIViewController
	{
		private TimesheetsViewModel _vm;

		public iosDemoEntryViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Hub.Navigator = new IosNavigator (this);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			// Perform any additional setup after loading the view, typically from a nib.
			if (Hub.ApiClient.TokenProvider == null) {
				var authenticator = new Xamarin.Auth.WindowsAzureOAuth2Authenticator (
					                   "https://login.windows.net/<your tenant id>",
                                       "<client app id>",
                                       "<api / resource uri>",
                                       new Uri("<client redirect uri>"));

				authenticator.Completed += HandleCompleted;

								var view = authenticator.GetUI ();
				base.PresentViewController (view, true, null);
			}
		}


		void HandleCompleted (object sender, Xamarin.Auth.AuthenticatorCompletedEventArgs e)
		{
			base.DismissViewController (true, null);

			if (e.IsAuthenticated) {
				Hub.ApiClient.TokenProvider = e.Account;
			}

			_vm = new TimesheetsViewModel (Hub.ApiClient, Hub.Navigator);
			this.timesheetList.Bind (_vm.Timesheets, t => t.Name, _vm.TimesheetSelectedCommand);
			this.refreshTimesheets.Bind (_vm.Timesheets.RefreshCommand);
		}
	}
}
