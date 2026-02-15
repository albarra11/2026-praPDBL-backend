using Microsoft.EntityFrameworkCore;
using PinjamRuanganAPI.Entities;

namespace PinjamRuanganAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        public DbSet<Peminjaman> Peminjaman {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<Peminjaman>()
                .Property(p => p.Status)
                .HasConversion<string>();
        }
    }
}