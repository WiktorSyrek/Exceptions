using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwd.Exceptions.Test
{
    [TestFixture]
    public class IgnoreTests
    {
        [Test]
        public void IgnoreException()
        {
            bool exceptionHappened = false;
            Assert.DoesNotThrow(() =>
                Ignore.AllExceptions(() => throw new Exception("Test"), e => exceptionHappened = true));

            Assert.That(exceptionHappened, Is.True);
        }

        [Test]
        public void IgnoreExceptionAndReturnNull()
        {
            Assert.DoesNotThrow(() =>
            {
                var result = Ignore.AllExceptions<string>(() => throw new Exception("Test"));
                Assert.That(result, Is.Null);
            });
        }

        [Test]
        public void NoExceptionThrownReturnString()
        {
            Assert.DoesNotThrow(() =>
            {
                var result = Ignore.AllExceptions(() => "Hello World");
                Assert.That(result, Is.EqualTo("Hello World"));
            } );
        }
    }
}
