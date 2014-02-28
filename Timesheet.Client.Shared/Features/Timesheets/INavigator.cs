using System.Threading.Tasks;

namespace Timesheet.Client.Shared.Features.Timesheets
{
    public interface INavigator
    {
        Task DisplayViewerFor<TResource>(TResource resource);
    }
}