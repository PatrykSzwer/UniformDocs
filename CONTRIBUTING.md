# Developer instructions

## Branches

This repo supports Starcounter 2.4.0 with Polymer 2 (`master` branch) and Starcounter 2.3.1 with Polymer 1 (`master-2.3` branch).

All changes should be as a PR to the `master` branch. 

The branch `master-2.3` does not get updates anymore and is only for reference purposes.

### Build scripts specific prerequisites

* Install the latest global .NET Core Cake tool by running `dotnet tool install -g Cake.Tool`.
* Add [nuget.exe](https://www.nuget.org/downloads) to PATH.
* Make sure that the MSBuild version that is used comes from your Visual Studio installation and not from a previous installation of .NET Framework. The easiest way to test this is to run `msbuild /version` which should return a `15.X` version. If it returns something like `4.X`, then you have to either
    * add `C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin` to PATH (check both System AND User).  Make sure it's inserted before any `C:\Windows\Microsoft.NET\Framework\v4.0.30319`
    * or run `C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\Tools\VsDevCmd.bat` which will only set proper environment variable values for the current process.

## How to build and run

### Build and run using CLI

1. Clone this repo to your local machine
2. Build the solution by executing `build.bat` in Windows or `./build.bat` in Git Bash
3. Run it by executing `run.bat` in Windows or `./run.bat` in Git Bash
4. Go to [http://localhost:8080/UniformDocs](http://localhost:8080/UniformDocs)

### Build and run using Visual Studio

1. Clone this repo to your local machine
2. Open the `.sln` file in Visual Studio
3. Build and run it using Debug in Visual Studio (**Debug** > **Start debugging** or <kbd>F5</kbd>)
4. Go to [http://localhost:8080/UniformDocs](http://localhost:8080/UniformDocs)

## Contributing code

To contribute code to this repository, follow the instructions in the [guidelines](https://github.com/Starcounter/CompanyTrack/blob/master/AppsTeam/Guidelines/version_control/contributing_code.md).

## How to release a package

To release the app to the warehouse, follow the instructions in the [guidelines](https://github.com/Starcounter/CompanyTrack/blob/master/AppsTeam/Guidelines/releasing-to-warehouse.md).

## How to deploy to `uniform.starcounter.io`

1. Make sure that the new version of UniformDocs is released to the App Warehouse
2. Connect via RDP to the AWS EC2 instance of hosted UniformDocs (credentials in `N*********.kdbx`)
3. In RDP, wipe out the current data by running `staradmin delete db default` 
4. Install the current version of Starcounter 2.4 from http://downloads.starcounter.com/
5. Start Starcounter by running `staradmin start server`
6. Create a new database on port 80 by running `staradmin new db default DefaultUserHttpPort=80`
7. In Administrator, go to the database `default` and install the current version of UniformDocs from App Warehouse
8. Go to https://uniform.starcounter.io/ to verify that the new UniformDocs is correctly deployed

## Testing

### Prepare your environment for cloud Selenium (default)

Before running the steps, you need to:

- Download and install Visual Studio 2017 to run the tests
- Install NUnit 3 Test Adapter in VS (Tools > Extensions and Updates... > Online) in order to see tests in Test Explorer window
- Set `BROWSERSTACK_USERNAME` (this is not the e-mail address!) and `BROWSERSTACK_ACCESS_KEY` envionment variables  (credentials in `N*********.kdbx`). Restart Visual Studio 2017 to acknowledge the changes.
- Get BrowserStackLocal CLI binary from [the download page](https://www.browserstack.com/local-testing#command-line) (CTRL+F "Download the appropriate binary")
- Run `BrowserStackLocal.exe --key %BROWSERSTACK_ACCESS_KEY% --local-identifier %COMPUTERNAME%`


### Prepare your environment for local Selenium

Before running the steps, you need to:

- Download and install Visual Studio 2017 to run the tests
- Install NUnit 3 Test Adapter in VS (Tools > Extensions and Updates... > Online) in order to see tests in Test Explorer window
- Download and install Java, required by Selenium Standalone Server
- Download Selenium Standalone Server and the drivers [Microsoft WebDriver (Edge), Google ChromeDriver (Chrome) and Mozilla GeckoDriver (Firefox)] using [these instructions](https://docs.starcounter.io/cookbook/acceptance-testing-with-selenium).
- Add path to the folder with drivers to system path on your computer
- Start Selenium Remote Driver using `java -jar selenium-server-standalone-3.*.jar -enablePassThrough false`

### Run the test (from Visual Studio)

1. Open `UniformDocs.sln` in Visual Studio and enable Test Explorer (Test > Window > Test Explorer)
2. Start the UniformDocs app
3. Press "Run all" in Test Explorer
   - If you get an error about some packages not installed, right click on the project in Solution Explorer. Choose "Manage NuGet Packages" and click on "Restore".

### Run the test (from command line)

1. Build the solution (`build.bat`)
2. Start the UniformDocs.Test runner (`test.bat`)

To run a specific test, add the param `--test="<testname>"`.

By default, it connects to cloud Selenium (BrowserStack). To connect to a local Selenium, add the param `--params="Server=<Uri>"`, e.g. `--params="Server=http://localhost:4444/wd/hub/`.

To run in a specific browser, add the param `--params="Browsers=<BrowserName>"` (case sensitive). Possible values: `Chrome`, `Firefox`, `Edge` (separated by a comma). 

```
test --params="Server=http://192.168.1.49:4444/wd/hub" --params="Browsers=Chrome" --test="UniformDocs.Tests.Test.TextareaPageTest(Chrome).TextareaPage_WriteToTextArea"
```
