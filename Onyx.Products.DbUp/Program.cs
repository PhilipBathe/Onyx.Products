using DbUp;
using DbUp.Engine;
using Microsoft.Extensions.Configuration;
using System.Reflection;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json");

var config  = configuration.Build();

string connectionString = config.GetConnectionString("ProductConnectionString")!;

UpgradeEngine upgrader =
    DeployChanges.To
        .SqlDatabase(connectionString)
        .JournalToSqlTable("dbo", "SchemaVersions")
        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
        .LogToConsole()
        .WithExecutionTimeout(TimeSpan.FromMinutes(10))
        .Build();

DatabaseUpgradeResult result = upgrader.PerformUpgrade();

if(result.Successful == false)
{
    Console.WriteLine(result.Error);
    return -1;
}

Console.WriteLine("Success");
return 0;