# FlatMVVM
FlatMVVM is a small MVVM framework split into a cross-platform `TT.FlatMVVM.Core` package targeting `net8.0` and `net10.0`
and a WPF-specific `TT.FlatMVVM.Wpf` package targeting `net8.0-windows` and `net10.0-windows`.
Its focus is on the View - ViewModel part of MVVM by providing ViewModel base classes, ICommand implementations
and some often used IValueConverter implementations.

[![Nuget Core](https://img.shields.io/nuget/v/TT.FlatMVVM.Core?style=for-the-badge&label=TT.FlatMVVM.Core)](https://www.nuget.org/packages/TT.FlatMVVM.Core/)
[![Nuget WPF](https://img.shields.io/nuget/v/TT.FlatMVVM.Wpf?style=for-the-badge&label=TT.FlatMVVM.Wpf)](https://www.nuget.org/packages/TT.FlatMVVM.Wpf/)

### Features
+ ViewModel base classes implementing INotifyPropertyChanged and optional INotifyDataErrorInfo validation
+ ICommand implementations via DelegateCommand, ICommandFactory and command event strategies
+ EventBinding of Commands via Microsoft.Xaml.Behaviors.Wpf based TriggerActionCommand
+ BindingProxy to access DataContext when not inherited
+ ValueConverter Markup Extension

## Migrate from 1.4.2 to 2.0.0:
Version 2.0.0 is a breaking package split. The previous `TT.FlatMVVM` package was replaced by `TT.FlatMVVM.Core` and `TT.FlatMVVM.Wpf`.

1. Retarget projects. `TT.FlatMVVM.Core` targets `net8.0` and `net10.0`; `TT.FlatMVVM.Wpf` targets `net8.0-windows` and `net10.0-windows`. Projects still targeting `net48`, `netcoreapp3.1` or `net5.0-windows` must be retargeted before upgrading.
2. Replace NuGet references. Remove `TT.FlatMVVM`. Add `TT.FlatMVVM.Core` for ViewModel and command types. WPF applications that use converters, `TriggerActionCommand`, `BindingProxy`, control ViewModels or WPF command invalidation should also add `TT.FlatMVVM.Wpf`.
3. Update C# namespaces:

| 1.x namespace | 2.0.0 namespace | Types |
| --- | --- | --- |
| `TT.FlatMVVM` | `TT.FlatMVVM.Core` | `FlatVM`, `FlatValidationVM`, `DelegateCommand`, `ICommandFactory`, `CommandFactory`, `ICommandEventStrategy`, `LocalEventStrategy` |
| `TT.FlatMVVM.Converter` | `TT.FlatMVVM.Wpf.Converter` | WPF value converters |
| `TT.FlatMVVM.Utils` | `TT.FlatMVVM.Wpf.Utils` | `BindingProxy`, `BooleanParameter`, `TriggerActionCommand`, `UI` |
| `TT.FlatMVVM.ControlVM` | `TT.FlatMVVM.Wpf.ControlVM` | `MenuItemVM`, `TabControlVM` |

4. Update XAML namespace declarations:

```xml
xmlns:converter="clr-namespace:TT.FlatMVVM.Wpf.Converter;assembly=TT.FlatMVVM.Wpf"
xmlns:utils="clr-namespace:TT.FlatMVVM.Wpf.Utils;assembly=TT.FlatMVVM.Wpf"
```

5. Update command creation. `DelegateCommand` constructors now require an `ICommandEventStrategy`. In WPF apps, register `WpfEventStrategy` and prefer creating commands through `ICommandFactory`.

```csharp
services.AddSingleton<ICommandEventStrategy, WpfEventStrategy>();
services.AddSingleton<ICommandFactory, CommandFactory>();
```

```csharp
private readonly ICommandFactory _commandFactory;
private ICommand? _saveCommand;

public ExampleVM(ICommandFactory commandFactory)
{
    _commandFactory = commandFactory;
}

public ICommand Save => _saveCommand ??= _commandFactory.Create(ExecuteSave, CanExecuteSave);
```

If you create commands manually, pass a strategy explicitly:

```csharp
var command = new DelegateCommand(new WpfEventStrategy(), ExecuteSave, CanExecuteSave);
```

For non-WPF usage, use `LocalEventStrategy` and call `RaiseCanExecuteChanged()` on the command when its executable state changes.

6. Move validating ViewModels to `FlatValidationVM`. `FlatVM` now implements only `INotifyPropertyChanged`. ViewModels that use `HasErrors`, `GetErrors`, `ErrorsChanged` or validation-rule overloads of `SetProperty` should inherit from `FlatValidationVM`.

## Migrate from 1.0.1 to 1.1.0:
Version 1.1.0 uses [Microsoft.Xaml.Behaviors.Wpf](https://www.nuget.org/packages/Microsoft.Xaml.Behaviors.Wpf/) instead of [Expression.Blend.Sdk](https://www.nuget.org/packages/Expression.Blend.Sdk/1.0.2).
Also the default namespace has changed from FlatMVVM to TT.FlatMVVM. When you upgrade your project won't compile.
1. Update FlatMVVM
2. In xaml replace *xmlns:i="http://schemas.microsoft.com/expression/2010/interactivity"* by *xmlns:i="http://schemas.microsoft.com/xaml/behaviors"*
3. In ViewModels replace using FlatMVV; by using TT.FlatMVVM

## Migrate from 1.2.2 to 1.3.0:
Version 1.3.0 changed the way parameters are passed into DelegateCommands.

Parameterless Command in ViewModel
``` csharp
new DelegateCommand(e => ExecuteExportData(), ce => CanExecuteExportData())
```
changes to
``` csharp
new DelegateCommand(ExecuteExportData, CanExecuteExportData)
```

## 1. Binding Basics
### 1.1. Binding Properties

**Example1:** Binding content and IsEnabled state of a button to properties in a viewmodel showing two different styles of property setter implementation.

``` xml
<Button Content="{Binding Content1}" IsEnabled="{Binding Bool1}" />
```

``` csharp
internal class ViewModel : FlatVM
{
    private bool _bool1;
    private string _content1;

    public bool Bool1
    {
        get => _bool1;
        set
        {
            if (value == _bool1)
                return;
            _bool1 = value;
            OnPropertyChanged();
        }
    }

    public string Content1
    {
        get => _content1;
        set => SetProperty(ref _content1, value);
    }

    public ViewModel()
    {
        Content1 = "Try to click me";

        _timer = new Timer(1000);
        _timer.Elapsed += (sender, args) => { Bool1 = !Bool1; };
        _timer.Start();
    }
}
```
In this example two different options are shown to implement the property setter. For Bool1 property the classic OnPropertyChanged notation is used. Doing it this way you have full control over the setter implementation. But in most cases the setter implementation of a viewmodel property is like doing the equality check, updating the backing field if check evaluated to false and then raising the PropertyChanged event. To shorten that Content1 property uses the SetProperty() method which does exactly the basic operations to update a property. FlatMVVM offers both possibilities.

**Example2:** Binding content of TextBox and TextBlock controls to string properties. Updating further properties

``` csharp
public string FistName
{
    get => _fistName;
    set => SetProperty(ref _fistName, value, new[] { nameof(FullName), nameof(Email) /*add more properties here*/ });
}

public string LastName
{
    get => _lastName;
    set => SetProperty(ref _lastName, value, new[] { nameof(FullName), nameof(Email) /*add more properties here*/ });
}

public string FullName => $"{FistName} {LastName}";

public string Email => $"{FistName.ToLower()}.{LastName.ToLower()}@flatmvvm.com";
```
In this example FirstName and LastName properties are read/write properties that have dependent readonly properties FullName and Email. The SetProperty notation lets you update these properties by providing a string array of properties that should also be updated. Using nameof() this can easily be achieved while not relying on hard coded strings. 

### 1.2. Binding Commands

**Example1:** Binding *Click Command* of a button to a command defined in ViewModel.

XAML:
```xml
<Button Content="ClickButton" Command="{Binding SimpleClick}" />
```
ViewModel:
```csharp
private readonly ICommandFactory _commandFactory;
private ICommand? _simpleClickCommand;

public ViewModel(ICommandFactory commandFactory)
{
    _commandFactory = commandFactory;
}

public ICommand SimpleClick => _simpleClickCommand ??= _commandFactory.Create(ExecuteSimpleClickCommand);

private void ExecuteSimpleClickCommand()
{
    Debug.WriteLine("Simple Click Command");
}
```
**Note:** Command in this example is initialized lazily. The factory creates the command when the binding first evaluates the command property, not when the ViewModel is constructed.

### 1.3. Binding Events

**Example1:** Binding to *Loaded Event* of a Window to a command defined in ViewModel. This is realized using *Microsoft.Xaml.Behaviors.Wpf* library which can be used by adding *xmlns:b="http://schemas.microsoft.com/xaml/behaviors"* namespace to the Window.

XAML:
```xml
<Window x:Class="WpfCoreDemo.Part4.Part4Window"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:WpfCoreDemo.Part4"
        xmlns:b="http://schemas.microsoft.com/xaml/behaviors"
        xmlns:utils="clr-namespace:TT.FlatMVVM.Wpf.Utils;assembly=TT.FlatMVVM.Wpf"
        mc:Ignorable="d"
        d:DataContext="{d:DesignInstance local:Part4VM}"
        Title="Commands" Height="450" Width="800">
    <b:Interaction.Triggers>
        <b:EventTrigger EventName="Loaded">
            <utils:TriggerActionCommand Command="{Binding WindowLoaded}" />
        </b:EventTrigger>
    </b:Interaction.Triggers>
</Window>
```
ViewModel:
```csharp
private readonly ICommandFactory _commandFactory;
private ICommand? _windowLoadedCommand;

public ViewModel(ICommandFactory commandFactory)
{
    _commandFactory = commandFactory;
}

public ICommand WindowLoaded => _windowLoadedCommand ??= _commandFactory.Create<RoutedEventArgs>(ExecuteWindowLoaded);

private void ExecuteWindowLoaded(RoutedEventArgs? args)
{
    if (args == null) return;

    Debug.WriteLine($"{((Window)args.OriginalSource).Title} loaded.");
}
```
**Note:** This example passes the *RoutedEventArgs* of the Window Loaded event to the command to demonstrate how event parameter can be passed to commands. If the event arguments are not needed, remove the TypeParameter from DelegateCommand and corresponding command delegates. 

## 2. Converter

FlatMVVM provides implementations of some binding converter that may be used very often.

### 2.1 ValueConverter
#### InverseBooleanConverter

Use InverseBooleanConverter to bind to bool properties and invert their value when propagated to binding target. When binding to nullable bool properties the converter allows to specify a NullValue.

**Example1:**
``` xml
<Button IsEnabled="{Binding BoolProperty, Converter={converter:InverseBooleanConverter}}" />
```

**Example2:**
``` xml
 <Button IsEnabled="{Binding NullableBoolProperty, Converter={converter:InverseBooleanConverter NullValue=False}}" />
```

### 2.2 MultiValueConverter
#### MultiBooleanConverter

MultiBooleanConverter allows to link together multiple bool properties when using a MultiBinding. Its Operators property is used to define the operator.

**Example1:** Using MultiBooleanConverter to link two bool properties in a MultiBinding.
``` xml
<Button Content="MultiBinding" VerticalAlignment="Top">
    <Button.IsEnabled>
        <MultiBinding Converter="{converter:MultiBooleanConverter Operators=A}">
            <Binding Path="Bool1" Converter="{converter:InverseBooleanConverter}" />
            <Binding Path="Bool2" />
        </MultiBinding>
    </Button.IsEnabled>
</Button>
```

## 3. Utils
### 3.1. UI
### 3.2. Binding Proxy
### 3.3 BooleanParameter
BooleanParameter is a static type that can be used when static bool values should be defined in xaml code.

**Example 1:** Using BooleanParameter to define a CommandParameter
``` xml
CommandParameter="{x:Static utils:BooleanParameter.False}"
```
