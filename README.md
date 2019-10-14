# FlatMVVM
FlatMVVM is a simple MVVM framework for WPF and WPF netCore applications with focus on MVVM basics. 
Therefore it does not provide its own DI container implementaion or other mechanics related to DI.

[![flatmvvm MyGet Build Status](https://www.myget.org/BuildSource/Badge/flatmvvm?identifier=63c10aac-91d5-4311-bff7-59e86dafe8b8)](https://www.myget.org/)

NuGet: [FlatMVVM](https://www.nuget.org/packages/TT.FlatMVVM/)

### Features
+ Supports Wpf net Core projects
+ ViewModel BaseClass implementing INotifyPrypertyChanged and INotifyDataErrorInfo
+ ICommand as DelegateCommand
+ EventBinding of Commands via System.Windows.Interactivity based TriggerActionCommand
+ BindingProxy to access DataContext when not inherited
+ Binding to readonly DependencyProperties (TODO)


## Migrate from 1.0.1 to 1.1.0:
Version 1.1.0 uses [Microsoft.Xaml.Behaviors.Wpf](https://www.nuget.org/packages/Microsoft.Xaml.Behaviors.Wpf/) instead of [Expression.Blend.Sdk](https://www.nuget.org/packages/Expression.Blend.Sdk/1.0.2).
Also the default namespace has changed from FlatMVVM to TT.FlatMVVM. When you upgrade your project won't compile.
1. Update FlatMVVM
2. In xaml replace *xmlns:i="http://schemas.microsoft.com/expression/2010/interactivity"* by *xmlns:i="http://schemas.microsoft.com/xaml/behaviors"*
3. In ViewModels replace using FlatMVV; by using TT.FlatMVVM


## 1. Basic Binding
### 1.1. Binding Properties
### 1.2. Binding Commands
### 1.3. Binding Events
## 2. Utils
### 2.1. UI
### 2.2. Binding Proxy
