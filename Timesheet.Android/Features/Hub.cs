using System.Collections.Generic;
using System.Net.Http;
using Timesheet.Client.Shared.ResourceAccess;

namespace Timesheet.Android.Features
{
    public static class Hub
    {
        private static readonly IDictionary<object, object> _dataStash = new Dictionary<object, object>();

        static Hub()
        {
            ApiClient = new ApiClient(new HttpClient(), new TimesheetApiConfig());
        }

        public static AndroidNavigator Navigator { get; set; }
        public static ApiClient ApiClient { get; private set; }

        public static void PutEx(object forConsumer, object data)
        {
            _dataStash.Add(forConsumer, data);
        }

        public static T GetEx<T>(object consumer) where T : class
        {
            var data = _dataStash[consumer] as T;
            _dataStash.Remove(consumer);
            return data;
        }
    }
}