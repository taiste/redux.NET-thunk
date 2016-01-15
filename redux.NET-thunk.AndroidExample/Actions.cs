using System;
using Redux;
using System.Collections.Immutable;

namespace Taiste.Redux.AndroidExample
{
    public class SetResultsAction : IAction {
        public ImmutableList<string> Results;
    }
}

