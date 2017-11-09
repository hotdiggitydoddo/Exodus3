using Foundation;
using System;
using UIKit;

namespace Exodus3.iOS
{
    public partial class SeriesViewController : UIViewController
    {
        public SeriesViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
           
            lblTitle.Font = UIFont.FromName("BrandonGrotesque-Bold", 26f);
            txtAboutSeries.Font = UIFont.FromName("BrandonGrotesque-Medium", 20f);
            txtAboutSeries.TextContainerInset = UIEdgeInsets.Zero;
            txtAboutSeries.TextContainer.LineFragmentPadding = 0;

            tblViewSermons.Source = new SermonsTableSource(new string[] { "First Sermon", "Second Sermon", "Third Sermon", "Fourth Sermon" });
            //tblViewSermons.RowHeight = 84;
            //tblViewSermons.RowHeight = 10;
            tblViewSermons.BackgroundColor = UIColor.FromRGB(47f / 255f, 43f / 255f, 33f / 255f);


           
        }
        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            // this line below seems to break the storyboard designer (makes it hang).
           // txtAboutSeries.ContentOffset = CoreGraphics.CGPoint.Empty;
        }
    }

    public class SermonsTableSource : UITableViewSource
    {

        string[] TableItems;
        string CellIdentifier = "TableCell";

        public SermonsTableSource(string[] items)
        {
            TableItems = items;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return 1;
        }
        public override nint NumberOfSections(UITableView tableView)
        {
            return TableItems.Length;
        }
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            E3TableViewCell cell = (E3TableViewCell)tableView.DequeueReusableCell(CellIdentifier);
            string item = TableItems[indexPath.Section];

            //---- if there are no cells to reuse, create a new one
            if (cell == null)
            { cell = new E3TableViewCell(UITableViewCellStyle.Default, CellIdentifier); }

            cell.TextLabel.Text = item;
            
            return cell;
        }


    }

    public class E3TableViewCell : UITableViewCell
    {
        public E3TableViewCell(UITableViewCellStyle style, string identifier) : base(style, identifier)
        {
            this.BackgroundColor = UIColor.FromRGB(67f / 255f, 68f / 255f, 59f / 255f);
            this.TextLabel.TextColor = UIColor.FromRGB(150f / 255f, 154f / 255f, 156f / 255f);
            this.TextLabel.Font = UIFont.FromName("BrandonGrotesque-Medium", 20f);
        }

      
    }
}