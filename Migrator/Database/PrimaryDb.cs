using Migrator.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Migrator.Database;

public class PrimaryDb : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserSubscription> UserSubscriptions { get; set; } = null!;
    public DbSet<Login> Logins { get; set; } = null!;
}