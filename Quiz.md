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
