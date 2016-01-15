using System;
using Redux;

namespace Taiste.Redux.AndroidExample
{
    public static class Reducers
    {
        public static State ReduceState (State previous, IAction action)
        {
            var resultAction = action as SetResultsAction;
            if (resultAction != null) {
                return new State (resultAction.Results);
            }
            return previous;
        }
    }
}

