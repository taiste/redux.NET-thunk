using NUnit.Framework;
using System;
using Redux;
using Taiste.Redux;
namespace Taiste.Redux.Tests
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void ThunkActionShouldBeFired ()
        {
            Store<int> store = new Store<int>(0, Reducers.Reduce , Middleware.ThunkMiddleware);
            bool called = false;
            ThunkAction<int> action = new ThunkAction<int> ((dispatcher, getState) => {
                called = true;
            });
            store.Dispatch (action);
            Assert.True (called);
        }

        [Test]
        public void ThunkActionShouldBeAbleToDispatchAction ()
        {
            Store<int> store = new Store<int>(0, Reducers.Reduce , Middleware.ThunkMiddleware);

            ThunkAction<int> action = new ThunkAction<int> ((dispatcher, getState) => 
                dispatcher(new TestAction(){Value = 1})
            );
            store.Dispatch (action);
            Assert.AreEqual(store.GetState(), 1);
        }

        [Test]
        public void ThunkActionShouldBeAbleToGetCorrectState ()
        {
            Store<int> store = new Store<int>(0, Reducers.Reduce , Middleware.ThunkMiddleware);
            store.Dispatch (new TestAction(){Value = 1});
            ThunkAction<int> action = new ThunkAction<int> ((dispatcher, getState) => 
                dispatcher(new TestAction(){Value = getState() + 5})
            );
            store.Dispatch (action);
            Assert.AreEqual(store.GetState(), 6);
        }
    }
}

