using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Context;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Pastikan relasi antara Transaction dan Location sudah benar
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Location)
            .WithMany(l => l.Transactions)
            .HasForeignKey(t => t.location_id)
            .OnDelete(DeleteBehavior.Cascade);  // Jika Anda menginginkan pengaturan penghapusan yang lebih baik
    }
}