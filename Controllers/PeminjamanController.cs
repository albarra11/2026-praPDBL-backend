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
        public async Task<ActionResult<IEnumerable<PeminjamanResponseDto>>> GetAll(
            [FromQuery] StatusPeminjaman? status,
            [FromQuery] string? keyword,
            [FromQuery] string? ruangan,
            [FromQuery] DateTime? tanggal
        )
        {
            var query = _context.Peminjaman.AsQueryable();

            if (status.HasValue)
                query = query.Where(p => p.Status == status.Value);

            if (!string.IsNullOrWhiteSpace(ruangan))
                query = query.Where(p => p.NamaRuangan.Contains(ruangan));

            if (tanggal.HasValue)
                query = query.Where(p => p.Tanggal.Date == tanggal.Value.Date);

            if (!string.IsNullOrWhiteSpace(keyword))
                query = query.Where(p =>
                    p.NamaPeminjam.Contains(keyword) ||
                    p.NomorPeminjam.Contains(keyword) ||
                    p.NamaRuangan.Contains(keyword) ||
                    p.AlasanPeminjaman.Contains(keyword)
                );

            var data = await query
                .OrderByDescending(p => p.Id)
                .Select(p => new PeminjamanResponseDto
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
            var p = await _context.Peminjaman.FirstOrDefaultAsync(x => x.Id == id);

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var peminjaman = await _context.Peminjaman.FirstOrDefaultAsync(p => p.Id == id);
            if (peminjaman == null) return NotFound("Data peminjaman tidak ditemukan");

            peminjaman.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}