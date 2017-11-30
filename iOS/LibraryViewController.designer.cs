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

namespace Exodus3.iOS
{
    [Register ("LibraryViewController")]
    partial class LibraryViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblMessages { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tblMessageOptions { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (lblMessages != null) {
                lblMessages.Dispose ();
                lblMessages = null;
            }

            if (tblMessageOptions != null) {
                tblMessageOptions.Dispose ();
                tblMessageOptions = null;
            }
        }
    }
}