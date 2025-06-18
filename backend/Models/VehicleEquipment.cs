using System.ComponentModel.DataAnnotations;

namespace VehicleApi.Models
{
    public class VehicleEquipment
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}