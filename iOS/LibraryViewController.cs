using Foundation;
using System;
using UIKit;

namespace Exodus3.iOS
{
    public partial class LibraryViewController : UIViewController
    {
        public LibraryViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            lblMessages.Font = UIFont.FromName("BrandonGrotesque-Bold", 26f);
            tblMessageOptions.Source = new MessageOptionsTableSource(this);
            tblMessageOptions.BackgroundColor = UIColor.FromRGB(47f / 255f, 43f / 255f, 33f / 255f);
            if (NavigationController != null)
            {
                NavigationController.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
                NavigationController.NavigationBar.TintColor = UIColor.FromRGB(150f / 255f, 154f / 255f, 156f / 255f);

            }

            //fix to eliminate the topmost section header and the gap it creates
            var frame = CoreGraphics.CGRect.Empty;
            frame.Height = 1;
            tblMessageOptions.TableHeaderView = new UIView(frame);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            Title = null;
            NavigationController.NavigationBarHidden = true;
        }
    }

    public class MessageOptionsTableSource : UITableViewSource
    {
        string[] TableItems = { "All Series", "Latest Sermons", "Judges - Part One" };
        string CellIdentifier = "TableCell";

        LibraryViewController owner;

        public MessageOptionsTableSource(LibraryViewController owner)
        {
            this.owner = owner;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return 1;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return TableItems.Length;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var board = UIStoryboard.FromName("Main", null);

            var sermonCtrl = board.InstantiateViewController("SermonViewController") as SermonViewController;
            sermonCtrl.Title = TableItems[indexPath.Section];
          // // sermonCtrl.SeriesName = owner.SeriesName;

            owner.NavigationController.PushViewController(sermonCtrl, true);

            tableView.DeselectRow(indexPath, true);
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            E3TableViewCell cell = (E3TableViewCell)tableView.DequeueReusableCell(CellIdentifier);
            string item = TableItems[indexPath.Section];

            //---- if there are no cells to reuse, create a new one
            if (cell == null)
            {
                cell = new E3TableViewCell(UITableViewCellStyle.Default, CellIdentifier); 
            }

            cell.TextLabel.Text = item;
            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

            return cell;
        }
    }

}