using Microsoft.EntityFrameworkCore;
using PinjamRuanganAPI.Entities;

namespace PinjamRuanganAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        public DbSet<Peminjaman> Peminjaman {get; set;}
    }
}