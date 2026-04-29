using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace KadiovVehicleCare.ViewModels
{
    public class EditAppointmentViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Клиент")]
        public int ClientId { get; set; }

        [Required]
        [Display(Name = "Автомобил")]
        public int CarId { get; set; }

        [Required]
        [Display(Name = "Услуга")]
        public int ServiceId { get; set; }

        [Display(Name = "Служител")]
        public string? EmployeeId { get; set; }

        [Required]
        [Display(Name = "Дата и час")]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [Display(Name = "Статус")]
        public string Status { get; set; } = "Pending";

        [Display(Name = "Бележки")]
        [StringLength(1000)]
        public string? Notes { get; set; }

        public IEnumerable<SelectListItem> Clients { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Cars { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Services { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Statuses { get; set; } = new List<SelectListItem>();
    }
}
