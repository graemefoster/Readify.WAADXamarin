// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace iosDemo
{
	[Register ("iosDemoEntryViewController")]
	partial class iosDemoEntryViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton refreshTimesheets { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView timesheetList { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (timesheetList != null) {
				timesheetList.Dispose ();
				timesheetList = null;
			}

			if (refreshTimesheets != null) {
				refreshTimesheets.Dispose ();
				refreshTimesheets = null;
			}
		}
	}
}
