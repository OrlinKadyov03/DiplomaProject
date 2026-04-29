using System.ComponentModel.DataAnnotations;

namespace KadiovVehicleCare.ViewModels
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Клиент")]
        public string ClientFullName { get; set; } = string.Empty;

        [Display(Name = "Автомобил")]
        public string CarInfo { get; set; } = string.Empty;

        [Display(Name = "Услуга")]
        public string ServiceName { get; set; } = string.Empty;

        [Display(Name = "Служител")]
        public string? EmployeeId { get; set; }

        [Display(Name = "Дата и час")]
        public DateTime AppointmentDate { get; set; }

        [Display(Name = "Статус")]
        public string Status { get; set; } = string.Empty;

        [Display(Name = "Бележки")]
        public string? Notes { get; set; }
    }
}
