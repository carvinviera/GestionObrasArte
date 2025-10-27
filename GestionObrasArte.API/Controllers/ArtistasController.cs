using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionObrasArte.API.Data;
using GestionObrasArte.Shared.Models;

namespace GestionObrasArte.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArtistasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/artistas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artista>>> GetArtistas([FromQuery] string? nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return await _context.Artistas.ToListAsync();
            }

            string filtro = nombre.ToLower();

            return await _context.Artistas
                .Where(a =>
                    a.NombreArtista.ToLower().Contains(filtro) ||
                    a.ApellidosArtista.ToLower().Contains(filtro))
                .ToListAsync();
        }

        // POST: api/artistas
        [HttpPost]
        public async Task<ActionResult<Artista>> PostArtista(Artista artista)
        {
            _context.Artistas.Add(artista);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetArtistas), new { id = artista.IdArtista }, artista);
        }

        // PUT: api/artistas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtista(int id, Artista artista)
        {
            if (id != artista.IdArtista)
            {
                return BadRequest();
            }
            _context.Entry(artista).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/artistas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtista(int id)
        {
            var artista = await _context.Artistas.FindAsync(id);
            if (artista == null)
            {
                return NotFound();
            }
            _context.Artistas.Remove(artista);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}