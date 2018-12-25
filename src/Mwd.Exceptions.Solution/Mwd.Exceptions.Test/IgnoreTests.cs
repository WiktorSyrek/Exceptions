
namespace Mwd.Exceptions.Test
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

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
        public void IgnoreExceptionAndReturnNativeType()
        {
            Assert.DoesNotThrow(() =>
            {
                var result = Ignore.AllExceptions<bool>(() => throw new Exception("Test"));
                Assert.That(result, Is.False);
            });
        }

        [Test]
        public void NoExceptionThrownReturnString()
        {
            Assert.DoesNotThrow(() =>
            {
                var result = Ignore.AllExceptions(() => "Hello World");
                Assert.That(result, Is.EqualTo("Hello World"));
            });
        }

        [Test]
        public void IgnoreSomeExceptionsCaughtExceptionIsIgnored()
        {
            Assert.DoesNotThrow(
                () =>
                {
                    var result = Ignore.SomeExceptions<bool, ArgumentException, NullReferenceException, FormatException>(() =>
                    {
                        throw new ArgumentException("Test");
                    });

                    Assert.That(result, Is.False);
                }
                );
        }

        [Test]
        public void IgnoreRethrowExceptionThatDoesntMatch()
        {
            Assert.Throws<ArgumentException>(
                () =>
                {
                    Exception occurredException = null;

                    // Act
                    var result = Ignore.SomeExceptions<bool, ArgumentNullException, NullReferenceException, FormatException>(() =>
                    {
                        throw new ArgumentException("Test");
                    },
                    e => occurredException = e);

                    // Assert
                    Assert.Multiple(() =>
                    {
                        Assert.That(result, Is.False);
                        Assert.That(occurredException, Is.Not.Null);
                        Assert.That(occurredException.GetType(), Is.EqualTo(typeof(ArgumentException)));
                    });
                }
                );
        }
    }
}
