using Timesheet.Client.Shared.Features.Timesheets;

namespace Timesheet.Client.Shared.ResourceAccess
{
    internal class GetTimesheetsApi : IApi<TimesheetResource>
    {
        public string RelativeUri
        {
            get { return "timesheets"; }
        }
    }
}