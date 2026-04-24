using System.ComponentModel.DataAnnotations;

namespace KadiovVehicleCare.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [Phone]
        public int PhoneNumber { get; set; }
        [EmailAddress]
        public string? Email { get; set; }

        public DateTime CreatedAt { get; set; }
        public ICollection<Car> Cars { get; set; } = new List<Car>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
