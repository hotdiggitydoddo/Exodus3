using Foundation;
using System;
using UIKit;
using Exodus3.Core;

namespace Exodus3.iOS
{
    public partial class SermonViewController : UIViewController
    {
        public string SermonName { get; set; }
        public string SeriesName { get; set; }
        public string SermonSummary { get; set; }

        public SermonViewController(IntPtr handle) : base(handle)
        {
        }

       
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (string.IsNullOrWhiteSpace(SeriesName) && string.IsNullOrWhiteSpace(SermonName))
            {
                var sermon = App.Database.GetLastSermon().Result;
                SermonName = sermon.Name;
                SeriesName = sermon.SeriesItem.Name;
                txtAboutSermon.Text = sermon.Summary;
            }
           

            btnPlaySermon.BackgroundColor = UIColor.FromRGB(251f / 255f, 206f / 255f, 69f / 255f);
            btnPlaySermon.Font = UIFont.FromName("BrandonGrotesque-Bold", 30f);
            lblSermonName.Font = UIFont.FromName("BrandonGrotesque-Bold", 30f);
            lblSermonName.Text = $"{SeriesName} | {SermonName}";
            lblSermonName.TextColor = UIColor.FromRGB(251f / 255f, 206f / 255f, 69f / 255f);
            txtAboutSermon.Font = UIFont.FromName("BrandonGrotesque-Medium", 20f);

            if (NavigationController != null)
            {
                NavigationController.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
                NavigationController.NavigationBar.TintColor = UIColor.FromRGB(150f / 255f, 154f / 255f, 156f / 255f);
   
            }

            btnPlaySermon.TouchUpInside += (sender, ea) => {
                var board = UIStoryboard.FromName("Main", null);
                var playerCtrl = board.InstantiateViewController("MediaPlayerViewController") as MediaPlayerViewController;
                //sermonCtrl.Title = TableItems[indexPath.Section];
                //sermonCtrl.SermonName = TableItems[indexPath.Section];
                //sermonCtrl.SeriesName = owner.SeriesName;

                NavigationController.PushViewController(playerCtrl, true);
 
            };
        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (NavigationController.NavigationBar.TopItem.Title != null)
                NavigationController.NavigationBar.TopItem.Title = null;
     //       if (NavigationController.NavigationBar.BackItem != null)
     //           NavigationController.NavigationBar.BackItem.Title = SeriesName;

        }


    }
}