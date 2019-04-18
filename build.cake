#addin nuget:?package=Newtonsoft.Json&version=11.0.2
#addin nuget:?package=Microsoft.CSharp&version=4.0.1
#addin nuget:?package=Cake.FileHelpers&version=3.1.0
using Microsoft.CSharp;

///
/// Private namespace
///
{
    ///
    /// Configuration from current script
    ///
    DirectoryPath rootDirectory = Context.GetCallerInfo().SourceFilePath.GetDirectory();
    string rootPath = rootDirectory.FullPath;
    string targetSubName = rootDirectory.GetDirectoryName();

    ///
    /// Argument parsing
    ///
    string configuration = Argument("configuration", "Debug");
    string starcounterBin = Argument("scBinForApps", "");
    string msBuildFullPath = Argument("msBuildFullPath", "");
    string browserStackApiUserEnvVar = Argument("browserStackApiUserEnvVar", "");
    string browserStackApiPasswordEnvVar = Argument("browserStackApiPasswordEnvVar", "");
    string msBuildPropertyAssemblyVersion = Argument("msBuildPropertyAssemblyVersion", "");
    string msBuildPropertyBuildCommonTargets = Argument("msBuildPropertyBuildCommonTargets", "");
    string starpackArtifactsPath = Argument("starpackArtifactsPath", "");

    ///
    /// Dependent targets
    ///
    Task(string.Format("Build{0}", targetSubName))
        .IsDependentOn(string.Format("Restore{0}", targetSubName))
        .IsDependentOn(string.Format("Build{0}I", targetSubName));

    Task(string.Format("Run{0}", targetSubName))
        .IsDependentOn(string.Format("Restore{0}", targetSubName))
        .IsDependentOn(string.Format("Build{0}I", targetSubName))
        .IsDependentOn(string.Format("Run{0}I", targetSubName));

    Task(string.Format("Test{0}", targetSubName))
        .IsDependentOn(string.Format("Restore{0}", targetSubName))
        .IsDependentOn(string.Format("Build{0}I", targetSubName))
        .IsDependentOn(string.Format("Run{0}I", targetSubName))
        .IsDependentOn(string.Format("TestUnit{0}I", targetSubName));

    Task(string.Format("TestAll{0}", targetSubName))
        .IsDependentOn(string.Format("TestUnit{0}I", targetSubName))
        .IsDependentOn(string.Format("TestIntegration{0}I", targetSubName))
        .IsDependentOn(string.Format("TestUiIntegration{0}I", targetSubName));

    Task(string.Format("Pack{0}", targetSubName))
        .IsDependentOn(string.Format("Restore{0}", targetSubName))
        .IsDependentOn(string.Format("Build{0}I", targetSubName))
        .IsDependentOn(string.Format("Run{0}I", targetSubName))
        .IsDependentOn(string.Format("TestUnit{0}I", targetSubName))
        .IsDependentOn(string.Format("Pack{0}I", targetSubName));

    ///
    /// Independent restore target
    ///
    Task(string.Format("Restore{0}", targetSubName)).Does(() =>
    {
        var settings = new NuGetRestoreSettings
        {
            NoCache = true,
            EnvironmentVariables = GetEnvironmentVariables(),
            Verbosity = NuGetVerbosity.Normal
        };

        NuGetRestore($"{rootPath}/{targetSubName}.sln", settings);
    });

    ///
    /// Independent build target
    ///
    Task(string.Format("Build{0}I", targetSubName)).Does(() =>
    {
        var settings = new MSBuildSettings
        {
            Configuration = configuration,
            EnvironmentVariables = GetEnvironmentVariables(),
            Verbosity = Verbosity.Minimal,
            Restore = false,
            MSBuildPlatform = MSBuildPlatform.x64,
            PlatformTarget = PlatformTarget.x64
        };

        if (!string.IsNullOrEmpty(msBuildPropertyAssemblyVersion))
        {
            settings.WithProperty("VersionAssembly", msBuildPropertyAssemblyVersion);
        }

        if (!string.IsNullOrEmpty(msBuildPropertyBuildCommonTargets))
        {
            settings.WithProperty("BuildCommonTargets", msBuildPropertyBuildCommonTargets);
        }

        if (!string.IsNullOrEmpty(msBuildFullPath))
        {
            settings.ToolPath = msBuildFullPath;
        }

        MSBuild($"{rootPath}/{targetSubName}.sln", settings);
    });

    ///
    /// Independent run target
    ///
    Task(string.Format("Run{0}I", targetSubName)).Does(() =>
    {
        string cliShell = "cmd";
        string cliArgs;
        ProcessSettings processSettings = new ProcessSettings
        {
            EnvironmentVariables = GetEnvironmentVariables(),
            WorkingDirectory = rootPath
        };

        // Run UniformDocs
        cliArgs = $"/c star.exe --resourcedir=\"{rootPath}\\src\\UniformDocs\\wwwroot\" \"{rootPath}\\src\\UniformDocs\\bin\\{configuration}\\UniformDocs.exe\"";
        processSettings.Arguments = new ProcessArgumentBuilder().Append(cliArgs);

        int exitCode = StartProcess(cliShell, processSettings);

        if(exitCode != 0)
        {
            throw new Exception(string.Format("Error while running: {0} {1}", cliShell, processSettings.Arguments.Render()));
        }
    });

    ///
    /// Independent TestUnit target
    ///
    Task(string.Format("TestUnit{0}I", targetSubName)).Does(() =>
    {
        Warning("This target has not been implemented yet.");
    });

    ///
    /// Independent TestIntegration target
    ///
    Task(string.Format("TestIntegration{0}I", targetSubName)).Does(() =>
    {
        Warning("This target has not been implemented yet.");
    });

    ///
    /// Independent TestUiIntegration target
    ///
    Task(string.Format("TestUiIntegration{0}I", targetSubName)).Does(() =>
    {
        RunAllHelperApps();
        RunAllTests();
    });

    ///
    /// Independent pack target
    ///
    Task(string.Format("Pack{0}I", targetSubName)).Does(() =>
    {
        string cliShell = "cmd";
        DirectoryPath starpackOutputDir = string.IsNullOrEmpty(starpackArtifactsPath) ?
            Directory($"{rootPath}/src") : MakeAbsolute(Directory(starpackArtifactsPath));

        if (!DirectoryExists(starpackOutputDir))
        {
            CreateDirectory(starpackOutputDir);
        }

        var processSettings = new ProcessSettings
        {
            EnvironmentVariables = GetEnvironmentVariables(),
            WorkingDirectory = rootPath
        };

        // UniformDocs
        processSettings.Arguments = new ProcessArgumentBuilder()
            .Append("/c starpack.exe")
            .Append($"--pack \"{rootPath}/src/UniformDocs/UniformDocs.csproj\"")
            .Append($"--config={configuration}")
            .Append($"--output=\"{starpackOutputDir.FullPath}\"");

        int exitCode = StartProcess(cliShell, processSettings);

        if(exitCode != 0)
        {
            throw new Exception(string.Format("Error while packaging: {0} {1}", cliShell, processSettings.Arguments.Render()));
        }
    });

    ///
    /// Utilities
    ///
    void RunAllTests()
    {
        var uiTestsConfig = rootPath + "/test/BifrostTestConfig/UiTests.json";
        string cliShell = "cmd";
        string nunitConsoleRunnerPath = $"{rootPath}/packages/nunit.consolerunner/3.10.0/tools/nunit3-console.exe";

        // Get all test files.
        var allUiTestsJson = System.IO.File.ReadAllText(uiTestsConfig);
        var testInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(allUiTestsJson);
        var allTests = ((IEnumerable<dynamic>)testInfo.Tests)
                            .Where(t => t.Enabled == true)
                            .Select(t => rootPath + t.TestFile.ToString().Replace("<UniformDocsConfiguration>", configuration));

        // Run test one by one.
        foreach(var test in allTests)
        {
            var processSettings = new ProcessSettings
            {
                EnvironmentVariables = GetEnvironmentVariables(),
                WorkingDirectory = rootPath
            };

            processSettings.Arguments = new ProcessArgumentBuilder()
            .Append($"/c {nunitConsoleRunnerPath}")
            .Append($"{test}")
            .Append($"--noheader");

            int exitCode = StartProcess(cliShell, processSettings);

            Information($"Exit Code: {exitCode}");

            if(exitCode != 0 && exitCode != -5)
            {
                throw new Exception($"Error while running: {test}");
            }
        }

    }

    void RunAllHelperApps()
    {
        var helperAppsConfig = rootPath + "/test/BifrostTestConfig/UiTestHelperApps.json";
        var cliShell = "cmd";

        // Get all the helper app to be run.
        var allHelperAppsJson = System.IO.File.ReadAllText(helperAppsConfig);
        var appInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(allHelperAppsJson);
        var allApps = ((IEnumerable<dynamic>)appInfo.Apps)
                            .Where(a => a.Enabled == true)
                            .Select(a => new 
                                { 
                                    ResourceDirectory = rootPath + a.ResourceDirectory.ToString(), 
                                    ApplicationPath = rootPath + a.ApplicationPath.ToString().Replace("<UniformDocsConfiguration>", configuration),
                                    Arguments = a.Arguments
                                });

        // Run all apps one by one.
        foreach(var app in allApps)
        {
            var processSettings = new ProcessSettings
            {
                EnvironmentVariables = GetEnvironmentVariables(),
                WorkingDirectory = rootPath
            };

            // Start the app
            processSettings.Arguments = new ProcessArgumentBuilder()
                .Append("/c star.exe")
                .Append($"--resourcedir=\"{app.ResourceDirectory}\"")
                .Append($"{app.ApplicationPath}")
                .Append($"{app.Arguments}");

            int exitCode = StartProcess(cliShell, processSettings);

            if(exitCode != 0)
            {
                throw new Exception($"Error while running: {app.ApplicationPath}");
            }
        }

    }
    void PathExists(string path, string name)
    {
        var message = string.Empty;
        // get the file attributes for file or directory
        var isDirectory = System.IO.File.GetAttributes(path).HasFlag(FileAttributes.Directory);
        if(isDirectory && !DirectoryExists(path))
        {
          message = "Directory";
        } 
        else if (!isDirectory && !FileExists(path))
        {
            message = "File";
        }

        if(!string.IsNullOrEmpty(message))
        {
            throw new CakeException($"{message} {name} does not exist at : {path}");
        }
    }

    Dictionary<string, string> GetEnvironmentVariables()
    {
        if (!IsRunningOnWindows())
        {
            throw new Exception("Only Windows platform is supported.");
        }

        /*
        if(string.IsNullOrEmpty(EnvironmentVariable("BROWSERSTACK_USERNAME")) && string.IsNullOrEmpty(browserStackApiUserEnvVar))
        {
            throw new Exception("browserStackApiUserEnvVar cannot be empty. Please provide --browserStackApiUserEnvVar parameter.");
        }

        if(string.IsNullOrEmpty(EnvironmentVariable("BROWSERSTACK_ACCESS_KEY")) && string.IsNullOrEmpty(browserStackApiPasswordEnvVar))
        {
            throw new Exception("browserStackApiPasswordEnvVar cannot be empty. Please provide --browserStackApiPasswordEnvVar parameter.");
        }
        */

        Dictionary<string, string> envVars = new Dictionary<string, string>();

        envVars.Add("config", configuration);
        envVars.Add("configuration", configuration);
        var token = EnvironmentVariable(browserStackApiPasswordEnvVar);
        var user = EnvironmentVariable(browserStackApiUserEnvVar);

        if(!string.IsNullOrEmpty(token))
        {
            envVars.Add("BROWSERSTACK_ACCESS_KEY", token);
        }
        if(!string.IsNullOrEmpty(user))
        {
            envVars.Add("BROWSERSTACK_USERNAME", user);
        }

        if (!string.IsNullOrEmpty(starcounterBin))
        {
            string absoluteScBin = MakeAbsolute(Directory(starcounterBin)).FullPath;
            envVars.Add("StarcounterBin", absoluteScBin);
            envVars.Add("Path", $"{absoluteScBin};{absoluteScBin}/StarDump;{absoluteScBin}/StarPack;{Environment.GetEnvironmentVariable("Path")}");
            envVars.Add("ReferencePath", $"{absoluteScBin};{Environment.GetEnvironmentVariable("ReferencePath")}");
        }

        return envVars;
    }

    ///
    /// Run targets if invoked as self-containment script
    ///
    if (!Tasks.Any(t => t.Name.Equals("Bifrost")))
    {
        // Read targets arguments
        IEnumerable<string> targetsArg = Argument("targets", string.Format("Pack{0}", targetSubName)).Split(new Char[]{',', ' '}).Where(s => !string.IsNullOrEmpty(s));

        // Self-containment dependent targets
        Task("Restore").IsDependentOn(string.Format("Restore{0}", targetSubName));
        Task("Build").IsDependentOn(string.Format("Build{0}", targetSubName));
        Task("Run").IsDependentOn(string.Format("Run{0}", targetSubName));
        Task("Test").IsDependentOn(string.Format("Test{0}", targetSubName));
        Task("Pack").IsDependentOn(string.Format("Pack{0}", targetSubName));

        // Run target
        foreach (string t in targetsArg)
        {
            RunTarget(t);
        }
    }
}