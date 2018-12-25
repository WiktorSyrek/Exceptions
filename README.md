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
