using System;
using Redux;

namespace Taiste.Redux.Tests
{
    public static class Reducers
    {
        public static int Reduce(int previousState, IAction action){
            if (action is TestAction) {
                return ((TestAction)action).Value;
            }
            return previousState;
        }
    }
}

