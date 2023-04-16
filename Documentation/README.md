# Azure Function App with .NET7.0

This function app demonstrates configuration and runtime of an Azure Function using .NET7.0 with the following triggers

* Timer
* HTTP
* EventHub

To get started create a file  **./Src/Microservice/local.settings.json**

    {
        "IsEncrypted": false,
        "Values": {
            "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
            "ApplicationInsights__InstrumentationKey": "[Your App Insights Key Here]",
            "AzureWebJobsStorage": "[Storage Connection String Here]",
            "HttpHubReadWriteConnection": "[HTTP Hub Sender/Listener Connection String Here]",
            "AlphaHubWriteConnection": "[Alpha Hub Sender Connection String Here]"
        }
    }

## Repo Layout
1. The Solution file lives at the root (**/**) of the repo
2. The Project files live in the **/Src/** directory
    * ex. /Src/ProjectSample/ProjectSample.csproj
3. The Project files should build to the **/Bin/** directory
    * To accomplish this add the following code to the *.csproj in a **PropertyGroup**

            <OutputPath>../../bin/$(Configuration)</OutputPath>
4. The Documentation lives in the **/Documentation** directory
5. The Configuration as code lives in the **/Config** directory
6. The pipeline YAML file lives in the root (**/**) of the repo


## Best Practices

### C# Project Files
Use the following options to make builds faster and eliminate errors.

    <PropertyGroup>
        <TargetFramework>net7.0</TargetFramework>
        <WarningsAsErrors />
        <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
        <OutputPath>../../bin/$(Configuration)</OutputPath>
        <NoWarn>1701;1702;</NoWarn>        
    </PropertyGroup>

    <PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Debug|AnyCPU'">
        <DefineConstants>DEBUG;TRACE</DefineConstants>
    </PropertyGroup>

### Build Pipeline

#### Security Code Scan
A NuGet based OWASP scanner that will check for vulnerabilities every build. Include this in **All** Projects **Except Test** Projects


    <PackageReference Include="SecurityCodeScan.VS2019" Version="5.6.7" PrivateAssets="all" />
#### Cobertura
A NuGet based code coverage utility that will run as part of the build.  Include this in the Unit Test project.


    <PackageReference Include="coverlet.msbuild" Version="3.2.0" PrivateAssets="all" />

## Exception Tracking Strategy
### Exception Tracking
Make use of the ILoggerFactory and Dependency injection to log all exceptions.  Function Start points should be wrapped in a try{..}catch{throw} where the Logger is tracking the Exception.

      public class Microservice{
            private readonly _ILogger logger;
            public Microservice(ILoggerFactory loggerFactory){
              _logger = loggerFactory.CreateLogger<Microservice>();
            }
            public async Task RunAsync(...){

              try{
                ...
              }
              catch(Exception ex){
                _logger.LogError(ex, "An error occurred during execution of the microservice.{NewLine}{Error}", Environment.NewLine, ex.ToString())
                throw;
              }
            }
      }
## Logging
Use the same logger for logging information about the request

    _logger.LogInformation("A request came in with name {Name} and sequence {Sequence}", request.Name, request.Sequence);

## Sample Call
You can use PostMan to do a post as well by importing this curl command.

    curl --location --request POST 'https://func-demoapp-funchubdemo-dev-usc.azurewebsites.net/api/Demo'
