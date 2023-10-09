using _03_Ef_Core_GlobalFiltering.Entities;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<DatabaseContext>(optionsBuilder => optionsBuilder.UseSqlServer(connectionString));

var app = builder.Build();

app.UseHttpsRedirection();
app.MapGet("/",async (DatabaseContext dbContext,CancellationToken cancellationToken)=>
{

    await dbContext
        .Set<Company>()
        //Ignores query filter for this specific query
        .IgnoreQueryFilters()
        .ToListAsync(cancellationToken);

    return Results.NoContent();
});

app.Run();
