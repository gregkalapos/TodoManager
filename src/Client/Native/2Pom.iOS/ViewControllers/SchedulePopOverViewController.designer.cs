// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Pom.iOS.ViewControllers
{
    [Register ("SchedulePopOverViewController")]
    partial class SchedulePopOverViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIDatePicker ScheduleDatePicker { get; set; }

        [Action ("UIButton21630_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton21630_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (ScheduleDatePicker != null) {
                ScheduleDatePicker.Dispose ();
                ScheduleDatePicker = null;
            }
        }
    }
}