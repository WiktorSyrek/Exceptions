
namespace Mwd.Exceptions
{
    using System;

    public static class Boundary
    {
        /// <summary>
        /// Calls <see cref="action"/>. If an exception happened the user can use the optional
        /// <see cref="exceptionHappened"/> parameter to e.g. log the exception.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="exceptionHappened"></param>
        public static void CatchAll(
            Action action, 
            Action<Exception> exceptionHappened = null)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception exception)
            {
                exceptionHappened?.Invoke(exception);
            }
        }
    }
}
