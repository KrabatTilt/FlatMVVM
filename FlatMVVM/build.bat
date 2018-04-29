@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)
 
set version=1.0.0
if not "%PackageVersion%" == "" (
   set version=%PackageVersion%
)

set nuget=
if "%nuget%" == "" (
	set nuget=nuget
)


REM Build

"%programfiles(x86)%\MSBuild\15.0\Bin\MSBuild.exe" ../FlatMVVM.sln /t:FlatMVVM /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
Rem "%programfiles(x86)%\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\MSBuild.exe" ../FlatMVVM.sln /t:FlatMVVM /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false



REM Package

mkdir Build
mkdir Build\lib
mkdir Build\lib\net45

call %nuget% pack "FlatMVVM.nuspec" -verbosity detailed -symbols -o Build -Version %version% -p Configuration=%config%
REM %nuget% pack "FlatMVVM.nuspec" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"