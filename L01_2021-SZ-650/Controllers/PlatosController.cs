using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L01_2021_SZ_650.Models;
using Microsoft.IdentityModel.Tokens;

namespace L01_2021_SZ_650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatosController : ControllerBase
    {
        private readonly RestauranteDbContext _context;

        public PlatosController(RestauranteDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Plato>> GetPlatos(string precioLimite = "")
        {
            if (precioLimite.IsNullOrEmpty())
                return _context.Platos.ToList();

            return _context.Platos.Where(p => p.Precio < Decimal.Parse(precioLimite)).ToList();
        }

        [HttpPut("{id}")]
        public IActionResult PutPlato(int id, Plato plato)
        {
            if (!PlatoExists(id))
            {
                return NotFound();
            }

            plato.PlatoId = id;
            _context.Entry(plato).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        public ActionResult<Plato> PostPlato(Plato plato)
        {
            _context.Platos.Add(plato);
            _context.SaveChanges();

            return Created();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlato(int id)
        {
            var plato = _context.Platos.Find(id);
            if (plato == null)
            {
                return NotFound();
            }

            _context.Platos.Remove(plato);
            _context.SaveChanges();

            return NoContent();
        }

        private bool PlatoExists(int id)
        {
            return _context.Platos.Any(e => e.PlatoId == id);
        }
    }
}
