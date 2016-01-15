Redux.NET-thunk
===============

This is an implementation of the Redux thunk middleware for use with the [Redux.NET](https://github.com/GuillaumeSalles/redux.NET/) library.

[NuGet](https://www.nuget.org/packages/redux.NET-thunk/)

Installation
------------

Install directly from the NuGet package, or from the Package Manager Console by running:

```
Install-Package redux.NET-thunk
```

Apply the middleware by passing it as a parameter to the Store constructor:

```csharp
var store = new Store<State>(initialState, reducer, Taiste.Redux.Middleware.ThunkMiddleware);
``` 

Usage
-----

Thunk middleware can be used to create Redux actions that dispatch other actions for conditional or asynchronous dispatching.

Example conditional dispatch:

```csharp
public static IAction ConditionalIncrement ()
{
    return new ThunkAction<int> ((dispatch, getState) => {
        int state = getState ();

        if (state < 10) {
            dispatch(new IncrementAction());
        }
    });
}

...

store.Dispatch(ConditionalIncrement());
```

Asynchronous dispatch:

```csharp
public static IAction AsyncActionCreator ()
{
    return new ThunkAction<ApplicationState> ((dispatch, getState) => {
        Task.Factory.StartNew (() => {
            var result = expensiveOperation ();
            dispatch (new ResultAction (result));
        });
    });
}

...

store.Dispatch(AsyncActionCreator());
```

### Notes

Unlike the [Javascript version](https://github.com/gaearon/redux-thunk), this version of the middleware considers Dispatch to be a void action, i.e. no Promise or similar is returned from Dispatch. The dispatched ThunkAction is returned to satisfy the interface.