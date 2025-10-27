using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionObrasArte.API.Data;
using GestionObrasArte.Shared.Models;

namespace GestionObrasArte.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PinturasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PinturasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/pinturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pintura>>> GetPinturas([FromQuery] string? titulo)
        {
            var query = _context.Pinturas
                                .Include(p => p.Artista) // Incluimos datos relacionados
                                .Include(p => p.TipoPintura)
                                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(titulo))
            {
                query = query.Where(p => p.TituloPintura.Contains(titulo));
            }

            return await query.ToListAsync();
        }

        // POST: api/pinturas
        [HttpPost]
        public async Task<ActionResult<Pintura>> PostPintura(Pintura pintura)
        {
            // Evitar insertar objetos anidados
            pintura.Artista = null;
            pintura.TipoPintura = null;

            _context.Pinturas.Add(pintura);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPinturas), new { id = pintura.IdPintura }, pintura);
        }

        // PUT: api/pinturas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPintura(int id, Pintura pintura)
        {
            if (id != pintura.IdPintura)
            {
                return BadRequest();
            }

            // Asegurarnos de que solo actualizamos la entidad Pintura
            pintura.Artista = null;
            pintura.TipoPintura = null;

            _context.Entry(pintura).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/pinturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePintura(int id)
        {
            var pintura = await _context.Pinturas.FindAsync(id);
            if (pintura == null)
            {
                return NotFound();
            }
            _context.Pinturas.Remove(pintura);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}