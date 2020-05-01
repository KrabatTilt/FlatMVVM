# FlatMVVM
FlatMVVM is a small MVVM framework supporting WPF netFramework and WPF netCore applications.
Its focus is on the View - ViewModel part of MVVM by providing base class for ViewModel, ICommand implementation 
and some often used IValueConverter implementations.

[![Nuget](https://img.shields.io/nuget/v/TT.FlatMVVM?style=for-the-badge)](https://www.nuget.org/packages/TT.FlatMVVM/) 

### Features
+ ViewModel BaseClass implementing INotifyPrypertyChanged and INotifyDataErrorInfo
+ ICommand implemented as DelegateCommand
+ EventBinding of Commands via Microsoft.Xaml.Behaviors.Wpf based TriggerActionCommand
+ BindingProxy to access DataContext when not inherited
+ ValueConverter Markup Extension

### TODO
+ Binding to readonly DependencyProperties
+ Async DelegateCommand implementation


## Migrate from 1.0.1 to 1.1.0:
Version 1.1.0 uses [Microsoft.Xaml.Behaviors.Wpf](https://www.nuget.org/packages/Microsoft.Xaml.Behaviors.Wpf/) instead of [Expression.Blend.Sdk](https://www.nuget.org/packages/Expression.Blend.Sdk/1.0.2).
Also the default namespace has changed from FlatMVVM to TT.FlatMVVM. When you upgrade your project won't compile.
1. Update FlatMVVM
2. In xaml replace *xmlns:i="http://schemas.microsoft.com/expression/2010/interactivity"* by *xmlns:i="http://schemas.microsoft.com/xaml/behaviors"*
3. In ViewModels replace using FlatMVV; by using TT.FlatMVVM

## Migrate from 1.2.2 to 1.3.0:
Version 1.3.0 changed the way parameters are passed into DelegateCommands.

Parameterless Command in ViewModel
``` cs
new DelegateCommand(e => ExecuteExportData(), ce => CanExecuteExportData())
```
changes to
``` cs
new DelegateCommand(ExecuteExportData, CanExecuteExportData)
```

## 1. Binding Basics
### 1.1. Binding Properties
### 1.2. Binding Commands
### 1.3. Binding Events
## 2. Converter
## 3. Utils
### 3.1. UI
### 3.2. Binding Proxy
