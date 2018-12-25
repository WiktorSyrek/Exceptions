
namespace Mwd.Exceptions
{
    using System;

    /// <summary>
    /// Defines convertion methods to e.g. convert ExceptionA to ExceptionB.
    /// </summary>
    public static class Convert
    {
        /// <summary>
        /// 
        /// Converts the given TExceptionToCatch type to TExceptionToThrow type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TExceptionToCatch"></typeparam>
        /// <typeparam name="TExceptionToThrow"></typeparam>
        /// <param name="action"></param>
        /// <param name="errorTextToIncludeInThrownException"></param>
        /// <returns></returns>
        public static TResult To<TResult, TExceptionToCatch, TExceptionToThrow>(
            Func<TResult> action,
            string errorTextToIncludeInThrownException = null)
            where TExceptionToCatch : Exception
            where TExceptionToThrow : Exception
            where TResult : class
        {
            try
            {
                return action?.Invoke() ?? default(TResult);
            }
            catch (TExceptionToCatch exception)
            {
                // Based on generice restriction "TDestinationException : Exception" as-operator should always return 
                // a valid exception object.
                var newEx = Activator.CreateInstance(typeof(TExceptionToThrow), errorTextToIncludeInThrownException, exception) as Exception;
                throw newEx;
            }
        }

        /// <summary>
        /// Void implementation
        /// </summary>
        /// <typeparam name="TExceptionToCatch"></typeparam>
        /// <typeparam name="TExceptionToThrow"></typeparam>
        /// <param name="action"></param>
        /// <param name="errorTextToIncludeInThrownException"></param>
        /// <returns></returns>
        public static void To<TExceptionToCatch, TExceptionToThrow>(
            Action action,
            string errorTextToIncludeInThrownException = null)
            where TExceptionToCatch : Exception
            where TExceptionToThrow : Exception
            =>
            To<Void, TExceptionToCatch, TExceptionToThrow>(
                () =>
                {
                    action?.Invoke();
                    return new Void();
                },
                errorTextToIncludeInThrownException);
    }
}
