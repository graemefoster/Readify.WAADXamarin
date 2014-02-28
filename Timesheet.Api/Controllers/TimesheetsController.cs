using System.Collections.Generic;
using System.Web.Http;

namespace Timesheet.Api.Controllers
{
    public class TimesheetsController : ApiController
    {
        [Authorize]
        public IEnumerable<TimesheetResource> Get()
        {
            return new TimesheetResource[]
                {
                    new TimesheetResource() { Id=1, Name = "One"},
                    new TimesheetResource() { Id=2, Name = "Two"},
                    new TimesheetResource() { Id=3, Name = "Three"},
                };
        }
    }

    public class TimesheetResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}