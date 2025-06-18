using System.ComponentModel.DataAnnotations;

namespace VehicleApi.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        public int VehicleIdentificationNumber { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string LicensePlate { get; set; } = string.Empty;

        [Required]
        public int Year { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public ICollection<VehicleEquipment> Equipment { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
    public class VehicleDto
    {
        public int Id { get; set; }
        public string Model { get; set; } = string.Empty;
        public int VehicleIdentificationNumber { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public int Year { get; set; }
        public int BrandId { get; set; }
        public List<int> EquipmentIds { get; set; } = [];
    }
}