@ECHO OFF

IF "%CONFIGURATION%"=="" SET CONFIGURATION=Debug

SET INTERACTIVE=1
ECHO %CMDCMDLINE% | find /i "%~0" >nul
IF NOT ERRORLEVEL 1 SET INTERACTIVE=0

:: Stop all the running apps
staradmin stop db default

:: Start the tested app
star --resourcedir="%~dp0src\UniformDocs\wwwroot" "%~dp0src/UniformDocs/bin/%Configuration%/UniformDocs.exe"
IF ERRORLEVEL 1 EXIT /B 1

:: Start the test
IF NOT EXIST "%~dp0packages\nunit.consolerunner\3.10.0\" (ECHO Error: Cannot find NUnit Console Runner. Build the project to restore the packages && PAUSE && EXIT /B 1)
%~dp0packages\nunit.consolerunner\3.10.0\tools\nunit3-console.exe %~dp0test\UniformDocs.Tests\bin\Debug\UniformDocs.Tests.dll --noheader %*

if %interactive%==0 pause

IF ERRORLEVEL 1 EXIT /B 1