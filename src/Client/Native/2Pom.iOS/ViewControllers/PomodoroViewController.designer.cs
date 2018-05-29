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
    [Register ("PomodoroViewController")]
    partial class PomodoroViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton StartPomodoroButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TimeRemainingLabel { get; set; }

        [Action ("StartPomodoroButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void StartPomodoroButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (StartPomodoroButton != null) {
                StartPomodoroButton.Dispose ();
                StartPomodoroButton = null;
            }

            if (TimeRemainingLabel != null) {
                TimeRemainingLabel.Dispose ();
                TimeRemainingLabel = null;
            }
        }
    }
}