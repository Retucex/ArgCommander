# ArgCommander
ArgCommander is a small .NET library for eliminating some of the boilerplate when building a CLI.

## Installation
The easiest way to install ArgCommander is to add the NuGet package to your project either by the GUI in Visual Studio, or by using the NuGet console. 

[Link to project on nuget.org](https://www.nuget.org/packages/ArgCommander/1.0.0)

##### Package Manager
`Install-Package ArgCommander -Version 1.0.0`
##### .NET CLI
`dotnet add package ArgCommander --version 1.0.0`
##### Package Reference
`<PackageReference Include="ArgCommander" Version="1.0.0" />`

## Usage
ArgCommander is very simple to use. It relies on attribute-adorned properties to capture and parse the arguments passed to the application.
It can parse [Value Types](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/value-types) (except [Structs](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/struct)) and [Strings](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/strings/).

In order to use ArgCommander, we must first create a class to receive our arguments. The simplest form it can take is a single `bool` property:
```csharp
class Foo
{
    [CmdArg("-b")]					// The CmdArg attribute defines that "-b" is a valid argument.
    public bool Bar { get; set; }	// If "-b" is passed as an argument, Bar will be set to true.
}
```

Once our class is defined, we simply pass our `args[]` object to the static `ParseArgs<T>` method of Parser.
```csharp
using static ArgCommander.Parser;
class Program
{
  static void Main(string[] args)
  {
  	// If "-b" is present in args[], foo.Bar will be set to true.
  	Foo foo = ParseArgs<Foo>(args);
  }
}
```

If the properties we have adorned with the `CmdArg` attribute are of a type different than `bool`, ArgCommander expects the argument following the flag to be the value associated with said flag.

For example, let's consider the following class:
```csharp
class Foo
{
    [CmdArg("-b")]
    public int Bar { get; set; }
}
```

The property `Bar` is of type `int`. Let's say we wish to pass the value `3` to `Bar`. The `args[]` object will need to look like this:
```csharp
{ "-b", "3" }
```
Once parsed, our `Foo` object will have its `Bar` property set to `3`.

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)