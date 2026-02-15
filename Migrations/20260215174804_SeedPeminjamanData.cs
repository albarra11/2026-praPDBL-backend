using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PinjamRuanganAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedPeminjamanData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Peminjaman",
                columns: new[] { "Id", "AlasanPeminjaman", "DeletedAt", "NamaPeminjam", "NamaRuangan", "NomorPeminjam", "Status", "Tanggal", "WaktuMulai", "WaktuSelesai" },
                values: new object[,]
                {
                    { 1, "Rapat UKM", null, "Budi", "A101", "081234567890", "Menunggu", new DateTime(2026, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0), new TimeSpan(0, 11, 0, 0, 0) },
                    { 2, "Kelas tambahan", null, "Siti", "B202", "089876543210", "Disetujui", new DateTime(2026, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 13, 0, 0, 0), new TimeSpan(0, 15, 0, 0, 0) },
                    { 3, "Seminar", null, "Andi", "A101", "087700112233", "Ditolak", new DateTime(2026, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Peminjaman",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Peminjaman",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Peminjaman",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
