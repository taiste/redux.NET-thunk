using System;
using Redux;
using System.Net;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Immutable;
using System.Threading;

namespace Taiste.Redux.AndroidExample
{
    public static class ActionCreators
    {
        public static IAction SearchUsers (string query)
        {
            return new ThunkAction<State> ((dispatcher, getState) => {

                var request = WebRequest.Create (new Uri (String.Format ("https://api.github.com/search/users?q={0}", query))) as HttpWebRequest;
                request.UserAgent = "redux.NET thunk example";
                var obs = Observable.FromAsync (() => Task.Factory.FromAsync<WebResponse> (request.BeginGetResponse, request.EndGetResponse, request));

                obs.Subscribe (
                    (next) => {
                        var responseData = new StreamReader (next.GetResponseStream ()).ReadToEnd ();
                        var results = ImmutableList<string>.Empty;

                        JObject res = JObject.Parse (responseData);

                        var items = (JArray)res ["items"];

                        foreach (var item in items) {
                            results = results.Add (item ["login"].Value<string> ());
                        }

                        dispatcher (new SetResultsAction () { Results = results });
                    },
                    (error) => System.Diagnostics.Debug.WriteLine (error.Message),
                    CancellationToken.None);
            });
        }
    }
}

