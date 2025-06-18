using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleApi.Data;
using VehicleApi.Models;

namespace VehicleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly VehicleContext _context;

        public VehiclesController(VehicleContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            var vehicles = await _context.Vehicles
                .Include(v => v.Brand)
                .Include(v => v.Equipment)
                .Select(v => new Vehicle
                {
                    Id = v.Id,
                    VehicleIdentificationNumber = v.VehicleIdentificationNumber,
                    Model = v.Model,
                    LicensePlate = v.LicensePlate,
                    Year = v.Year,
                    BrandId = v.BrandId,
                    Equipment = v.Equipment,
                    IsDeleted = v.IsDeleted
                })
                .ToListAsync();

            return vehicles;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            var vehicle = await _context.Vehicles
                .Include(v => v.Brand)
                .Include(v => v.Equipment)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(VehicleDto vehicleDto)
        {
            var vehicle = new Vehicle
            {
                VehicleIdentificationNumber = vehicleDto.VehicleIdentificationNumber,
                LicensePlate = vehicleDto.LicensePlate,
                Model = vehicleDto.Model,
                Year = vehicleDto.Year,
                BrandId = vehicleDto.BrandId,
                Brand = await _context.Brands.FindAsync(vehicleDto.BrandId),
                Equipment = await _context.VehicleEquipments
                .Where(e => vehicleDto.EquipmentIds.Contains(e.Id))
                .ToListAsync(),
                IsDeleted = false
            };

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.Id }, vehicle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle(int id, VehicleDto vehicleDto)
        {
            var vehicle = await _context.Vehicles
            .Include(v => v.Equipment)
            .FirstOrDefaultAsync(v => v.Id == id);

            if (vehicle == null) return NotFound();

            vehicle.VehicleIdentificationNumber = vehicleDto.VehicleIdentificationNumber;
            vehicle.LicensePlate = vehicleDto.LicensePlate;
            vehicle.Model = vehicleDto.Model;
            vehicle.Year = vehicleDto.Year;
            vehicle.Brand = await _context.Brands.FindAsync(vehicleDto.BrandId);
            vehicle.BrandId = vehicleDto.BrandId;

            _context.Entry(vehicle).State = EntityState.Modified;

            vehicle.Equipment.Clear();
            vehicle.Equipment = await _context.VehicleEquipments
                .Where(e => vehicleDto.EquipmentIds.Contains(e.Id))
                .ToListAsync();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Vehicles.Any(v => v.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            vehicle.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}