using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Android.App;
using Android.Views;
using Android.Widget;
using Timesheet.Client.Shared.UIAbstractions;

namespace Timesheet.Android
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

    public static class BindingExtensions
    {
        public static void Bind<T>(this ListView listView, Activity context, ObservableCollection<T> items, Func<T, string> getter) where T : Timesheet.Client.Shared.ResourceAccess.Resource
        {
            listView.Adapter = new ObservableCollectionAdapter<T>(context, items, getter);
        }

        public static void BindItemSelected<T>(this ListView listView, ObservableCollection<T> items, DelegateCommand<T> command)
        {
            EventHandler<AdapterView.ItemClickEventArgs> selectedEventHandler =
                (sender, args) =>
                {
                    var item = items[args.Position];
                    if (command.CanExecute(item))
                        command.Execute(item);
                };

            EventHandler<View.ViewAttachedToWindowEventArgs> onAttached = (sender, args) =>
            {
                listView.ItemClick+= selectedEventHandler;
            };

            EventHandler<View.ViewDetachedFromWindowEventArgs> onDetached = (sender, args) =>
            {
                listView.ItemClick -= selectedEventHandler;
            };


            listView.ViewAttachedToWindow += onAttached;
            listView.ViewDetachedFromWindow += onDetached;
        }

        public static void Bind(this Button button, ICommand command)
        {
            EventHandler onExecuteChangeHandler = (object s, EventArgs e) =>
                {
                    button.Enabled = command.CanExecute(null);
                };

            EventHandler onTapped = (s, e) => command.Execute(null);

            EventHandler<View.ViewAttachedToWindowEventArgs> onAttached = (sender, args) =>
                {
                    command.CanExecuteChanged += onExecuteChangeHandler;
                    button.Click += onTapped;
                };

            EventHandler<View.ViewDetachedFromWindowEventArgs> onDetached = (sender, args) =>
                {
                    command.CanExecuteChanged -= onExecuteChangeHandler;
                    button.Click -= onTapped;
                };

            button.ViewAttachedToWindow += onAttached;
            button.ViewDetachedFromWindow += onDetached;
        }
    }
}