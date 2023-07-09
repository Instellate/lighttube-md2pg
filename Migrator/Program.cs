using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Migrator;
using Migrator.Database;

ConfigurationBuilder builder = new();
#if DEBUG
builder.AddUserSecrets<Program>();
#else
builder.AddEnvironmentVariables();
#endif
IConfiguration config = builder.Build();

ServiceCollection collection = new();
collection.AddDbContext<PrimaryDb>((o) => o.UseNpgsql(config["CONN_STR"]!));

IServiceProvider provider = collection.BuildServiceProvider();

using (IServiceScope scope = provider.CreateScope())
{
    MongoClient client = new(new MongoUrl(config["MONGO_CONN_STR"]!));
    PrimaryDb db = provider.GetRequiredService<PrimaryDb>();
    await db.Database.MigrateAsync();

    await Migrate.StartMigration(db, client.GetDatabase("Lighttube"));
}