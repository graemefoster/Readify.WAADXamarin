using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Timesheet.Client.Shared.Features.Timesheets;
using Xamarin.Auth;

namespace Timesheet.Android.Features
{
    [Activity(Label = "Timesheet.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class TimesheetIndexActivity : ListActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            AndroidEnvironment.UnhandledExceptionRaiser += OnUnhandledEception;
            TaskScheduler.UnobservedTaskException += OnUnhandledTaskException;

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Hub.Navigator = new AndroidNavigator(this);

            var vm = new TimesheetsViewModel(Hub.ApiClient, Hub.Navigator);
            ListView.Bind(this, vm.Timesheets, t => t.Name);
            ListView.BindItemSelected(vm.Timesheets, vm.TimesheetSelectedCommand);
            FindViewById<Button>(Resource.Id.refreshItems).Bind(vm.Timesheets.RefreshCommand);

            //Initiate the OAuth2 authentication process:
            var authenticator = new Xamarin.Auth.WindowsAzureOAuth2Authenticator(
                "https://login.windows.net/<your tenant id>",
                "<client app id>",
                "<api / resource uri>",
                new Uri("<client redirect uri>"));

            authenticator.Completed += AuthenticatorOnCompleted;

            var activity = authenticator.GetUI(this);
            StartActivity(activity);

        }

        private void AuthenticatorOnCompleted(object sender, AuthenticatorCompletedEventArgs authenticatorCompletedEventArgs)
        {
            if (authenticatorCompletedEventArgs.IsAuthenticated)
                Hub.ApiClient.TokenProvider = authenticatorCompletedEventArgs.Account;

        }

        private void OnUnhandledTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Trace.WriteLine("GOT IT OnUnhandledTaskException");
        }

        private void OnUnhandledEception(object sender, RaiseThrowableEventArgs e)
        {
            Trace.WriteLine("GOT IT OnUnhandledException");
        }
    }
}