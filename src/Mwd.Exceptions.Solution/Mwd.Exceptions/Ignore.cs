
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
        /// Calls <see cref="action"/>. User can add a bunch of exception types that get ignored
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="exceptionsToIgnore"></param>
        /// <param name="ignoredExceptioHappened"></param>
        /// <returns></returns>
        public static T SomeExceptions<T>(
            Func<T> action, 
            IEnumerable<Type> exceptionsToIgnore, 
            Action<Exception> ignoredExceptioHappened = null)
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

        /// <summary>
        /// Same as <see cref="SomeExceptions{T}(Func{T}, IEnumerable{Type}, Action{Exception})"/>, except that it uses an <see cref="Action"/>.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="exceptionsToIgnore"></param>
        /// <param name="ignoredExceptioHappened"></param>
        public static void SomeExceptions(
            Action action,
            IEnumerable<Type> exceptionsToIgnore,
            Action<Exception> ignoredExceptioHappened = null)
            =>
                SomeExceptions<Void>(() =>
                {
                    action?.Invoke();
                    return new Void();
                }, exceptionsToIgnore, ignoredExceptioHappened);


        public static T SomeExceptions<T, TException1>(
            Func<T> action, 
            Action<Exception> ignoredExceptioHappened = null)
            =>
            SomeExceptions<T>(
                action, 
                new List<Type> { typeof(TException1) }, 
                ignoredExceptioHappened);

        public static T SomeExceptions<T, TException1, TException2>(
            Func<T> action, 
            Action<Exception> ignoredExceptioHappened = null)
            =>
            SomeExceptions<T>(
                action, 
                new List<Type> { typeof(TException1), typeof(TException2) }, 
                ignoredExceptioHappened);

        public static T SomeExceptions<T, TException1, TException2, TException3>(
            Func<T> action, 
            Action<Exception> ignoredExceptioHappened = null)
            =>
            SomeExceptions<T>(
                action, 
                new List<Type> { typeof(TException1), typeof(TException2), typeof(TException3) }, 
                ignoredExceptioHappened);

        public static void  SomeExceptions<TException1>(
            Action action, 
            Action<Exception> ignoredExceptioHappened = null)
            =>
            SomeExceptions(
                action, 
                new List<Type> { typeof(TException1) }, 
                ignoredExceptioHappened);

        public static void SomeExceptions<TException1, TException2>(
            Action action, 
            Action<Exception> ignoredExceptioHappened = null)
            =>
            SomeExceptions(
                action, 
                new List<Type> { typeof(TException1), typeof(TException2) }, 
                ignoredExceptioHappened);

        public static void SomeExceptions<TException1, TException2, TException3>(
            Action action, 
            Action<Exception> ignoredExceptioHappened = null)
            =>
            SomeExceptions(
                action, 
                new List<Type> { typeof(TException1), typeof(TException2), typeof(TException3) }, 
                ignoredExceptioHappened);
    }
}
