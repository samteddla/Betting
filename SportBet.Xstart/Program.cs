using SportBet.Infrastructure;
using Cocona;

var builder = CoconaApp.CreateBuilder();
// dotnet run -- -h

var services = builder.Services;

services
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

// get Dbcontext 
var dbContext = app.Services.GetRequiredService<BetContext>();

/* Use this to replace the default command
app.AddCommand((
    [Option(Description = "Description of the option")] int value,
    [Argument(Description = "Description of the argument")]string arg
) => {
if (value == 0)
{
        dbContext.Database.EnsureDeleted();
}
else
{
    dbContext.Database.EnsureCreated();
}
});*/

app.AddCommand("add", () =>
{
    Console.WriteLine("Add command");
    dbContext.Database.EnsureCreated();
});
app.AddCommand("delete", () =>
{
    Console.WriteLine("Delete command");
    dbContext.Database.EnsureDeleted();
});
app.AddCommand("create", () =>
{
    Console.WriteLine("Create command");

    var utils = new Utils();
    utils.CreateFirstTimeData(dbContext);
});

app.AddCommand("do", () =>
{
    Console.WriteLine("Do command");
});
app.Run();
