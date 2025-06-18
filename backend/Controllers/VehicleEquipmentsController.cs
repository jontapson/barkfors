using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleApi.Data;
using VehicleApi.Models;

namespace VehicleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleEquipmentsController : ControllerBase
    {
        private readonly VehicleContext _context;

        public VehicleEquipmentsController(VehicleContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleEquipment>>> GetEquipments()
        {
            return await _context.VehicleEquipments.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleEquipment>> GetEquipment(int id)
        {
            var equipment = await _context.VehicleEquipments.FindAsync(id);

            if (equipment == null)
            {
                return NotFound();
            }

            return equipment;
        }

        [HttpPost]
        public async Task<ActionResult<VehicleEquipment>> PostEquipment(VehicleEquipment equipment)
        {
            _context.VehicleEquipments.Add(equipment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEquipment), new { id = equipment.Id }, equipment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipment(int id, VehicleEquipment equipment)
        {
            if (id != equipment.Id)
            {
                return BadRequest();
            }

            _context.Entry(equipment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipment(int id)
        {
            var equipment = await _context.VehicleEquipments.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }

            _context.VehicleEquipments.Remove(equipment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}