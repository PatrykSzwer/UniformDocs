# KitchenSink

Shows how to model different UI patterns in JSON:

| String  | Number  | Boolean | Object  | Array  | Custom  |
|---|---|---|---|---|---|
|  Text |  Integer | Checkbox  |  Nested partials | Radio  | File upload  |
| Password  |  Decimal | Togglebutton  |   | Dropdown  | Set a cookie  |
| Textarea  |  Button |   |   |  Radiolist | Dialog box  |
| Markdown  | Map  |   |   | Multiselect  |  Callback behavior |
| Html  |   |   |   | Table  | Autocomplete  |
| Datepicker  |   |   |   | Datagrid  | Progress bar  |
| Url  |   |   |   |  Chart |  Lazy loading |
| Redirect  |   |   |   | Breadcrumb  | Pagination  |
| Validation  |   |   |   |   | Flash Message  |

## Video

Intended for 13 October 2015 webinar: http://starcounter.io/video-expressing-your-ui-in-json-plain-data-binding-advanced-data-binding/

## Screenshot

![](screenshot.PNG)

## Testing

### Prepare your environment

Before running the steps, you need to:

- Download and install Visual Studio 2015 to run the tests
- Download and install Java, required by Selenium Standalone Server
- Download Selenium Standalone Server and the drivers (Microsoft WebDriver (Edge), Google ChromeDriver (Chrome) and Mozilla GeckoDriver (Firefox)) using the instructions at http://starcounter.io/guides/web/acceptance-testing-with-selenium/#install-selenium-standalone-server-and-browser-drivers
- Add path to the folder with drivers to system path on your computer

### Run the test (from Visual Studio)

1. Start Selenium Remote Driver: `java -jar selenium-server-standalone-3.*.jar`
2. Open `KitchenSink.sln` in Visual Studio and enable Test Explorer (Test > Window > Test Explorer)
3. You need to install NUnit 3 Test Adapter in VS addon window in order to see tests in Test Explorer window
3. Start the KitchenSink app
4. Press "Run all" in Test Explorer
   - If you get an error about some packages not installed, right click on the project in Solution Explorer. Choose "Manage NuGet Packages" and click on "Restore".

### Run the test (from command line)

1. Start Selenium Remote Driver: `java -jar selenium-server-standalone-3.*.jar`
2. Build the solution (build.bat)
3. Run the KitchenSink app (run.bat)
4. Start the KitchenSink.Test runner (test.bat)

## License

MIT
