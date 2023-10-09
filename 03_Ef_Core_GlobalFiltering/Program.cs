using _03_Ef_Core_GlobalFiltering.Entities;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<DatabaseContext>(
    optionsBuilder => optionsBuilder.UseSqlServer(connectionString,
        //Method 2 of global query splitting
        options => options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
);

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();
