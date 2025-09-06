:: Courtesy of Google Gemini
@echo off
setlocal

:: Define the tool name
set "TOOL_NAME=dotnet-stryker"
set "TOOL_VERSION=4.8.*"

echo Attempting to update %TOOL_NAME%...

dotnet tool update -g %TOOL_NAME% --version %TOOL_VERSION% --allow-downgrade

:: Check the exit code of the previous command
if %errorlevel% neq 0 (
	echo %TOOL_NAME% is not installed or update failed. Attempting to install...

	dotnet tool install -g %TOOL_NAME% --version %TOOL_VERSION%

	if %errorlevel% neq 0 (
		echo Failed to install %TOOL_NAME%
	) else (
		echo %TOOL_NAME% installed successfully
	)
) else (
	echo %TOOL_NAME% updated successfully
)

endlocal

:: Run Stryker
cd SomeUtilities.Tests
dotnet stryker -o -f stryker-config.json