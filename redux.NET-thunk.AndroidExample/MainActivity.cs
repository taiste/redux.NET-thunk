using Android.App;
using Android.Widget;
using Android.OS;
using Redux;
using System.Collections.Immutable;
using System.Reactive.Linq;
using Android.Text;
using System;
using System.Collections.Generic;

namespace Taiste.Redux.AndroidExample
{
    [Activity (Label = "Redux.NET-thunk Android Example", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        Store<State> store;
        ListView list;

        public MainActivity ()
        {
            store = new Store<State> (new State (ImmutableList<string>.Empty), Reducers.ReduceState, Middleware.ThunkMiddleware);
        }

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            SetContentView (Resource.Layout.main);

            list = FindViewById<ListView> (Android.Resource.Id.List);

            ArrayAdapter<string> adapter = new ArrayAdapter<string> (this, Android.Resource.Layout.SimpleListItem1, new List<string> ());
            list.Adapter = adapter;

            EditText edit = FindViewById<EditText> (Android.Resource.Id.Edit);

            Observable.FromEventPattern<TextChangedEventArgs> (edit, "TextChanged")
                .Throttle (TimeSpan.FromMilliseconds (500))
                .Subscribe (e => store.Dispatch (ActionCreators.SearchUsers (edit.Text.Trim ())));

            store.Select (s => s.Results)
                .DistinctUntilChanged ()
                .Subscribe (UpdateAdapter);
        }

        private void UpdateAdapter (ImmutableList<string> items)
        {
            RunOnUiThread (() => {
                var adapter = list.Adapter as ArrayAdapter<string>;
                adapter.Clear ();
                adapter.AddAll (items);
                adapter.NotifyDataSetChanged ();
            });
        }
    }
}


