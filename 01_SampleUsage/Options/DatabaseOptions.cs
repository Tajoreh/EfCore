using Microsoft.Extensions.Options;

namespace _01_SampleUsage.Options;

public class DatabaseOptions
{
    public string ConnectionStrings { get; set; } = String.Empty;
    public int MaxRetryCount { get; set; }
    public int CommandTimeout { get; set; }
    public bool EnableDetailedErrors { get; set; }
    public bool EnableSensitiveDataLogging { get; set; }
}

public class DatabaseOptionSetup : IConfigureOptions<DatabaseOptions>
{
    readonly IConfiguration _configuration;

    public DatabaseOptionSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(DatabaseOptions options)
    {
        var connectionString = _configuration.GetConnectionString("Database");

        options.ConnectionStrings = connectionString;

        _configuration.GetSection(nameof(DatabaseOptions)).Bind(options);
    }
}