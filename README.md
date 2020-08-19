## damphat.Json

## Json with advanced features
* lightweight javascript with hight performance
* basic expressions 2*(a+b)/c
* variables
* single quote
* optional commas
* trailing commas
* interact with .NET objects and methods via context
* and mores ...

### basic
```cs

json = @"
{
  city: 'Saigon'
  location: [10, 100]
}
";

var city = JSON.Parse(json);
```

### Use it as a calculator
```cs
var result = JSON.Parse(" (145 + 47) / 2 ");
```

### Hack with context
```cs
var context = new Dictionary<string, object>();
context["PI"] = Math.PI;

var result = JSON.Parse(" 2 * PI ", context);
```
