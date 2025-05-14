using Guvercin.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Guvercin.Persistance.Context;

public class AppDbContext: DbContext
{
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    public DbSet<AdvertItem> AdvertItems { get; set; }
    public DbSet<Category> Categories { get; set; }

    
   
}
