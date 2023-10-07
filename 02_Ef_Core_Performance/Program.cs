using _02_Ef_Core_Performance.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(
    o => o.UseSqlServer(builder.Configuration.GetConnectionString("Database"))
);

var app = builder.Build();


app.UseHttpsRedirection();

app.Run();

