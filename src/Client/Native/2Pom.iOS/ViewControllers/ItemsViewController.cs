using System;
using Foundation;
using Pom.iOS.Services;
using ToDoManager;
using ToDoManager.Model;
using UIKit;

namespace Pom.iOS.ViewControllers
{
	public partial class ItemsViewController : UIViewController
	{
		private ItemsViewModel _vm;
		private TodoListSource _todoListSource;

		public ItemsViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			_vm = new ItemsViewModel(new CloudDataStore(), new IosNavigation(this));
			_todoListSource = new TodoListSource(_vm, this);
			TodoListTableView.Source = _todoListSource;

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

			ItemSelectionOptionSegmentControl.ValueChanged += (s, o) =>
			{
				switch (ItemSelectionOptionSegmentControl.SelectedSegment)
				{
					case 0:
						_vm.LoadItemsCommand.Execute(null);
						break;
					case 1:
						_vm.LoadAllItemsCommand.Execute(null);
						break;
					case 2:
						_vm.LoadDoneItemsCommand.Execute(null);
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
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			//AddNewTodoTouchedSegue: New Todo touched
			if (segue.Identifier == "ToDoItemSelectedSegue")
			{
				var itemDetailViewController = segue.DestinationViewController as ItemDetailViewController;
				itemDetailViewController.SelectedToDoItem = _todoListSource.SelectedTodoItem;
			}
		}

		class TodoListSource : UITableViewSource
		{
			protected string cellIdentifier = "basicCell";
			ItemsViewModel _vm;
			ItemsViewController _parent;

			public ToDoItemModel SelectedTodoItem
			{
				get;
				private set;
			}

			public TodoListSource(ItemsViewModel vm, ItemsViewController parent)
			{
				_vm = vm;
				_parent = parent;
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

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				SelectedTodoItem = _vm.ToDoItems[indexPath.Row];
				_parent.PerformSegue("ToDoItemSelectedSegue", this);
			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
				return _vm.ToDoItems.Count;
			}
		}
	}
}

