using _01_SampleUsage;
using _01_SampleUsage.Models;
using _01_SampleUsage.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

//Method 1: too many configurations hardCoded
//builder.Services.AddDbContext<DatabaseContext>(
//    dbContextOptionBuilder =>
//    {
//        var connectionString = builder.Configuration.GetConnectionString("database");

//        dbContextOptionBuilder.UseSqlServer(connectionString, sqlServerActions =>
//        {
//            sqlServerActions.EnableRetryOnFailure(3);
//            sqlServerActions.CommandTimeout(30);
//        });


//        //enable details
//        //in development env and local
//        dbContextOptionBuilder.EnableDetailedErrors(true);
//        dbContextOptionBuilder.EnableSensitiveDataLogging(true);
//    });

//Method 2: Add Configurations to appsetting.json
builder.Services.ConfigureOptions<DatabaseOptionSetup>();

builder.Services.AddDbContext<DatabaseContext>(
   (serviceProvider, dbContextOptionBuilder) =>
   {
       var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>().Value;

       dbContextOptionBuilder.UseSqlServer(databaseOptions.ConnectionStrings, sqlServerActions =>
       {
           sqlServerActions.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
           sqlServerActions.CommandTimeout(databaseOptions.CommandTimeout);
       });

       dbContextOptionBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
       dbContextOptionBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
   });
var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("companies/{companyId:int}", async (int companyId, DatabaseContext context) =>
{
    var company = await context
        .Set<Company>()
        .AsNoTracking()
        .FirstOrDefaultAsync(c => c.Id == companyId);

    if (company is null)
        return Results.NotFound($"The company with id '{companyId}' was not found.");

    var response = new CompanyResponse(company.Id, company.Name);

    return Results.Ok(response);
});

app.Run();
