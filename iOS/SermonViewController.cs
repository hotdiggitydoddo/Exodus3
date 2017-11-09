using Foundation;
using System;
using UIKit;

namespace Exodus3.iOS
{
    public partial class SermonViewController : UIViewController
    {
        public SermonViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var font = UIFont.FromName("BrandonGrotesque-Bold", 24f);
            lblTitle.Font = font;

        }
    }
}