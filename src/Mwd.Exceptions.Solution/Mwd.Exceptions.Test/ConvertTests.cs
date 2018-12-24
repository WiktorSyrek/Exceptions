
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
                Exceptions.Convert.To<NullReferenceException, MyCustomException>(() => throw new ArgumentNullException("Test"));
            });
        }
    }
}
