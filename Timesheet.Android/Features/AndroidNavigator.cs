using System.Threading.Tasks;
using Android.Content;
using Timesheet.Client.Shared.Features.Timesheets;

namespace Timesheet.Android.Features
{
    public class AndroidNavigator : INavigator
    {
        private readonly Context _context;

        public AndroidNavigator(Context context)
        {
            _context = context;
        }

        public Task DisplayViewerFor<TResource>(TResource resource)
        {
            if (resource is TimesheetResource)
            {
                var vm = new TimesheetViewModel();
                vm.InitialiseWith(resource as TimesheetResource);
                Hub.PutEx(typeof(EditTimesheetActivity), vm);
                _context.StartActivity(typeof(EditTimesheetActivity));
            }

            var t = new TaskCompletionSource<object>();
            t.SetResult(null);
            return t.Task;
        }
    }
}