NetHacksPack.Hosting.Environment
=====================
This package helps you to handle properly Environment variables parse the stored value to a CLR type and also to a complex type 

### Why use NetHacksPack.Hosting.Environment
---
Istead of try to parse environment variables and concerns about null values, serialization error or not found variables, you can just call a method that will do the hard work for you

#### How to use
---
It's very simple to use it, instead of try to parse environment variables and concerns about null values, serializing error or notfound variables, you can just call a method that will do the hard work for you

```c#
using NetHacksPack.Hosting.Environment;

// to get a bool
bool isProduction = "MyEnvironmentIsProductionVariableName".GetBool();

// to get an integer
int maxTries = "MyEnvironmentMaxTriesVariableName".GetInt();

// to get a long
long ticks = "MyEnvironmentTicksVariableName".GetLong();

// to get a string
string environmentName = "MyEnvironmentEnvironemntNameVariableName".GetString();

```

If you need a default value in cases that the variable is not found, you can use use the second argument

```c#
using NetHacksPack.Hosting.Environment;

// the default value if miss this parameter is false
bool isProduction = "MyEnvironmentIsProductionVariableName".GetBool(defaultValue: false);

// the default value if miss this parameter is 0
int maxTries = "MyEnvironmentMaxTriesVariableName".GetInt(defaultValue: 3);

// the default value if miss this parameter is 0
long ticks = "MyEnvironmentTicksVariableName".GetLong(defaultValue: 10);

// the default value if miss this parameter is null
string environmentName = "MyEnvironmentEnvironemntNameVariableName".GetString(defaultValue: "Development");

```

Also you can throw an exception in case some error happened when try deserialize the environment variable
The most common exception is InvalidEnvironmentVariableValueException when the variable is not found

```c#
using NetHacksPack.Hosting.Environment;

// the default value if miss this parameter is false
bool isProduction = "MyEnvironmentIsProductionVariableName".GetBool(throwExceptionIfNotFoundOrInvalid: true);

int maxTries = "MyEnvironmentMaxTriesVariableName".GetInt(throwExceptionIfNotFoundOrInvalid: true);

long ticks = "MyEnvironmentTicksVariableName".GetLong(throwExceptionIfNotFoundOrInvalid: true);

'string environmentName = "MyEnvironmentEnvironemntNameVariableName".GetString(throwExceptionIfNotFoundOrInvalid: true);

```
### Boolean types
---

A little trick is that boolean values can be deserialized by some string patterns:

```c#
using NetHacksPack.Hosting.Environment;

System.Environment.SetEnvironmentVariable("MyEnvironmentIsProductionVariableName", "YES")
bool isProduction = "MyEnvironmentIsProductionVariableName".GetBool();
Console.WriteLine(isProduction);
// prints true

System.Environment.SetEnvironmentVariable("MyEnvironmentIsProductionVariableName", "Y")
bool isProduction = "MyEnvironmentIsProductionVariableName".GetBool();
Console.WriteLine(isProduction);
// prints true

System.Environment.SetEnvironmentVariable("MyEnvironmentIsProductionVariableName", "SIM")
bool isProduction = "MyEnvironmentIsProductionVariableName".GetBool();
Console.WriteLine(isProduction);
// prints true

System.Environment.SetEnvironmentVariable("MyEnvironmentIsProductionVariableName", "S")
bool isProduction = "MyEnvironmentIsProductionVariableName".GetBool();
Console.WriteLine(isProduction);
// prints true

System.Environment.SetEnvironmentVariable("MyEnvironmentIsProductionVariableName", "TRUE")
bool isProduction = "MyEnvironmentIsProductionVariableName".GetBool();
Console.WriteLine(isProduction);
// prints true

System.Environment.SetEnvironmentVariable("MyEnvironmentIsProductionVariableName", "T")
bool isProduction = "MyEnvironmentIsProductionVariableName".GetBool();
Console.WriteLine(isProduction);
// prints true

System.Environment.SetEnvironmentVariable("MyEnvironmentIsProductionVariableName", "NO")
bool isProduction = "MyEnvironmentIsProductionVariableName".GetBool();
Console.WriteLine(isProduction);
// prints false

System.Environment.SetEnvironmentVariable("MyEnvironmentIsProductionVariableName", "N")
bool isProduction = "MyEnvironmentIsProductionVariableName".GetBool();
Console.WriteLine(isProduction);
// prints false

System.Environment.SetEnvironmentVariable("MyEnvironmentIsProductionVariableName", "NAO")
bool isProduction = "MyEnvironmentIsProductionVariableName".GetBool();
Console.WriteLine(isProduction);
// prints false

System.Environment.SetEnvironmentVariable("MyEnvironmentIsProductionVariableName", "FALSE")
bool isProduction = "MyEnvironmentIsProductionVariableName".GetBool();
Console.WriteLine(isProduction);
// prints false

System.Environment.SetEnvironmentVariable("MyEnvironmentIsProductionVariableName", "F")
bool isProduction = "MyEnvironmentIsProductionVariableName".GetBool();
Console.WriteLine(isProduction);
// prints false

```

### Complex objects
---

This packages also allow you to get complex objects from the environment variables

```c#

using NetHacksPack.Hosting.Environment;

public class EnvironmentOptions
{
	public bool IsProduction { get; set; }

	public string Name { get; set; }
}

System.Environment.SetEnvironmentVariable("environmentOptions", "{ IsProduction: false, Name: \"Development\"}");

var myEnvironmentOptions = "environmentOptions".Get<EnvironmentOptions>();

Console.WriteLine(myEnvironmentOptions.IsProduction);
// prints false

Console.WriteLine(myEnvironmentOptions.Name);
// prints Development

```