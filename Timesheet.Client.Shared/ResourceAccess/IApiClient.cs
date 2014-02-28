using System.Collections.Generic;
using System.Threading.Tasks;

namespace Timesheet.Client.Shared.ResourceAccess
{
    public interface IApiClient
    {
        Task<IEnumerable<TResource>> Fetch<TResource>(IApi<TResource> resource);
    }
}