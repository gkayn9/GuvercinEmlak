using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Guvercin.Persistance.Context.Identity;

public class AppIdentityDbContext : IdentityDbContext<AppIdentityUser, AppIdentityRole, string>
{
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<AppIdentityUser>(entity =>
        {
            entity.ToTable("Users");
        });
        builder.Entity<AppIdentityRole>(entity =>
        {
            entity.ToTable("Roles");
        });
    }
    
}