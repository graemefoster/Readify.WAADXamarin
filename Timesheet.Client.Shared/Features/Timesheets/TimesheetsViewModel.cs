using System.Diagnostics;
using System.Threading.Tasks;
using Timesheet.Client.Shared.ResourceAccess;
using Timesheet.Client.Shared.UIAbstractions;

namespace Timesheet.Client.Shared.Features.Timesheets
{
    public class TimesheetsViewModel
    {
        private readonly IApiClient _apiClient;
        private readonly INavigator _navigator;

        public TimesheetsViewModel(IApiClient apiClient, INavigator navigator)
        {
            _apiClient = apiClient;
            _navigator = navigator;
            var timesheets = new ResourceBackedObservableCollection<TimesheetResource>
                (_apiClient,
                 new GetTimesheetsApi());

            Timesheets = timesheets;
            TimesheetSelectedCommand = new DelegateCommand<TimesheetResource>(t => true, OnEditTimesheet);
        }

        public ResourceBackedObservableCollection<TimesheetResource> Timesheets { get; private set; }

        public DelegateCommand<TimesheetResource> TimesheetSelectedCommand { get; private set; }

        private Task OnEditTimesheet(TimesheetResource arg)
        {
            Debug.WriteLine("Yeah editing timesheet");
            return _navigator.DisplayViewerFor(arg);
        }
    }
}