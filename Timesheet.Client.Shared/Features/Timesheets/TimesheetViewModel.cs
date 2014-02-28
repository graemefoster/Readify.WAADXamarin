namespace Timesheet.Client.Shared.Features.Timesheets
{
    public class TimesheetViewModel
    {
        public TimesheetViewModel()
        {
        }

        public void InitialiseWith(TimesheetResource timesheet)
        {
            this.Timesheet = timesheet;
        }

        public TimesheetResource Timesheet { get; private set; }
    }
}