using System;
using Foundation;
using ToDoManager;
using UIKit;

namespace Pom.iOS.ViewControllers
{
	public partial class ItemsViewController : UIViewController
	{
		private ItemsViewModel _vm;

		public ItemsViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			_vm = new ItemsViewModel(new CloudDataStore());
			TodoListTableView.Source = new TodoListSource(_vm);

			_vm.ToDoItems.CollectionChanged += (s, o) =>
			{
				TodoListTableView.ReloadData();
			};

			_vm.PropertyChanged += (s, o) =>
			{
				switch (o.PropertyName)
				{
					case nameof(_vm.ToDoItems):
						TodoListTableView.ReloadData();
						break;
					default:
						break;
				}
			};
			_vm.LoadItemsCommand.Execute(null);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		class TodoListSource : UITableViewSource
		{
			protected string cellIdentifier = "basicCell";
			ItemsViewModel _vm;

			public TodoListSource(ItemsViewModel vm)
			{
				_vm = vm;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				// declare vars
				UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);
				// if there are no cells to reuse, create a new one
				if (cell == null)
					cell = new UITableViewCell(UITableViewCellStyle.Default, cellIdentifier);
				// set the item text
				cell.TextLabel.Text = _vm.ToDoItems[indexPath.Row].Title;

				return cell;
			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
				return _vm.ToDoItems.Count;
			}
		}
	}
}

