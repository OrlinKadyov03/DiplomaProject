using System.ComponentModel.DataAnnotations;

namespace KadiovVehicleCare.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(0, 10000)]
        public decimal Price { get; set; }

        [Range(1, 1000)]
        public int DurationInMinutes { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
