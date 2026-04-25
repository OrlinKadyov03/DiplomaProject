using System.ComponentModel.DataAnnotations;

namespace KadiovVehicleCare.ViewModels
{
    public class ServiceViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Име")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Продължителност (минути)")]
        public int DurationInMinutes { get; set; }
    }
}
