Uniquify is a lightweigth liblary for generating unique strings or numbers.


# Install

```
dotnet add package Uniquify --version 1.0.0
```

# Usage

Uniquify contains several static methods that helps you shape your tokens the way you want.
`GetInt64` and `GetInt32` methods are useful for generating numbers. Based on your needs you can generate unique long or int types (int type can have a maximum length of 8 and long type can increase this number to 18). They both get an integer type which is responsible for the length of the generated number, and return a number with your specified length. But keep in mind that uniqueness comes from a bigger range, to keep safety of uniqueness, give greater length and work with greater types.

```
var unique32BitsNumber = Uniquify.GetInt32(4);
var unique64BitsNumber = Uniquify.GetInt64(14);
```

The `GetString` method like his other siblings will return an unique string which specific length that you specified. Keep in mind that to keep you safe from `StackOverFlow` exception The `GetString` method is limited to 1000 length.

```
 var uniqueString = Uniquify.GetString(18);
 var uniqueString = Uniquify.GetString("15987!@#./,?sdafreERTSFDOI", 40);
```

For the sake of performance is better to instantiate `RNGCryptoServiceProvider` for your own, if you want to generate thousand of token in a block

```
using var rngProvider = new RNGCryptoServiceProvider();
for (var i = 0; i < 100; i++)
{
    var uniqueString = rngProvider.GetString(10);
    var uniqueInt32 = rngProvider.GetInt32(4);
    var uniqueInt64 = rngProvider.GetInt64(10);
    DoSomething(uniqueString, uniqueInt32, uniqueInt64);
}
```