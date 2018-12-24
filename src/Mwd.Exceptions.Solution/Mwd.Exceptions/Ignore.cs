
namespace Mwd.Exceptions
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public static class Ignore
    {

        /// <summary>
        /// Calls <see cref="action"/>. If an exception happened the user can use the optional
        /// <see cref="exceptionHappened"/> parameter to e.g. log the exception.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="exceptionHappened"></param>
        public static T AllExceptions<T>(
            Func<T> action, 
            Action<Exception> exceptionHappened = null)
            where T : class
        {
            try
            {
                return action.Invoke();
            }
            catch (Exception ex)
            {
                exceptionHappened?.Invoke(ex);
                return default(T);
            }
        }


        /// <summary>
        /// <see cref="AllExceptions{T}(Func{T}, Action{Exception})"/>.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="exceptionHappened"></param>
        public static void AllExceptions(
            Action action,
            Action<Exception> exceptionHappened = null)
            =>
            AllExceptions<Void>(
                () =>
                {
                    action?.Invoke();
                    return new Void();
                },
                exceptionHappened);


        /// <summary>
        /// Calls <see cref="action"/>. User can 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="exceptionsToIgnore"></param>
        /// <param name="ignoredExceptioHappened"></param>
        /// <returns></returns>
        public static T SomeExceptions<T, TExceptionsToIgnore>(
            Func<T> action, 
            IEnumerable<TExceptionsToIgnore> exceptionsToIgnore, 
            Action<Exception> ignoredExceptioHappened = null)
            where TExceptionsToIgnore : Exception
        {
            try
            {
                return action.Invoke();
            }
            catch (Exception exception) when (exceptionsToIgnore.Any(exType => exType.Equals(exception.GetType())))
            {
                ignoredExceptioHappened?.Invoke(exception);
                return default(T);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
