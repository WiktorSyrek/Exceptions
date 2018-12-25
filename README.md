# Exceptions

This module offers the following exception helpers:

- Boundary
- Convert
- Ignore

## Boundary

Since a lot of exceptions are undocumented in the NET framework or third party libraries code ends up with a lot of

``` C#

try{

}
catch (Exception ex){

}

```

blocks. In my oppionion these blocks should only work as exception boundary. Since it is unclear if a `try-catch`-anything-block works as exception boundary, an explicit "statement" would be an improvement. The static `Boundary` class offers such an statement:

```C#

 [Test]
public void CatchDoesntPopulateException()
{
  Assert.DoesNotThrow(
    () => Boundary.CatchAll(
      () => throw new Exception("Test")
    )
  );
}

```

As the name says `CatchAll` catches all kind of exceptions. `CatchAll` accepts an additional (optional) delegate populating the cought exception to e.g. log it to a file.

## Convert

The `Convert` class translates `ExceptionA` to `ExceptionB`. This behaviour is usefull to translate a standard exception to a custom one. Example: You've a config class, every error during config loading, parsing, validating shall be communicated via an `ConfigException`. Based on that you're able to e.g. wrap a `FileNotFound`-exception in a `ConfigException` (where the inner exeption is `FileNotFound`).

Example:

```C#
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

```
