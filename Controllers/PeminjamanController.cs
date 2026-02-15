using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinjamRuanganAPI.Data;
using PinjamRuanganAPI.Entities;
using PinjamRuanganAPI.DTOs;
using PinjamRuanganAPI.Enums;

namespace PinjamRuanganAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeminjamanController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PeminjamanController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<PeminjamanResponseDto>> CreatePeminjaman(PeminjamanCreateDto dto)
        {
            // 1️⃣ Validasi waktu mulai < waktu selesai
            if (dto.WaktuMulai >= dto.WaktuSelesai)
            {
                return BadRequest("Waktu mulai harus lebih kecil dari waktu selesai.");
            }

            // 2️⃣ Cek bentrok jadwal
            var bentrok = await _context.Peminjaman.AnyAsync(p =>
                p.NamaRuangan == dto.NamaRuangan &&
                p.Tanggal == dto.Tanggal &&
                dto.WaktuMulai < p.WaktuSelesai &&
                dto.WaktuSelesai > p.WaktuMulai
            );

            if (bentrok)
            {
                return BadRequest("Jadwal bentrok dengan peminjaman lain.");
            }

            var peminjaman = new Peminjaman
            {
                NamaPeminjam = dto.NamaPeminjam,
                NomorPeminjam = dto.NomorPeminjam,
                AlasanPeminjaman = dto.AlasanPeminjaman,
                NamaRuangan = dto.NamaRuangan,
                Tanggal = dto.Tanggal,
                WaktuMulai = dto.WaktuMulai,
                WaktuSelesai = dto.WaktuSelesai
            };

            _context.Peminjaman.Add(peminjaman);
            await _context.SaveChangesAsync();

            var response = new PeminjamanResponseDto
            {
                Id = peminjaman.Id,
                NamaPeminjam = peminjaman.NamaPeminjam,
                NomorPeminjam = peminjaman.NomorPeminjam,
                AlasanPeminjaman = peminjaman.AlasanPeminjaman,
                NamaRuangan = peminjaman.NamaRuangan,
                Tanggal = peminjaman.Tanggal,
                WaktuMulai = peminjaman.WaktuMulai,
                WaktuSelesai = peminjaman.WaktuSelesai,
                Status = peminjaman.Status
            };

            return CreatedAtAction(nameof(GetById), new {id = peminjaman.Id}, response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeminjamanResponseDto>>> GetAll()
        {
            var data = await _context.Peminjaman.Select(p => new PeminjamanResponseDto
            {
                Id = p.Id,
                NamaPeminjam = p.NamaPeminjam,
                NomorPeminjam = p.NomorPeminjam,
                AlasanPeminjaman = p.AlasanPeminjaman,
                NamaRuangan = p.NamaRuangan,
                Tanggal = p.Tanggal,
                WaktuMulai = p.WaktuMulai,
                WaktuSelesai = p.WaktuSelesai,
                Status = p.Status
            }).ToListAsync();

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PeminjamanResponseDto>> GetById(int id)
        {
            var p = await _context.Peminjaman.FindAsync(id);

            if(p == null) return NotFound();
            var response = new PeminjamanResponseDto
            {
                Id = p.Id,
                NamaPeminjam = p.NamaPeminjam,
                NomorPeminjam = p.NomorPeminjam,
                AlasanPeminjaman = p.AlasanPeminjaman,
                NamaRuangan = p.NamaRuangan,
                Tanggal = p.Tanggal,
                WaktuMulai = p.WaktuMulai,
                WaktuSelesai = p.WaktuSelesai,
                Status = p.Status
            };
            
            return Ok(response);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateStatusDto dto)
        {
            var peminjaman = await _context.Peminjaman.FindAsync(id);

            if(peminjaman == null) return NotFound("Data peminjaman tidak ditemukan");

            peminjaman.Status = dto.Status;
            await _context.SaveChangesAsync();
            return Ok(peminjaman.Status.ToString());
        }
    }
}