using System;
using System.Threading.Tasks;
using Timesheet.Client.Shared.ResourceAccess;
using System.Net.Http;
using Timesheet.Client.Shared.Features.Timesheets;
using System.Collections.Generic;

namespace iosDemo
{
	public static class Hub
	{
		private static readonly IDictionary<object, object> _dataStash = new Dictionary<object, object>();

		static Hub()
		{
			ApiClient = new ApiClient(new HttpClient(), new TimesheetApiConfig());
		}

		public static ApiClient ApiClient { get; private set; }
		public static IosNavigator Navigator { get; set; }

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

