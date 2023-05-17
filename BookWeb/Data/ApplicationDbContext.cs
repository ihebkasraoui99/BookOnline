using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ApplicationDbContext : DbContext
{
    public DbSet<Livre> Livres { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Author> Authors { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=.;User=YourUsername;Password=YourPassword");
        }
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // configure relationships
        modelBuilder.Entity<Livre>()
            .HasOne(l => l.Client)
            .WithMany(c => c.Livres)
            .HasForeignKey(l => l.ClientId);

        modelBuilder.Entity<Livre>()
            .HasOne(l => l.Author)
            .WithMany(a => a.Livres)
            .HasForeignKey(l => l.AuthorId);
    }
}
