using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KadiovVehicleCare.Models
{
    public class Car
    {
        [Required]
        [StringLength(50)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Brand { get; set; }
        [Required]
        [StringLength(50)]
        public string Model { get; set; }
        [Required]
        [StringLength(15)]
        public string PlateNumber { get; set; }

        public int Year { get; set; }
        [Required]
        [StringLength(30)]
        public string? Color { get; set; }

        [ForeignKey("Client")]

        // Външен ключ в базата
        // Пази кой е този клиент.
        public int ClientId { get; set; }

        // Navigation Property, Това е самият свързан обект в C#.
        // Той може да бъде от тип Null.
        // Дава целеият обект на клиента
        public Client? Client { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
