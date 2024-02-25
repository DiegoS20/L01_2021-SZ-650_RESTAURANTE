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
    public class MotoristasController : ControllerBase
    {
        private readonly RestauranteDbContext _context;

        public MotoristasController(RestauranteDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Motorista>> GetMotoristas(string name = "")
        {
            if (name.IsNullOrEmpty())
                return _context.Motoristas.ToList();

            return _context.Motoristas.Where(m => m.NombreMotorista == name).ToList();
        }

        [HttpPut("{id}")]
        public IActionResult PutMotorista(int id, Motorista motorista)
        {
            if (!MotoristaExists(id))
            {
                return NotFound();
            }

            motorista.MotoristaId = id;
            _context.Entry(motorista).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        public ActionResult<Motorista> PostMotorista(Motorista motorista)
        {
            _context.Motoristas.Add(motorista);
            _context.SaveChanges();

            return Created();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMotorista(int id)
        {
            var motorista = _context.Motoristas.Find(id);
            if (motorista == null)
            {
                return NotFound();
            }

            _context.Motoristas.Remove(motorista);
            _context.SaveChanges();

            return NoContent();
        }

        private bool MotoristaExists(int id)
        {
            return _context.Motoristas.Any(e => e.MotoristaId == id);
        }
    }
}
