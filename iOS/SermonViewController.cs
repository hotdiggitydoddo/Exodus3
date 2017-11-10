using Foundation;
using System;
using UIKit;

namespace Exodus3.iOS
{
    public partial class SermonViewController : UIViewController
    {
        public string SermonName { get; set; }
        public string SeriesName { get; set; }

        public SermonViewController (IntPtr handle) : base (handle)
        {
        }

       
    }
}