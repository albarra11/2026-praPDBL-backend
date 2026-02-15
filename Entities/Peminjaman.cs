using System;
using System.ComponentModel.DataAnnotations;
using PinjamRuanganAPI.Enums;

namespace PinjamRuanganAPI.Entities
{
    public class Peminjaman
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string NamaPeminjam { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string NomorPeminjam {get; set;} = null!;

        [Required]
        [MaxLength(255)]
        public string AlasanPeminjaman { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string NamaRuangan {get; set;} = null!;

        [Required]
        public DateTime Tanggal {get; set;} 

        [Required]
        public TimeSpan WaktuMulai {get; set;}

        [Required]
        public TimeSpan WaktuSelesai {get; set;}

        [Required]
        public StatusPeminjaman Status {get; set;} = StatusPeminjaman.Menunggu;
    }
}