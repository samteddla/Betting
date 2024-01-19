using SportBet.Api;
using SportBet.Application;
using SportBet.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;


services
    .AddPresentation(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

// EnableCors
services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore.Api v1"));
    app.UseStatusCodePagesWithReExecute("/errors/{0}");
}

app.UseExceptionHandler("/errors");

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
