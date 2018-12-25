
namespace Mwd.Exceptions
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Holds several methods to ignore exceptions.
    /// </summary>
    public static class Ignore
    {

        /// <summary>
        /// Calls <paramref name="action"/>. If an exception happened the user can use the optional
        /// <paramref name="exceptionHappened"/> parameter to e.g. log the exception.
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
        /// Calls <paramref name="action"/>. User can add a bunch of exception types that get ignored.
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
                SomeExceptions(() =>
                {
                    action?.Invoke();
                    return new Void();
                }, exceptionsToIgnore, ignoredExceptioHappened);


        /// <summary>
        /// See <see cref="SomeExceptions{T}(Func{T}, IEnumerable{Type}, Action{Exception})"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TException1"></typeparam>
        /// <param name="action"></param>
        /// <param name="ignoredExceptioHappened"></param>
        /// <returns></returns>
        public static T SomeExceptions<T, TException1>(
            Func<T> action,
            Action<Exception> ignoredExceptioHappened = null)
            =>
            SomeExceptions(
                action,
                new List<Type> { typeof(TException1) },
                ignoredExceptioHappened);


        /// <summary>
        /// See <see cref="SomeExceptions{T}(Func{T}, IEnumerable{Type}, Action{Exception})"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TException1"></typeparam>
        /// <typeparam name="TException2"></typeparam>
        /// <param name="action"></param>
        /// <param name="ignoredExceptioHappened"></param>
        /// <returns></returns>
        public static T SomeExceptions<T, TException1, TException2>(
            Func<T> action,
            Action<Exception> ignoredExceptioHappened = null)
            =>
            SomeExceptions(
                action,
                new List<Type> { typeof(TException1), typeof(TException2) },
                ignoredExceptioHappened);


        /// <summary>
        /// See <see cref="SomeExceptions{T}(Func{T}, IEnumerable{Type}, Action{Exception})"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TException1"></typeparam>
        /// <typeparam name="TException2"></typeparam>
        /// <typeparam name="TException3"></typeparam>
        /// <param name="action"></param>
        /// <param name="ignoredExceptioHappened"></param>
        /// <returns></returns>
        public static T SomeExceptions<T, TException1, TException2, TException3>(
            Func<T> action,
            Action<Exception> ignoredExceptioHappened = null)
            =>
            SomeExceptions(
                action,
                new List<Type> { typeof(TException1), typeof(TException2), typeof(TException3) },
                ignoredExceptioHappened);


        /// <summary>
        /// See <see cref="SomeExceptions(Action, IEnumerable{Type}, Action{Exception})"/>
        /// </summary>
        /// <typeparam name="TException1"></typeparam>
        /// <param name="action"></param>
        /// <param name="ignoredExceptioHappened"></param>
        public static void SomeExceptions<TException1>(
            Action action,
            Action<Exception> ignoredExceptioHappened = null)
            =>
            SomeExceptions(
                action,
                new List<Type> { typeof(TException1) },
                ignoredExceptioHappened);


        /// <summary>
        /// See <see cref="SomeExceptions(Action, IEnumerable{Type}, Action{Exception})"/>
        /// </summary>
        /// <typeparam name="TException1"></typeparam>
        /// <typeparam name="TException2"></typeparam>
        /// <param name="action"></param>
        /// <param name="ignoredExceptioHappened"></param>
        public static void SomeExceptions<TException1, TException2>(
            Action action,
            Action<Exception> ignoredExceptioHappened = null)
            =>
            SomeExceptions(
                action,
                new List<Type> { typeof(TException1), typeof(TException2) },
                ignoredExceptioHappened);


        /// <summary>
        /// See <see cref="SomeExceptions(Action, IEnumerable{Type}, Action{Exception})"/>
        /// </summary>
        /// <typeparam name="TException1"></typeparam>
        /// <typeparam name="TException2"></typeparam>
        /// <typeparam name="TException3"></typeparam>
        /// <param name="action"></param>
        /// <param name="ignoredExceptioHappened"></param>
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
