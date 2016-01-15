using System;
using System.Collections.Immutable;

namespace Taiste.Redux.AndroidExample
{
    public class State
    {
        public ImmutableList<string> Results { get; }
        public State (ImmutableList<string> results)
        {
            Results = results;
        }
    }
}

