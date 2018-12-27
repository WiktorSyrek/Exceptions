
namespace Mwd.Exceptions
{
    using System;
    
    Force build to fail to test status badge.

    /// <summary>
    /// Defines an exception boundary.
    /// </summary>
    public static class Boundary
    {
        /// <summary>
        /// Calls <paramref name="action"/>. If an exception happened the user can use the optional
        /// <paramref name="exceptionHappened"/> parameter to e.g. log the exception.
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
