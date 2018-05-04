# FlatMVVM
FlatMVVM is a simple MVVM framework for WPF applications with focus on MVVM basics. It is intended for use in small and medium size projects. It does not contain a dependeny injection framework implementation.

[![flatmvvm MyGet Build Status](https://www.myget.org/BuildSource/Badge/flatmvvm?identifier=63c10aac-91d5-4311-bff7-59e86dafe8b8)](https://www.myget.org/)

NuGet: [FlatMVVM](https://www.nuget.org/packages/TT.FlatMVVM/)

### Features
+ ViewModel BaseClass implementing INotifyPrypertyChanged and INotifyDataErrorInfo
+ ICommand as DelegateCommand
+ EventBinding of Commands via System.Windows.Interactivity based TriggerActionCommand
+ BindingProxy to access DataContext when not inherited
+ Binding to readonly DependencyProperties (TODO)


## Note:
FlatMVVM is currently limited to .net >= 4.5 as FlatVM currently implements INotifyDataErrorInfo which is a .net 4.5 feature. In the future there will also be a .net 4.0 version available.

## 1. Basic Binding
### 1.1. Binding Properties
### 1.2. Binding Commands
### 1.3. Binding Events
## 2. Utils
### 2.1. UIWrapper
### 2.2. Binding Proxy
