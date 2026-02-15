using System;

namespace PinjamRuanganAPI.DTOs
{
    public class PeminjamanResponseDto
    {
        public int Id {get; set;}
        public string NamaPeminjam { get; set; } = null!;
        public string NomorPeminjam { get; set; } = null!;
        public string AlasanPeminjaman { get; set; } = null!;
        public string NamaRuangan { get; set; } = null!;
        public DateTime Tanggal { get; set; }
        public TimeSpan WaktuMulai { get; set; }
        public TimeSpan WaktuSelesai { get; set; }
        public string Status { get; set; } = null!;
    }
}