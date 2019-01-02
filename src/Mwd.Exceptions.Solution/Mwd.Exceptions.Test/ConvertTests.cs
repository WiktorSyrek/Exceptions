
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
    }
}
