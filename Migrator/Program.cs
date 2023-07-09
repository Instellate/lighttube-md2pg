using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Migrator;
using Migrator.Database;

ServiceCollection collection = new();
collection.AddDbContext<PrimaryDb>((o) => o.UseNpgsql(""));

IServiceProvider provider = collection.BuildServiceProvider();


using (IServiceScope scope = provider.CreateScope())
{
    PrimaryDb db = provider.GetRequiredService<PrimaryDb>();
    await db.Database.MigrateAsync();
    await Migrate.StartMigration(db);
}