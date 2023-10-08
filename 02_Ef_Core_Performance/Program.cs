using _02_Ef_Core_Performance.Entities;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<DatabaseContext>(
    optionsBuilder => optionsBuilder.UseSqlServer(connectionString,
        //Method 2 of global query splitting
        options => options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
);

var app = builder.Build();


app.UseHttpsRedirection();


//Method 1: it takes 1001 updates for each  employee and also company
app.MapPut("increase-salary/{companyId:int}", async (int companyId, DatabaseContext dbContext) =>
{
    var company = await dbContext
        .Set<Company>()
        //Method 1 of query splitting != SingleQuery
        .AsSplitQuery()
        .Include(x => x.Employees)
        .AsNoTracking()
        .FirstOrDefaultAsync(c => c.Id == companyId);

    if (company is null)
        return Results.NotFound($"The company with id '{companyId}' was not found.");

    foreach (var employee in company.Employees)
    {
        employee.Salary *= 1.1m;
    }

    company.LastSalaryUpdateUtc = DateTime.UtcNow;
    await dbContext.SaveChangesAsync();

    return Results.NoContent();
});


app.MapPut("increase-salary-sql", async (int companyId, DatabaseContext dbContext) =>
{
    var company = await dbContext
        .Set<Company>()
        .FirstOrDefaultAsync(c => c.Id == companyId);

    if (company is null)
        return Results.NotFound($"The company with id '{companyId}' was not found.");

    await dbContext.Database.BeginTransactionAsync();

    await dbContext.Database.ExecuteSqlInterpolatedAsync(
         $"UPDATE Employees SET Salary= Salary * 1.1 WHERE companyId={companyId}"
     );

    company.LastSalaryUpdateUtc = DateTime.UtcNow;

    await dbContext.SaveChangesAsync();

    await dbContext.Database.CommitTransactionAsync();

    return Results.NoContent();
});

app.MapPut("increase-salary-dapper", async (int companyId, DatabaseContext dbContext) =>
{
    var company = await dbContext
        .Set<Company>()
        .FirstOrDefaultAsync(c => c.Id == companyId);

    if (company is null)
        return Results.NotFound($"The company with id '{companyId}' was not found.");

    var transaction=await dbContext.Database.BeginTransactionAsync();

    await dbContext.Database.GetDbConnection().ExecuteAsync(
        $"UPDATE Employees SET Salary= Salary * 1.1 WHERE CompanyId=@CompanyId",
        new { CompanyId = company.Id },
        transaction.GetDbTransaction());

    company.LastSalaryUpdateUtc = DateTime.UtcNow;

    await dbContext.SaveChangesAsync();

    await dbContext.Database.CommitTransactionAsync();

    return Results.NoContent();
});

app.Run();

