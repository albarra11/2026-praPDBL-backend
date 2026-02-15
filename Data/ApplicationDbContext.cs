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
            modelBuilder.Entity<Peminjaman>(entity =>
            {
                entity.Property(p => p.Status)
                    .HasConversion<string>();

                entity.HasQueryFilter(p => p.DeletedAt == null);
                entity.HasData(
                    new Peminjaman
                    {
                        Id = 1,
                        NamaPeminjam = "Budi",
                        NomorPeminjam = "081234567890",
                        AlasanPeminjaman = "Rapat UKM",
                        NamaRuangan = "A101",
                        Tanggal = new DateTime(2026, 2, 15),
                        WaktuMulai = new TimeSpan(9, 0, 0),
                        WaktuSelesai = new TimeSpan(11, 0, 0),
                        Status = Enums.StatusPeminjaman.Menunggu,
                        DeletedAt = null
                    },
                    new Peminjaman
                    {
                        Id = 2,
                        NamaPeminjam = "Siti",
                        NomorPeminjam = "089876543210",
                        AlasanPeminjaman = "Kelas tambahan",
                        NamaRuangan = "B202",
                        Tanggal = new DateTime(2026, 2, 16),
                        WaktuMulai = new TimeSpan(13, 0, 0),
                        WaktuSelesai = new TimeSpan(15, 0, 0),
                        Status = Enums.StatusPeminjaman.Disetujui,
                        DeletedAt = null
                    },
                    new Peminjaman
                    {
                        Id = 3,
                        NamaPeminjam = "Andi",
                        NomorPeminjam = "087700112233",
                        AlasanPeminjaman = "Seminar",
                        NamaRuangan = "A101",
                        Tanggal = new DateTime(2026, 2, 17),
                        WaktuMulai = new TimeSpan(8, 0, 0),
                        WaktuSelesai = new TimeSpan(10, 0, 0),
                        Status = Enums.StatusPeminjaman.Ditolak,
                        DeletedAt = null
                    }
                );
            });
        }
    }
}