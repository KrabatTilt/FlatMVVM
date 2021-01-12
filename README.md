# FlatMVVM
FlatMVVM is a small MVVM framework supporting net45 - net48, netcoreapp3.1 and net5.0-windows applications.
Its focus is on the View - ViewModel part of MVVM by providing base class for ViewModel, ICommand implementation 
and some often used IValueConverter implementations.

[![Nuget](https://img.shields.io/nuget/v/TT.FlatMVVM?style=for-the-badge)](https://www.nuget.org/packages/TT.FlatMVVM/) 

### Features
+ ViewModel BaseClass implementing INotifyPropertyChanged and INotifyDataErrorInfo
+ ICommand implemented as DelegateCommand
+ EventBinding of Commands via Microsoft.Xaml.Behaviors.Wpf based TriggerActionCommand
+ BindingProxy to access DataContext when not inherited
+ ValueConverter Markup Extension


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
private ICommand _simpleClickCommand;

public ICommand SimpleClick => _simpleClickCommand ??= new DelegateCommand(ExecuteSimpleClickCommand);

private void ExecuteSimpleClickCommand()
{
    Debug.WriteLine("Simple Click Command");
}
```
**Note:** The Command in this example is initialized lazy in terms of when the ViewModel is created no instance of the DelegateCommand is created. But when the View is loaded the Command property gets evaluated and an instance of DelegateCommand is created. So it is not created on the first call triggered by the user.

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
        xmlns:utils="clr-namespace:TT.FlatMVVM.Utils;assembly=TT.FlatMVVM"
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
private ICommand _windowLoadedCommand;

public ICommand WindowLoaded => _windowLoadedCommand ??= new DelegateCommand<RoutedEventArgs>(ExecuteWindowLoaded);

private void ExecuteWindowLoaded(RoutedEventArgs args)
{
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
