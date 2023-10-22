using _04_EfCore_Seed.Entities;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<DatabaseContext>(optionsBuilder => optionsBuilder.UseSqlServer(connectionString));

var app = builder.Build();

app.UseHttpsRedirection();
app.MapGet("/", async (DatabaseContext dbContext, CancellationToken cancellationToken) =>
{

    var companies=await dbContext
        .Set<Company>()
        .IgnoreQueryFilters()
        .ToListAsync(cancellationToken);

    return Results.Ok(companies);
});

app.Run();