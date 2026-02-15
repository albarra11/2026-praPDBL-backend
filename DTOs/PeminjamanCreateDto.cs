using System;
using System.ComponentModel.DataAnnotations;

namespace PinjamRuanganAPI.DTOs
{
    public class PeminjamanCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string NamaPeminjam { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string NomorPeminjam { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string AlasanPeminjaman { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string NamaRuangan { get; set; } = null!;

        [Required]
        public DateTime Tanggal { get; set; }

        [Required]
        public TimeSpan WaktuMulai { get; set; }

        [Required]
        public TimeSpan WaktuSelesai { get; set; }
    }
}