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
    [Register ("ItemsViewController")]
    partial class ItemsViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISegmentedControl ItemSelectionOptionSegmentControl { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView TodoListTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ItemSelectionOptionSegmentControl != null) {
                ItemSelectionOptionSegmentControl.Dispose ();
                ItemSelectionOptionSegmentControl = null;
            }

            if (TodoListTableView != null) {
                TodoListTableView.Dispose ();
                TodoListTableView = null;
            }
        }
    }
}