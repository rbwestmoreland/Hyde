@echo Off

rd artifacts
md artifacts

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild ..\Hyde.sln /p:Configuration=Release /p:OutputPath=..\Build\artifacts\ /flp:LogFile=artifacts\msbuild.log;Verbosity=Normal
::pause