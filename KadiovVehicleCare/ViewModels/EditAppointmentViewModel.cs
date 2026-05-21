using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace KadiovVehicleCare.ViewModels
{
    public class EditAppointmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Полето Клиент е задължително!")]
        [Display(Name = "Клиент")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Полето Автомобил е задължително!")]
        [Display(Name = "Автомобил")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "Полето Услуга е задължително!")]
        [Display(Name = "Услуга")]
        public int ServiceId { get; set; }

        [Display(Name = "Служител")]
        public string? EmployeeId { get; set; }

        [Required(ErrorMessage = "Полето Дата и час е задължително!")]
        [Display(Name = "Дата и час")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Полето Статус е задължително!")]
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
