using _01_SampleUsage;
using _01_SampleUsage.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(
    dbContextOptionBuilder =>
    {
        var connectionString = builder.Configuration.GetConnectionString("database");

        dbContextOptionBuilder.UseSqlServer(connectionString);
    });

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("companies/{companyId:int}",async  (int companyId,DatabaseContext context) =>
{
    var company=await context
        .Set<Company>()
        .AsNoTracking()
        .FirstOrDefaultAsync(c=>c.Id == companyId);

    if (company is null)
        return Results.NotFound($"The company with id '{companyId}' was not found.");

    var response=new CompanyResponse(company.Id,company.Name);

    return Results.Ok(response);
});

app.Run();
