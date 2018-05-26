using System;
using Foundation;
using UIKit;

namespace Pom.iOS.ViewControllers
{
	public partial class AllCategoriesTableViewController : UITableViewController
	{
		public AllCategoriesTableViewController(IntPtr handle): base(handle)
		{
			
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.TableView.Source = new TableSource();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		protected class TableSource : UITableViewSource
		{
			protected string cellIdentifier = "basicCell";

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				// declare vars
				UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);
				// if there are no cells to reuse, create a new one
				if (cell == null)
					cell = new UITableViewCell(UITableViewCellStyle.Default, cellIdentifier);
				// set the item text
				cell.TextLabel.Text = "dsfds";

				return cell;
			}

			public override string TitleForHeader(UITableView tableView, nint section)
			{
				return "Categories";
			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
				return 3;
			}
		}
	}

}