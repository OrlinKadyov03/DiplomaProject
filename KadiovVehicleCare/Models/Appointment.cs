using KadiovVehicleCare.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace KadiovVehicleCare.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        public string? EmployeeId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        [StringLength(1000)]
        public string? Notes { get; set; }

        public Client? Client { get; set; }
        public Car? Car { get; set; }
        public Service? Service { get; set; }
    }
}
