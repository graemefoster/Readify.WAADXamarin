using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Timesheet.Client.Shared.UIAbstractions;

namespace Timesheet.Client.Shared.ResourceAccess
{
    public class ResourceBackedObservableCollection<T> : ObservableCollection<T> where T : Resource
    {
        private readonly IApi<T> _api;
        private readonly IApiClient _client;

        public ResourceBackedObservableCollection(IApiClient client, IApi<T> api)
        {
            _client = client;
            _api = api;
            RefreshCommand = new DelegateCommand(() => true, Refresh);
        }

        public DelegateCommand RefreshCommand { get; private set; }

        private async Task Refresh()
        {
            var timesheets = await _client.Fetch(_api);
            foreach (var timesheetResource in timesheets)
            {
                if (!Contains(timesheetResource))
                {
                    Add(timesheetResource);
                    Debug.WriteLine("Added resource");
                }
            }
        }
    }
}