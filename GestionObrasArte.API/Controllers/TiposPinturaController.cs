using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionObrasArte.API.Data;
using GestionObrasArte.Shared.Models;

namespace GestionObrasArte.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposPinturaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TiposPinturaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/tipospintura
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoPintura>>> GetTiposPintura([FromQuery] string? titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                return await _context.TiposPintura.ToListAsync();
            }
            return await _context.TiposPintura
                                 .Where(t => t.TítuloTipoPintura.Contains(titulo))
                                 .ToListAsync();
        }

        // POST: api/tipospintura
        [HttpPost]
        public async Task<ActionResult<TipoPintura>> PostTipoPintura(TipoPintura tipoPintura)
        {
            _context.TiposPintura.Add(tipoPintura);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTiposPintura), new { id = tipoPintura.IdTipoPintura }, tipoPintura);
        }

        // PUT: api/tipospintura/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoPintura(int id, TipoPintura tipoPintura)
        {
            if (id != tipoPintura.IdTipoPintura)
            {
                return BadRequest();
            }
            _context.Entry(tipoPintura).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/tipospintura/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoPintura(int id)
        {
            var tipoPintura = await _context.TiposPintura.FindAsync(id);
            if (tipoPintura == null)
            {
                return NotFound();
            }
            _context.TiposPintura.Remove(tipoPintura);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}