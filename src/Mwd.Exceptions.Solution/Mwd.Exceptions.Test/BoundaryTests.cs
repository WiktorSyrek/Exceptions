
namespace Mwd.Exceptions.Test
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class BoundaryTests
    {
        [Test]
        public void CatchDoesntPopulateException()
        {
            Assert.DoesNotThrow(
                () => Boundary.CatchAll(() => throw new Exception("Test")));
        }

        [Test]
        public void CatchCallsExceptionHappened()
        {
            bool exceptionHappenedCalled = false;
            Boundary.CatchAll(() => throw new Exception("Test"), e => exceptionHappenedCalled = true);
            Assert.That(exceptionHappenedCalled, Is.True);
        }

        [Test]
        public void CatchDoesntPopulateExceptionThoughActionIsNull()
        {
            Assert.DoesNotThrow(
                () => Boundary.CatchAll(null));
        }
    }
}
