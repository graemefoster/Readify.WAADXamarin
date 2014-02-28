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
	[Register ("TimesheetView2Controller")]
	partial class TimesheetView2Controller
	{
		[Outlet]
		MonoTouch.UIKit.UILabel timesheetName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (timesheetName != null) {
				timesheetName.Dispose ();
				timesheetName = null;
			}
		}
	}
}
