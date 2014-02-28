using System;
using System.Threading.Tasks;
using Timesheet.Client.Shared.ResourceAccess;
using System.Net.Http;
using Timesheet.Client.Shared.Features.Timesheets;
using System.Collections.Generic;

namespace iosDemo
{

	public class IosNavigator: INavigator
	{
		private iosDemo.iosDemoEntryViewController _controller;

		public IosNavigator(iosDemo.iosDemoEntryViewController entryController)
		{
			_controller = entryController;
		}

		public Task DisplayViewerFor<TResource>(TResource resource)
		{
			if (resource is TimesheetResource)
			{
				Hub.PutEx ("timesheet", resource);
				_controller.PerformSegue ("showTimesheet", _controller);
				var tcs = new TaskCompletionSource<object>();
				tcs.SetResult (null);
				return tcs.Task;
			}
			throw new InvalidOperationException("No view for this resource type");
		}
	}
}
