using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Timesheet.Client.Shared.UIAbstractions;
using MonoTouch.UIKit;
using System.ComponentModel;
using System.Diagnostics;

namespace iosDemon
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> ForEachEager<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			foreach (var item in enumerable)
			{
				action(item);
			}
			return enumerable;
		}
	}

	internal class SimpleTableViewDataSource<T>: UITableViewSource where T: Timesheet.Client.Shared.ResourceAccess.Resource
	{
		ObservableCollection<T> _items;
		Func<T, string> _getter;
		Action onRefresh;
		private IList<T> _handledItems;

		Action<T> onSelected;

		public SimpleTableViewDataSource (ObservableCollection<T> items, Func<T, string> getter, Action onRefresh, Action<T> onSelected) 
		{
			this.onSelected = onSelected;
			this.onRefresh = onRefresh;
			_items = items;
			SubscribeToChangeEvents ();
			_getter = getter;
		}

		public override void RowSelected (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var item = _items [(int)indexPath.IndexAtPosition (1)];
			onSelected (item);
		}

		private void SubscribeToChangeEvents()
		{
			_items.CollectionChanged += (sender, args) =>
			{
				UnSubscribeFromNotifyPropertyChangeEvents();
				onRefresh();
				SubscribeToNotifyPropertyChangeEvents();
			};
		}

		private void SubscribeToNotifyPropertyChangeEvents()
		{
			_handledItems = new List<T>();
			_items.ForEachEager(SubscribeItem);
		}

		private void SubscribeItem(T item)
		{
			if (!(item is INotifyPropertyChanged))
				return;

			((INotifyPropertyChanged)item).PropertyChanged += OnPropertyChanged;
			_handledItems.Add(item);
		}

		private void UnSubscribeFromNotifyPropertyChangeEvents()
		{
			_items.ForEachEager(UnSubscribeItem);
			_handledItems = new List<T>();
		}

		private void UnSubscribeItem(T item)
		{
			if (!(item is INotifyPropertyChanged))
				return;

			((INotifyPropertyChanged)item).PropertyChanged -= OnPropertyChanged;
		}

		private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			Trace.WriteLine("Woohoo!!!");
		}

		public override int NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return _items.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var reuseCell = tableView.DequeueReusableCell ("SimpleCell");
			if (reuseCell == null)
			{
				reuseCell = new UITableViewCell (UITableViewCellStyle.Default, "SimpleCell");
			}
			var item = _items [(int)indexPath.IndexAtPosition(1)];
			reuseCell.TextLabel.Text = _getter (item);
			return reuseCell;
		}
	}

	public static class BindingExtensions
	{
		public static void Bind<T>(this UITableView tableView, ObservableCollection<T> items, Func<T, string> getter, DelegateCommand<T> selectedCommand) where T : Timesheet.Client.Shared.ResourceAccess.Resource
		{
			Action<T> onSelected = t =>
			{
				if (selectedCommand.CanExecute(t))
					selectedCommand.Execute(t);
			};
			var source = new SimpleTableViewDataSource<T>(items, getter, () => tableView.ReloadData(), onSelected);
			tableView.Source = source; 
		}

		public static void Bind(this UIButton button, ICommand command)
		{
			EventHandler onExecuteChangeHandler = (object s, EventArgs e) => {
				button.Enabled = command.CanExecute (null);
			};

			EventHandler onTapped = (s, e) => command.Execute (null);

			button.TouchUpInside += onTapped;
			command.CanExecuteChanged += onExecuteChangeHandler;
		}	
	}
}