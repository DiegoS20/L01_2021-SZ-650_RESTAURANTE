using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L01_2021_SZ_650.Models;

namespace L01_2021_SZ_650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly RestauranteDbContext _context;

        public PedidosController(RestauranteDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pedido>> GetPedidos()
        {
            return _context.Pedidos.ToList();
        }

        [HttpPut("{id}")]
        public IActionResult PutPedido(int id, Pedido pedido)
        {
            if (!PedidoExists(id))
            {
                return NotFound();
            }

            pedido.PedidoId = id;
            _context.Entry(pedido).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        public ActionResult<Pedido> PostPedido(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            return Created();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePedido(int id)
        {
            var pedido = _context.Pedidos.Find(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _context.Pedidos.Remove(pedido);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("Cliente/{idCliente}")]
        public ActionResult<IEnumerable<Pedido>> GetByClient(int idCliente)
        {
            return _context.Pedidos.Where(p => p.ClienteId == idCliente).ToList();
        }

        [HttpGet("Motorista/{idMotorista}")]
        public ActionResult<IEnumerable<Pedido>> GetByMotorista(int idMotorista)
        {
            return _context.Pedidos.Where(p => p.MotoristaId == idMotorista).ToList();
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.PedidoId == id);
        }
    }
}
