namespace Timesheet.Client.Shared.ResourceAccess
{
    public interface IApi<TResource>
    {
        string RelativeUri { get; }
    }
}