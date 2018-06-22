@ECHO OFF

IF "%CONFIGURATION%"=="" SET CONFIGURATION=Debug

star %* --resourcedir="%~dp0src\UniformDocs\wwwroot" "%~dp0src/UniformDocs/bin/%CONFIGURATION%/UniformDocs.exe"