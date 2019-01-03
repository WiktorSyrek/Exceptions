
namespace Mwd.Exceptions.Test
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public partial class ConvertTests
    {
        [Test]
        public void ConvertExceptionToMyCustomException()
        {
            var caughtException = Assert.Throws<MyCustomException>(() =>
            {
                Exceptions.Convert.To<ArgumentNullException, MyCustomException>(() => throw new ArgumentNullException("Test"));
            });

            Assert.Multiple(() =>
            {
                Assert.That(caughtException.InnerException, Is.Not.Null);
                Assert.That(caughtException.InnerException.GetType(), Is.EqualTo(typeof(ArgumentNullException)));
            });
        }

        [Test]
        public void ConvertRethrowsUnspecifiedEception()
        {    
            Assert.Throws<NullReferenceException>(() =>
            {
                Exceptions.Convert.To<ArgumentNullException, MyCustomException>(() => throw new NullReferenceException("Test"));
            });
        }

        [Test]
        public void ToIgnoreInvalidActionObjects()
        {
            Assert.DoesNotThrow(() =>
            {
                Exceptions.Convert.To<ArgumentNullException, MyCustomException>(null);
            });
        }

        [Test]
        public void ToIgnoreInvalidFuncObjectsAndReturnDefault()
        {
            string returnValue = "test";
            Assert.DoesNotThrow(() =>
            {
                returnValue = Exceptions.Convert.To<string,ArgumentNullException, MyCustomException>(null);
            });

            // Func object was null, check if the return value is set to default(T)
            Assert.That(returnValue, Is.Null);
        }
    }
}
