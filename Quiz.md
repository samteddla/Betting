# Create db first MVC application generating for the whole db.
add the necessary tools if not there
```bash
dotnet tool update --global dotnet-ef 
dotnet tool update -g Dotnet-aspnet-codegenerator 
```
#create web Application project with Controller and views
```bash
echo "Web application should be created as : "
echo "dotnet new mvc -o BetBackup"
echo "cd BetBackup"
```
## add refrences 
```bash
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.SqlServer.Design
```
The ps1 file:

```bash
$DB_CONNECTION_STRING = "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BetBackup;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
$DB_CONTEXT = "BetContext"
$DB_CONTEXT_FOLDER = "Context"
$MODEL_FOLDER = "Model"

# Scaffold DbContext and models
dotnet ef dbcontext scaffold $DB_CONNECTION_STRING Microsoft.EntityFrameworkCore.SqlServer -o $MODEL_FOLDER -c $DB_CONTEXT --context-dir $DB_CONTEXT_FOLDER -f --force

dotnet ef migrations add InitialCreate   

# Read the DbContext file and extract DbSet properties
$DB_CONTEXT_FILE = "$DB_CONTEXT_FOLDER\$DB_CONTEXT.cs"
$MODELS = Get-Content $DB_CONTEXT_FILE | Select-String "public virtual DbSet<(.+)> .+ {" | ForEach-Object { $_.Matches.Groups[1].Value }

if($MODELS.Count -eq 0) {
  echo "No DbSet properties found in $DB_CONTEXT"
  exit
}

# Generate controllers for each model with CRUD actions
foreach ($MODEL in $MODELS) {
  $CONTROLLER_NAME = "${MODEL}Controller"
  echo "Generating controller $CONTROLLER_NAME for model $MODEL"
  dotnet aspnet-codegenerator controller -name $CONTROLLER_NAME -m $MODEL -dc $DB_CONTEXT --relativeFolderPath Controllers --useDefaultLayout
}

# Generate views for each controller with CRUD actions
#foreach ($MODEL in $MODELS) {
#  $CONTROLLER_NAME = "${MODEL}Controller"
  
## Generate views for CRUD actions
#  $CRUD_ACTIONS = @("Index", "Create", "Edit", "Details", "Delete")
#  foreach ($ACTION in $CRUD_ACTIONS) {
#    dotnet aspnet-codegenerator view -name $ACTION -m $MODEL -dc $DB_CONTEXT --relativeFolderPath "Views\$CONTROLLER_NAME" --useDefaultLayout
#  }
#}
```
#### update dependency injection for db context
services.AddDbContext<BetContext>(options => options.UseSqlServer());


### Generate typescript interfaces from C# api
```project file
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <UserSecretsId>41758948-78eb-45df-9e32-fe16f526cbbb</UserSecretsId>
    <DockerDefaultTargetOS>Linux</DockerDefaultTargetOS>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="7.0.13" />
    <PackageReference Include="Microsoft.VisualStudio.Azure.Containers.Tools.Targets" Version="1.19.6" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
    <PackageReference Include="Serilog.AspNetCore" Version="8.0.1" />     
    <PackageReference Include="Serilog.Sinks.Graylog" Version="3.1.1" />
    <PackageReference Include="Mapster" Version="7.4.0" />
    <PackageReference Include="Mapster.DependencyInjection" Version="1.0.1" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\SportBet.Application\SportBet.Application.csproj" />
    <ProjectReference Include="..\SportBet.Contracts\SportBet.Contracts.csproj" />
  </ItemGroup>

 <ItemGroup>
		<PackageReference Include="NSwag.MSBuild" Version="13.20.0">
			<PrivateAssets>all</PrivateAssets>
			<IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
		</PackageReference>
		<PackageReference Include="Swashbuckle.AspNetCore.Swagger" Version="6.5.0" />
		<PackageReference Include="Swashbuckle.AspNetCore.SwaggerGen" Version="6.5.0" />
	</ItemGroup>


	<ItemGroup>
		<None Update="nswag.json">
			<CopyToOutputDirectory>Always</CopyToOutputDirectory>
		</None>
    <None Update="api.yaml">
			<CopyToOutputDirectory>Always</CopyToOutputDirectory>
		</None>
	</ItemGroup>

<PropertyGroup>
  <RunPostBuildEvent>OnBuildSuccess</RunPostBuildEvent>
</PropertyGroup>

	<Target Name="NSwag" AfterTargets="Build">
		<Exec ConsoleToMSBuild="true" ContinueOnError="true" Command="$(NSwagExe_Net70) run nswag.json /variables:Configuration=$(Configuration)">
			<Output TaskParameter="ExitCode" PropertyName="NSwagExitCode" />
			<Output TaskParameter="ConsoleOutput" PropertyName="NSwagOutput" />
		</Exec>
		<Message Text="$(NSwagOutput)" Condition="'$(NSwagExitCode)' == '0'" Importance="Low" />
		<Error Text="$(NSwagOutput)" Condition="'$(NSwagExitCode)' != '0'" />
	</Target>
	
	<Target Name="RunJavaJar" AfterTargets="NSwag">
      <Exec Command="Y:\Bin\jdk-21.0.2\bin\java.exe -jar Y:\bin\swagger-codegen-cli-3.0.52.jar generate -i .\api.yaml -l typescript-axios -o .\auto-generated\src\api --flatten-inline-schema true" />
  </Target>
</Project>
```
### nswag.json file
```json
{
    "runtime": "Net70",
    "defaultVariables": null,
    "documentGenerator": {
      "webApiToOpenApi": {
        "controllerNames": [],
        "isAspNetCore": true,
        "resolveJsonOptions": false,
        "defaultUrlTemplate": "api/{controller}/{id?}",
        "addMissingPathParameters": false,
        "includedVersions": null,
        "defaultPropertyNameHandling": "Default",
        "defaultReferenceTypeNullHandling": "Null",
        "defaultDictionaryValueReferenceTypeNullHandling": "NotNull",
        "defaultResponseReferenceTypeNullHandling": "NotNull",
        "defaultEnumHandling": "String",
        "flattenInheritanceHierarchy": false,
        "generateKnownTypes": true,
        "generateEnumMappingDescription": false,
        "generateXmlObjects": false,
        "generateAbstractProperties": false,
        "generateAbstractSchemas": true,
        "ignoreObsoleteProperties": false,
        "allowReferencesWithProperties": false,
        "excludedTypeNames": [],
        "serviceHost": null,
        "serviceBasePath": null,
        "serviceSchemes": [],
        "infoTitle": "PizzaStore Web Api",
        "infoDescription": null,
        "infoVersion": "1.0.0",
        "documentTemplate": null,
        "documentProcessorTypes": [],
        "operationProcessorTypes": [],
        "typeNameGeneratorType": null,
        "schemaNameGeneratorType": null,
        "contractResolverType": null,
        "serializerSettingsType": null,
        "useDocumentProvider": true,
        "documentName": "v1",
        "aspNetCoreEnvironment": null,
        "createWebHostBuilderMethod": null,
        "startupType": null,
        "allowNullableBodyParameters": true,
        "output": "./api.yaml",
        "outputType": "Swagger2",
        "assemblyPaths": [
          "../SportBet.Api/bin/$(Configuration)/net7.0/SportBet.Api.dll"
        ],
        "assemblyConfig": null,
        "referencePaths": [],
        "useNuGetCache": false
      }
    },
    "codeGenerators": {
     /* "openApiToTypeScriptClient": {
        "className": "{controller}Client",
        "moduleName": "",
        "namespace": "",
        "typeScriptVersion": 4.3,
        "template": "Axios",
        "promiseType": "Promise",
        "httpClass": "HttpClient",
        "withCredentials": false,
        "useSingletonProvider": false,
        "injectionTokenType": "OpaqueToken",
        "rxJsVersion": 6.0,
        "dateTimeType": "Date",
        "nullValue": "undefined",
        "generateClientClasses": true,
        "generateClientInterfaces": false,
        "generateOptionalParameters": false,
        "exportTypes": true,
        "wrapDtoExceptions": false,
        "exceptionClass": "ApiException",
        "clientBaseClass": null,
        "wrapResponses": false,
        "wrapResponseMethods": [],
        "generateResponseClasses": true,
        "responseClass": "SwaggerResponse",
        "protectedMethods": [],
        "configurationClass": null,
        "useTransformOptionsMethod": false,
        "useTransformResultMethod": false,
        "generateDtoTypes": true,
        "operationGenerationMode": "MultipleClientsFromOperationId",
        "markOptionalProperties": true,
        "generateCloneMethod": false,
        "typeStyle": "Interface",
        "enumStyle": "Enum",
        "useLeafType": false,
        "classTypes": [],
        "extendedClasses": [],
        "extensionCode": null,
        "generateDefaultValues": true,
        "excludedTypeNames": [],
        "excludedParameterNames": [],
        "handleReferences": false,
        "generateTypeCheckFunctions": false,
        "generateConstructorInterface": true,
        "convertConstructorInterfaceData": false,
        "importRequiredTypes": true,
        "useGetBaseUrlMethod": false,
        "baseUrlTokenName": "API_BASE_URL",
        "queryNullValue": "",
        "useAbortSignal": false,
        "inlineNamedDictionaries": false,
        "inlineNamedAny": false,
        "includeHttpContext": false,
        "templateDirectory": null,
        "serviceHost": null,
        "serviceSchemes": null,
        "output": "./auto-generated/SportBet.ts",
        "newLineBehavior": "Auto"
      }*/

      
    "openApiToTypeScriptClient": {
      "className": "{controller}Client",
      "moduleName": "",
      "namespace": "",
      "typeScriptVersion": 4.3,
      "template": "Axios",
      "promiseType": "Promise",
      "httpClass": "HttpClient",
      "withCredentials": false,
      "useSingletonProvider": false,
      "injectionTokenType": "OpaqueToken",
      "rxJsVersion": 6.0,
      "dateTimeType": "Date",
      "nullValue": "Null",
      "generateClientClasses": true,
      "generateClientInterfaces": false,
      "generateOptionalParameters": false,
      "exportTypes": true,
      "wrapDtoExceptions": false,
      "exceptionClass": "ApiException",
      "clientBaseClass": null,
      "wrapResponses": false,
      "wrapResponseMethods": [],
      "generateResponseClasses": true,
      "responseClass": "SwaggerResponse",
      "protectedMethods": [],
      "configurationClass": null,
      "useTransformOptionsMethod": false,
      "useTransformResultMethod": false,
      "generateDtoTypes": true,
      "operationGenerationMode": "MultipleClientsFromOperationId",
      "markOptionalProperties": true,
      "generateCloneMethod": false,
      "typeStyle": "Class",
      "enumStyle": "Enum",
      "useLeafType": true,
      "classTypes": [],
      "extendedClasses": [],
      "extensionCode": null,
      "generateDefaultValues": true,
      "excludedTypeNames": [],
      "excludedParameterNames": [],
      "handleReferences": false,
      "generateTypeCheckFunctions": true,
      "generateConstructorInterface": false,
      "convertConstructorInterfaceData": false,
      "importRequiredTypes": true,
      "useGetBaseUrlMethod": false,
      "baseUrlTokenName": "API_BASE_URL",
      "queryNullValue": "",
      "useAbortSignal": false,
      "inlineNamedDictionaries": false,
      "inlineNamedAny": false,
      "includeHttpContext": false,
      "templateDirectory": null,
      "serviceHost": null,
      "serviceSchemes": null,
      "output": "./auto-generated/api.ts",
      "newLineBehavior": "Auto"
    }
    }
  }
```