
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
            Assert.Throws<MyCustomException>(() =>
            {
                Exceptions.Convert.To<ArgumentNullException, MyCustomException>(() => throw new ArgumentNullException("Test"));
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
