using System.ComponentModel.DataAnnotations;

namespace VehicleApi.Models
{
    public class Brand
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}