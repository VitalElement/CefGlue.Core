/////////////////////////////////////////////////////////////////////
// ADDINS
/////////////////////////////////////////////////////////////////////

#addin "nuget:?package=Polly&version=5.0.6"
#addin "nuget:?package=NuGet.Core&version=2.14.0"
#addin "nuget:?package=SharpZipLib&version=0.86.0"
#addin "nuget:?package=Cake.Compression&version=0.2.1"

//////////////////////////////////////////////////////////////////////
// TOOLS
//////////////////////////////////////////////////////////////////////

#tool "nuget:https://dotnet.myget.org/F/nuget-build/?package=NuGet.CommandLine&version=4.3.0-beta1-2361&prerelease"

///////////////////////////////////////////////////////////////////////////////
// USINGS
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using Polly;
using NuGet;

///////////////////////////////////////////////////////////////////////////////
// SCRIPTS
///////////////////////////////////////////////////////////////////////////////

#load "./fileDownload.cake"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var platform = Argument("platform", "AnyCPU");
var configuration = Argument("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// CONFIGURATION
///////////////////////////////////////////////////////////////////////////////

var MainRepo = "VitalElement/CefGlue.Core";
var MasterBranch = "master";
var ReleasePlatform = "Any CPU";
var ReleaseConfiguration = "Release";

///////////////////////////////////////////////////////////////////////////////
// PARAMETERS
///////////////////////////////////////////////////////////////////////////////

var isPlatformAnyCPU = StringComparer.OrdinalIgnoreCase.Equals(platform, "Any CPU");
var isPlatformX86 = StringComparer.OrdinalIgnoreCase.Equals(platform, "x86");
var isPlatformX64 = StringComparer.OrdinalIgnoreCase.Equals(platform, "x64");
var isLocalBuild = BuildSystem.IsLocalBuild;
var isRunningOnUnix = IsRunningOnUnix();
var isRunningOnWindows = IsRunningOnWindows();
var isRunningOnAppVeyor = BuildSystem.AppVeyor.IsRunningOnAppVeyor;
var isPullRequest = BuildSystem.AppVeyor.Environment.PullRequest.IsPullRequest;
var isMainRepo = StringComparer.OrdinalIgnoreCase.Equals(MainRepo, BuildSystem.AppVeyor.Environment.Repository.Name);
var isMasterBranch = StringComparer.OrdinalIgnoreCase.Equals(MasterBranch, BuildSystem.AppVeyor.Environment.Repository.Branch);
var isTagged = BuildSystem.AppVeyor.Environment.Repository.Tag.IsTag 
               && !string.IsNullOrWhiteSpace(BuildSystem.AppVeyor.Environment.Repository.Tag.Name);
var isReleasable = StringComparer.OrdinalIgnoreCase.Equals(ReleasePlatform, platform) 
                   && StringComparer.OrdinalIgnoreCase.Equals(ReleaseConfiguration, configuration);
var isMyGetRelease = !isTagged && isReleasable;
var isNuGetRelease = isTagged && isReleasable;

var editbin = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\VC\Tools\MSVC\14.14.26428\bin\HostX86\x86\editbin.exe";

///////////////////////////////////////////////////////////////////////////////
// VERSION
///////////////////////////////////////////////////////////////////////////////

var version = "0.9.0";

if (isRunningOnAppVeyor)
{
    if (isTagged)
    {
        // Use Tag Name as version
        version = BuildSystem.AppVeyor.Environment.Repository.Tag.Name;
    }
    else
    {
        // Use AssemblyVersion with Build as version
        version += "-build" + EnvironmentVariable("APPVEYOR_BUILD_NUMBER") + "-alpha";
    }
}

///////////////////////////////////////////////////////////////////////////////
// DIRECTORIES
///////////////////////////////////////////////////////////////////////////////

var artifactsDir = (DirectoryPath)Directory("./artifacts");
var zipRootDir = artifactsDir.Combine("zip");
var nugetRoot = artifactsDir.Combine("nuget");
var fileZipSuffix = ".zip";

var buildDirs = 
    GetDirectories("./**/bin/") + 
    GetDirectories("./**/obj/");


var netCoreAppsRoot= ".";
var netCoreApps = new string[] { "CefGlue.Samples.Avalonia" };
var netCoreProjects = netCoreApps.Select(name => 
    new {
        Path = string.Format("{0}/{1}", netCoreAppsRoot, name),
        Name = name,
        Framework = XmlPeek(string.Format("{0}/{1}/{1}.csproj", netCoreAppsRoot, name), "//*[local-name()='TargetFramework']/text()"),
        Runtimes = XmlPeek(string.Format("{0}/{1}/{1}.csproj", netCoreAppsRoot, name), "//*[local-name()='RuntimeIdentifiers']/text()").Split(';')        
    }).ToList();

///////////////////////////////////////////////////////////////////////////////
// NUGET NUSPECS
///////////////////////////////////////////////////////////////////////////////

// Key: Package Id
// Value is Tuple where Item1: Package Version, Item2: The *.csproj/*.props file path.
var packageVersions = new Dictionary<string, IList<Tuple<string,string>>>();

System.IO.Directory.EnumerateFiles(((DirectoryPath)Directory(".")).FullPath, "*.csproj", SearchOption.AllDirectories)
    .ToList()
    .ForEach(fileName => {
    var xdoc = XDocument.Load(fileName);
    foreach (var reference in xdoc.Descendants().Where(x => x.Name.LocalName == "PackageReference"))
    {
        var name = reference.Attribute("Include").Value;
        var versionAttribute = reference.Attribute("Version");
        var packageVersion = versionAttribute != null 
            ? versionAttribute.Value 
            : reference.Elements().First(x=>x.Name.LocalName == "Version").Value;
        IList<Tuple<string, string>> versions;
        packageVersions.TryGetValue(name, out versions);
        if (versions == null)
        {
            versions = new List<Tuple<string, string>>();
            packageVersions[name] = versions;
        }
        versions.Add(Tuple.Create(packageVersion, fileName));
    }
});

Information("Checking installed NuGet package dependencies versions:");

packageVersions.ToList().ForEach(package =>
{
    var packageVersion = package.Value.First().Item1;
    bool isValidVersion = package.Value.All(x => x.Item1 == packageVersion);
    if (!isValidVersion)
    {
        Information("Error: package {0} has multiple versions installed:", package.Key);
        foreach (var v in package.Value)
        {
            Information("{0}, file: {1}", v.Item1, v.Item2);
        }
        throw new Exception("Detected multiple NuGet package version installed for different projects.");
    }
});

Information("Setting NuGet package dependencies versions:");

var AvaloniaVersion = packageVersions["Avalonia"].FirstOrDefault().Item1;

Information("Package: Avalonia, version: {0}", AvaloniaVersion);

var nuspecNuGetBehaviors = new NuGetPackSettings()
{
    Id = "VitalElement.CefGlue.Core",
    Version = version,
    Authors = new [] { "VitalElement" },
    Owners = new [] { "Dan Walmsley (dan at walms.co.uk)" },
    LicenseUrl = new Uri("http://opensource.org/licenses/MIT"),
    ProjectUrl = new Uri("https://github.com/VitalElement/CefGlue.Core/"),
    RequireLicenseAcceptance = false,
    Symbols = false,
    NoPackageAnalysis = true,
    Description = "Port of CEFGlue to NetStandard1.6.",
    Copyright = "Copyright 2017",
    Tags = new [] { "Avalonia", "CEF", "CEFGlue", "Core", "Dotnet", "Browser", "Control" },    
    Files = new []
    {
        new NuSpecContent { Source = "CefGlue/bin/" + configuration + "/netstandard1.6/CefGlue.dll", Target = "lib/netstandard1.6" },
    },
    BasePath = Directory("./"),
    OutputDirectory = nugetRoot
};

var nuspecNuGetSettings = new List<NuGetPackSettings>();

nuspecNuGetSettings.Add(nuspecNuGetBehaviors);

nuspecNuGetBehaviors = new NuGetPackSettings()
{
    Id = "VitalElement.CefGlue.Core.Avalonia",
    Version = version,
    Authors = new [] { "VitalElement" },
    Owners = new [] { "Dan Walmsley (dan at walms.co.uk)" },
    LicenseUrl = new Uri("http://opensource.org/licenses/MIT"),
    ProjectUrl = new Uri("https://github.com/VitalElement/CefGlue.Core/"),
    RequireLicenseAcceptance = false,
    Symbols = false,
    NoPackageAnalysis = true,
    Description = "CEF Glue support for Avalonia",
    Copyright = "Copyright 2017",
    Tags = new [] { "Avalonia", "CEF", "CEFGlue", "Core", "Dotnet", "Browser", "Control" },
    Dependencies = new []
    {
        new NuSpecDependency { Id = "Avalonia", Version = AvaloniaVersion }
    },
    Files = new []
    {
        new NuSpecContent { Source = "CefGlue.Avalonia/bin/" + configuration + "/netstandard2.0/CefGlue.Avalonia.dll", Target = "lib/netstandard2.0" },
    },
    BasePath = Directory("./"),
    OutputDirectory = nugetRoot
};

nuspecNuGetSettings.Add(nuspecNuGetBehaviors);

nuspecNuGetBehaviors = new NuGetPackSettings()
{
    Id = "VitalElement.CefGlue.Core.Win-x64",
    Version = version,
    Authors = new [] { "VitalElement" },
    Owners = new [] { "Dan Walmsley (dan at walms.co.uk)" },
    LicenseUrl = new Uri("http://opensource.org/licenses/MIT"),
    ProjectUrl = new Uri("https://github.com/VitalElement/CefGlue.Core/"),
    RequireLicenseAcceptance = false,
    Symbols = false,
    NoPackageAnalysis = true,
    Description = "CEF Glue support for Avalonia",
    Copyright = "Copyright 2017",
    Tags = new [] { "Avalonia", "CEF", "CEFGlue", "Core", "Dotnet", "Browser", "Control" },
    Files = new []
    {
        new NuSpecContent { Source = "**", Target = "runtimes/win7-x64/native" },
        new NuSpecContent { Source = "VitalElement.CefGlue.NativeSupport.targets", Target = "build/net45/VitalElement.CefGlue.Core.Win-x64.targets" }
    },
    BasePath = Directory("./artifacts/win-x64"),
    OutputDirectory = nugetRoot
};

nuspecNuGetSettings.Add(nuspecNuGetBehaviors);

nuspecNuGetBehaviors = new NuGetPackSettings()
{
    Id = "VitalElement.CefGlue.Core.OSX",
    Version = version,
    Authors = new [] { "VitalElement" },
    Owners = new [] { "Dan Walmsley (dan at walms.co.uk)" },
    LicenseUrl = new Uri("http://opensource.org/licenses/MIT"),
    ProjectUrl = new Uri("https://github.com/VitalElement/CefGlue.Core/"),
    RequireLicenseAcceptance = false,
    Symbols = false,
    NoPackageAnalysis = true,
    Description = "CEF Glue support for Avalonia",
    Copyright = "Copyright 2017",
    Tags = new [] { "Avalonia", "CEF", "CEFGlue", "Core", "Dotnet", "Browser", "Control" },
    Files = new []
    {
        new NuSpecContent { Source = "**", Target = "runtimes/osx/native" },
        new NuSpecContent { Source = "VitalElement.CefGlue.NativeSupport.targets", Target = "build/net45/VitalElement.CefGlue.Core.OSX.targets" }
    },
    BasePath = Directory("./artifacts/osx"),
    OutputDirectory = nugetRoot
};

nuspecNuGetSettings.Add(nuspecNuGetBehaviors);


var nugetPackages = nuspecNuGetSettings.Select(nuspec => {
    return nuspec.OutputDirectory.CombineWithFilePath(string.Concat(nuspec.Id, ".", nuspec.Version, ".nupkg"));
}).ToArray();

///////////////////////////////////////////////////////////////////////////////
// 3rd Party Downloads
///////////////////////////////////////////////////////////////////////////////

var toolchainDownloads = new List<ToolchainDownloadInfo> 
{ 
    new ToolchainDownloadInfo (artifactsDir)
    { 
        RID = "win-x64", 
        Downloads = new List<ArchiveDownloadInfo>()
        { 
            new ArchiveDownloadInfo()
            { 
                Format = "tar.bz2", 
                DestinationFile = "wincef.tar.bz2", 
                URL =  "http://opensource.spotify.com/cefbuilds/cef_binary_3.2987.1590.g1f1b268_windows64_client.tar.bz2",
                Name = "cef_binary_3.2987.1590.g1f1b268_windows64_client",
                PostExtract = (curDir, info) =>{
                    Utils.MoveFolderContents(curDir.Combine(info.Name).Combine("Release").ToString(), curDir.ToString());
                    DeleteDirectory(curDir.Combine(info.Name), true);

                    CopyFile ("./VitalElement.CefGlue.NativeSupport.targets", curDir.CombineWithFilePath("VitalElement.CefGlue.NativeSupport.targets"));              
                }
            }
        }
    },
    new ToolchainDownloadInfo (artifactsDir)
    { 
        RID = "osx", 
        Downloads = new List<ArchiveDownloadInfo>()
        { 
            new ArchiveDownloadInfo()
            { 
                Format = "tar.bz2", 
                DestinationFile = "osxcef.tar.bz2", 
                URL =  "http://opensource.spotify.com/cefbuilds/cef_binary_3.2987.1590.g1f1b268_macosx64_client.tar.bz2",
                Name = "cef_binary_3.2987.1590.g1f1b268_macosx64_client",
                PostExtract = (curDir, info) =>{
                    Utils.MoveFolderContents(curDir.Combine(info.Name).Combine("Release/cefclient.app/Contents/Frameworks").ToString(), curDir.ToString());
                    DeleteDirectory(curDir.Combine(info.Name), true);
                    DeleteDirectory(curDir.Combine("cefclient Helper.app"), true);      

                    CopyFile ("./VitalElement.CefGlue.NativeSupport.targets", curDir.CombineWithFilePath("VitalElement.CefGlue.NativeSupport.targets"));              
                }
            }
        }
    },
};

///////////////////////////////////////////////////////////////////////////////
// INFORMATION
///////////////////////////////////////////////////////////////////////////////

Information("Building version {0} of CefGlue.Core ({1}, {2}, {3}) using version {4} of Cake.", 
    version,
    platform,
    configuration,
    target,
    typeof(ICakeContext).Assembly.GetName().Version.ToString());

if (isRunningOnAppVeyor)
{
    Information("Repository Name: " + BuildSystem.AppVeyor.Environment.Repository.Name);
    Information("Repository Branch: " + BuildSystem.AppVeyor.Environment.Repository.Branch);
}

Information("Target: " + target);
Information("Platform: " + platform);
Information("Configuration: " + configuration);
Information("IsLocalBuild: " + isLocalBuild);
Information("IsRunningOnUnix: " + isRunningOnUnix);
Information("IsRunningOnWindows: " + isRunningOnWindows);
Information("IsRunningOnAppVeyor: " + isRunningOnAppVeyor);
Information("IsPullRequest: " + isPullRequest);
Information("IsMainRepo: " + isMainRepo);
Information("IsMasterBranch: " + isMasterBranch);
Information("IsTagged: " + isTagged);
Information("IsReleasable: " + isReleasable);
Information("IsMyGetRelease: " + isMyGetRelease);
Information("IsNuGetRelease: " + isNuGetRelease);

///////////////////////////////////////////////////////////////////////////////
// TASKS
/////////////////////////////////////////////////////////////////////////////// 

Task("Clean")
.Does(()=>{    
    CleanDirectories(buildDirs);
    CleanDirectory(artifactsDir);
    CleanDirectory(nugetRoot);

    foreach(var tc in toolchainDownloads)
    {
        CleanDirectory(tc.BaseDir);   
        CleanDirectory(tc.ZipDir);
    }
});

Task("Download")
.Does(()=>{
    foreach(var tc in toolchainDownloads)
    {
        foreach(var downloadInfo in tc.Downloads)
        {
            var fileName = tc.ZipDir.CombineWithFilePath(downloadInfo.DestinationFile);

            if(!FileExists(fileName))
            {
                DownloadFile(downloadInfo.URL, fileName);
            }
        }
    }
});

Task("Extract")
.Does(()=>{
    foreach(var tc in toolchainDownloads)
    {
        foreach(var downloadInfo in tc.Downloads)
        {
            var fileName = tc.ZipDir.CombineWithFilePath(downloadInfo.DestinationFile);
            var dest = tc.BaseDir;

            switch (downloadInfo.Format)
            {
                case "tar.bz2":
                BZip2Uncompress(fileName, dest);
                break;

                case "zip":
                ZipUncompress(fileName, dest);
                break;

                case "none":
                break;

                default:
                case "tar.xz":
                StartProcess("7z", new ProcessSettings{ Arguments = string.Format("x {0} -o{1}", fileName, dest) });
                break;
            }        

            if(downloadInfo.PostExtract != null)
            {
                downloadInfo.PostExtract(dest, downloadInfo);
            }
        }
    }
});

Task("Restore-NetCore")
    .IsDependentOn("Clean")
    .Does(() =>
{
    foreach (var project in netCoreProjects)
    {
        DotNetCoreRestore(project.Path);
    }
});

Task("Build-NetCore")
    .IsDependentOn("Restore-NetCore")
    .Does(() =>
{
    foreach (var project in netCoreProjects)
    {
        Information("Building: {0}", project.Name);
        DotNetCoreBuild(project.Path, new DotNetCoreBuildSettings {
            Configuration = configuration
        });
    }
});

Task("Publish-NetCore")
    .IsDependentOn("Restore-NetCore")
    .WithCriteria(()=>isMainRepo && isMasterBranch)
    .Does(() =>
{
    foreach (var project in netCoreProjects)
    {
        foreach(var runtime in project.Runtimes)
        {
            var outputDir = zipRootDir.Combine(project.Name + "-" + runtime);

            Information("Publishing: {0}, runtime: {1}", project.Name, runtime);
            DotNetCorePublish(project.Path, new DotNetCorePublishSettings {
                Framework = project.Framework,
                Configuration = configuration,
                Runtime = runtime,
                OutputDirectory = outputDir.FullPath
            });

            /*if (IsRunningOnWindows() && (runtime == "win7-x86" || runtime == "win7-x64"))
            {
                Information("Patching executable subsystem for: {0}, runtime: {1}", project.Name, runtime);
                var targetExe = outputDir.CombineWithFilePath(project.Name + ".exe");
                var exitCodeWithArgument = StartProcess(editbin, new ProcessSettings { 
                    Arguments = "/subsystem:windows " + targetExe.FullPath
                });
                Information("The editbin command exit code: {0}", exitCodeWithArgument);
            }*/
        }
    }
});

Task("Zip-NetCore")
    .IsDependentOn("Publish-NetCore")
    .WithCriteria(()=>isMainRepo && isMasterBranch)
    .Does(() =>
{
    foreach (var project in netCoreProjects)
    {
        foreach(var runtime in project.Runtimes)
        {
            var outputDir = zipRootDir.Combine(project.Name + "-" + runtime);

            Zip(outputDir.FullPath, zipRootDir.CombineWithFilePath(project.Name + "-" + runtime + fileZipSuffix), 
                GetFiles(outputDir.FullPath + "/*.*"));
        }
    }    
});


Task("Generate-NuGetPackages")
.IsDependentOn("Build-NetCore")
.IsDependentOn("Publish-NetCore")
.IsDependentOn("Zip-NetCore")
.IsDependentOn("Download")
.IsDependentOn("Extract")
.Does(()=>{
    foreach(var nuspec in nuspecNuGetSettings)
    {
        NuGetPack(nuspec);
    }
});

Task("Publish-AppVeyorNuget")
    .IsDependentOn("Generate-NuGetPackages")        
    .WithCriteria(() => isMainRepo)
    .WithCriteria(() => isMasterBranch)    
    .Does(() =>
{
    var apiKey = EnvironmentVariable("MYGET_API_KEY");
    if(string.IsNullOrEmpty(apiKey)) 
    {
        throw new InvalidOperationException("Could not resolve MyGet API key.");
    }

    var apiUrl = EnvironmentVariable("MYGET_API_URL");
    if(string.IsNullOrEmpty(apiUrl)) 
    {
        throw new InvalidOperationException("Could not resolve MyGet API url.");
    }

    foreach(var nupkg in nugetPackages)
    {
        NuGetPush(nupkg, new NuGetPushSettings {
            Source = apiUrl,
            ApiKey = apiKey
        });
    }
});

Task("Default")    
    .IsDependentOn("Publish-AppVeyorNuget");
    
RunTarget(target);
