using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Timesheet.Android
{
    public class ObservableCollectionAdapter<T> : BaseAdapter<T> where T : Timesheet.Client.Shared.ResourceAccess.Resource
    {
        private readonly Activity _context;
        private readonly ObservableCollection<T> _items;
        private readonly Func<T, string> _getter;
        private IList<T> _handledItems;

        public ObservableCollectionAdapter(Activity context, ObservableCollection<T> items, Func<T, string> getter)
        {
            _context = context;
            _items = items;
            _getter = getter;
            SubscribeToChangeEvents();
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

        private void UnSubscribeFromNotifyPropertyChangeEvents()
        {
            _items.ForEachEager(UnSubscribeItem);
            _handledItems = new List<T>();
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = _context.LayoutInflater.Inflate(Android.Resource.Layout.TextViewItem, null);
            }

            var textView = convertView as TextView;
            textView.Text = _getter(_items[position]);
            return textView;
        }

        public override int Count
        {
            get { return _items.Count; }
        }

        public override T this[int position]
        {
            get { return _items[position]; }
        }

        private void SubscribeToChangeEvents()
        {
            _items.CollectionChanged += (sender, args) =>
                {
                    UnSubscribeFromNotifyPropertyChangeEvents();
                    NotifyDataSetChanged();
                    SubscribeToNotifyPropertyChangeEvents();
                };
        }
    }
}