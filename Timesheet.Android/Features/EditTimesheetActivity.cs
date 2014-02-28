using System;
using Android.App;
using Android.OS;
using Android.Widget;
using Timesheet.Client.Shared.Features.Timesheets;

namespace Timesheet.Android.Features
{
    [Activity(Label = "Timesheet.Android", MainLauncher = false)]
    public class EditTimesheetActivity : Activity
    {
        private TimesheetViewModel _viewModel;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.EditTimesheet);

            _viewModel = Hub.GetEx<TimesheetViewModel>(typeof (EditTimesheetActivity));
            FindViewById<EditText>(Resource.Id.timsheetName).Text = _viewModel.Timesheet.Name;
        }
    }
}