using SportBet.Infrastructure;
using Cocona;

using SportBet.Xstart;

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
var utils = new Utils();
app.AddCommand("do", () =>
{
    Console.WriteLine("Nothing to do");
});

app.AddSubCommand("database", x =>
{
    x.AddCommand("delete", () => dbContext.Database.EnsureDeleted());
    x.AddCommand("create", () => dbContext.Database.EnsureCreated());
    x.AddCommand("sample", () => utils.CreateFirstTimeData(dbContext));
}).WithDescription("database commands");;

app.AddCommand("add", () =>
{
    Console.WriteLine("Create the database if it does not exist");
    dbContext.Database.EnsureCreated();
});
app.AddCommand("delete", () =>
{
    Console.WriteLine("delete the database if it exists");
    dbContext.Database.EnsureDeleted();
});
app.AddCommand("create", () =>
{
    Console.WriteLine("initialize the database with test data");
    utils.CreateFirstTimeData(dbContext);
});


app.Run();
